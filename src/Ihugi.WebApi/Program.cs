using Ihugi.Application;
using Ihugi.Infrastructure;
using Ihugi.Infrastructure.BackgroundJobs;
using Ihugi.Infrastructure.Interceptors;
using Ihugi.Presentation;
using Ihugi.WebApi.Config;
using Ihugi.WebApi.Hubs;
using Ihugi.WebApi.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Quartz;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// TODO: Вынести настройку Quartz в Ihugi.Infrastructure
builder.Services.AddQuartz(configure =>
{
    var jobKey = new JobKey(nameof(ProcessOutboxMessagesJob));

    configure
        .AddJob<ProcessOutboxMessagesJob>(jobKey)
        .AddTrigger(
            trigger =>
            {
                trigger.ForJob(jobKey)
                    .WithSimpleSchedule(
                        schedule =>
                        {
                            schedule.WithIntervalInSeconds(10)
                                .RepeatForever();
                        });
            });
});

builder.Services.AddQuartzHostedService();

// TODO: Вынести Redis в Ihugi.Infrastructure
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});

builder.Services.AddControllers().AddApplicationPart(typeof(Ihugi.Presentation.AssemblyReference).Assembly);

builder.Services.AddSingleton<ConvertDomainEventToOutboxMessageInterceptor>();

// TODO: Вынести добавление контекста в Ihugi.Infrastructure
builder.Services.ConfigureOptions<MySqlDatabaseOptionsSetup>();

builder.Services.AddDbContext<AppDbContext>(
    (serviceProvider, dbContextOptionsBuilder) =>
    {
        var databaseOptions = serviceProvider.GetService<IOptions<MySqlDatabaseOptions>>()!.Value;
        var interceptor = serviceProvider.GetService<ConvertDomainEventToOutboxMessageInterceptor>();

        dbContextOptionsBuilder.UseMySql(
                databaseOptions.ConnectionString,
                new MySqlServerVersion(databaseOptions.Version),
                mySqlDbContextOptionsBuilder =>
                {
                    mySqlDbContextOptionsBuilder.EnableRetryOnFailure(
                        databaseOptions.MaxRetryCount,
                        new TimeSpan(databaseOptions.MaxRetryDelay),
                        databaseOptions.ErrorNumbersToAdd
                    );
                }
            )
            .AddInterceptors(interceptor!);

        dbContextOptionsBuilder.EnableDetailedErrors(databaseOptions.EnableDetailedErrors);

        dbContextOptionsBuilder.EnableSensitiveDataLogging(databaseOptions.EnableSensitiveDataLogging);
    });

builder.Services
    .AddApplication()
    .AddInfrastructure()
    .AddPresentation();

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration)
);

// TODO: Подумать куда вынести SignalR и как работать с его хабами
builder.Services.AddSignalR();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowCredentials()
            .AllowAnyMethod();
    });
});

builder.Services.AddSwaggerGen(static options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo()
    {
        Version = "v1",
        Title = "API Ihugi",
        Description = "Сервис для приложения чата Ihugi",
        Contact = new OpenApiContact
        {
            Name = "Email главного разработчика",
            Email = "brnv.ma@gmail.com"
        }
    });

    foreach (var name in Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.xml",
                 SearchOption.AllDirectories))
    {
        options.IncludeXmlComments(name);
    }

    options.SchemaFilter<EnumSchemaFilter>();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseRouting();

app.MapHub<ChatHub>("/chat");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();