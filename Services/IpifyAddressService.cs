namespace PublicAddressMonitor.Services;

public class IpifyAddressService : IPublicAddressService
{
    private readonly HttpClient _client;

    public IpifyAddressService(IHttpClientFactory factory)
    {
        _client = factory.CreateClient();
        _client.BaseAddress = new Uri("https://api.ipify.org");
    }

    public async Task<string> GetPublicAddress()
    {
        var publicAddress = await _client.GetStringAsync(_client.BaseAddress);

        return publicAddress;
    }
}