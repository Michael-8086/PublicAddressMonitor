using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.Options;
using PublicAddressMonitor.Options;

namespace PublicAddressMonitor.Services;

public class MailgunService : IMailService
{
    private readonly HttpClient _client;
    private readonly IOptions<MailgunOptions> _mailgunOptions;

    public MailgunService(IHttpClientFactory factory, IOptions<MailgunOptions> mailgunOptions)
    {
        _mailgunOptions = mailgunOptions;
        _client = factory.CreateClient();
        if (_mailgunOptions.Value.BaseAddress != null) _client.BaseAddress = new Uri(_mailgunOptions.Value.BaseAddress);

        var apiKey = Encoding.ASCII.GetBytes($"api:{_mailgunOptions.Value.ApiKey}");
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Basic", Convert.ToBase64String(apiKey));
    }

    public async Task<HttpResponseMessage> SendMail(string messageBody)
    {
        MultipartFormDataContent postData = new MultipartFormDataContent();
        postData.Add(new StringContent(_mailgunOptions.Value.FromAddress), "from");
        postData.Add(new StringContent(_mailgunOptions.Value.ToAddress), "to");
        postData.Add(new StringContent("public address"), "subject");
        postData.Add(new StringContent(messageBody), "html");
        var responseMessage = await _client.PostAsync($"{_mailgunOptions.Value.Domain}/messages", postData);

        return responseMessage;
    }
}