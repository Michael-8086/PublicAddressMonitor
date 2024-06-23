using PublicAddressMonitor.Options;
using PublicAddressMonitor.Services;

namespace PublicAddressMonitor;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);
        builder.Services.AddHostedService<ConsoleHostedService>();
        builder.Services.AddHttpClient();
        builder.Services
            .AddSingleton<IPublicAddressService, IpifyAddressService>()
            .AddSingleton<IMailService, MailgunService>()
            .AddSingleton<IFileService, FileService>();
        builder.Services.AddOptions<MailgunOptions>().Bind(builder.Configuration.GetSection("Mailgun"));

        var host = builder.Build();
        await host.RunAsync();
    }
}