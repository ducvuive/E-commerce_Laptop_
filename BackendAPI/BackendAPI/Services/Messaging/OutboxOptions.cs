namespace BackendAPI.Services.Messaging;

public sealed class OutboxOptions
{
    public bool Enabled { get; set; } = true;
    public int PollIntervalSeconds { get; set; } = 5;
    public int BatchSize { get; set; } = 20;
    public int MaxRetryCount { get; set; } = 10;
}
