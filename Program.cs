using PublicAddressMonitor.Services;

namespace PublicAddressMonitor;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);
        builder.Services.AddHostedService<Worker>();
        builder.Services.AddHttpClient();
        builder.Services.AddSingleton<IPublicAddressService, IpifyAddressService>();

        var host = builder.Build();
        host.Run();
    }
}