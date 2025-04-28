using Microsoft.Extensions.Options;

namespace Ihugi.WebApi.Options;

public class MySqlDatabaseOptionsSetup : IConfigureOptions<MySqlDatabaseOptions>
{
    private const string ConfigurationSectionName = "MySqlDatabaseOptions";
    private readonly IConfiguration _configuration;

    public MySqlDatabaseOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(MySqlDatabaseOptions options)
    {
        options.ConnectionString = _configuration.GetConnectionString("MySql")!;
        
        _configuration.GetSection(ConfigurationSectionName).Bind(options);
    }
}