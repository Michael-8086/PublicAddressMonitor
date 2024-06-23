namespace PublicAddressMonitor.Options;

public class MailgunOptions
{
    public string? BaseAddress { get; set; }
    public string? ApiKey { get; set; }
    public string? FromAddress { get; set; }
    public string? ToAddress { get; set; }
    public string? Domain { get; set; }
}