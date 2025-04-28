namespace Ihugi.Infrastructure.Outbox;

// TODO: XML docs
public sealed class OutboxMessage
{
    public Guid Id { get; set; }

    public string Type { get; set; } = String.Empty;

    public string Content { get; set; } = String.Empty;

    public DateTime OccuredAtUtc { get; set; }

    public DateTime? ProcessedOnUtc { get; set; }
    
    public string? Error { get; set; }
}