namespace Ihugi.WebApi.Options;

public class MySqlDatabaseOptions
{
    public string ConnectionString { get; set; } = String.Empty;

    public string Version { get; set; } = String.Empty;
    
    public int MaxRetryCount { get; set; }

    public long MaxRetryDelay { get; set; }

    public ICollection<int>? ErrorNumbersToAdd { get; set; }

    public bool EnableDetailedErrors { get; set; }
    
    public bool EnableSensitiveDataLogging { get; set; }
}