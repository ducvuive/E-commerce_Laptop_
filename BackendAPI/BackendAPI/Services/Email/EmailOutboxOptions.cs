namespace BackendAPI.Services.Email;

public sealed class EmailOutboxOptions
{
    public bool Enabled { get; set; } = true;
    public int PollIntervalSeconds { get; set; } = 10;
    public int BatchSize { get; set; } = 20;
    public int MaxRetryCount { get; set; } = 10;
}
