namespace PublicAddressMonitor.Services;

public interface IMailService
{
    Task<HttpResponseMessage> SendMail(string messageBody);
}