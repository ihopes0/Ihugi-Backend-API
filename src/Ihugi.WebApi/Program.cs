using Ihugi.Application;
using Ihugi.Application.Abstractions;
using Ihugi.Infrastructure;
using Ihugi.Infrastructure.BackgroundJobs;
using Ihugi.Infrastructure.Interceptors;
using Ihugi.Infrastructure.RealTime;
using Ihugi.Presentation;
using Ihugi.Presentation.Hubs;
using Ihugi.WebApi.Configurations;
using Ihugi.WebApi.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
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

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer();

builder.Services.ConfigureOptions<JwtOptionsSetup>();
builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();

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

builder.Services.AddScoped<IRealTimeCommunicationService, RtcService<ChatHub>>();

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

builder.Services.AddConfiguredSwagger();

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

app.UseAuthentication();

app.UseAuthorization();

app.MapHub<ChatHub>("/chat");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();