using PublicAddressMonitor.Services;

namespace PublicAddressMonitor;

public class ConsoleHostedService : IHostedService
{
    private readonly ILogger<ConsoleHostedService> _logger;
    private readonly IHostApplicationLifetime _applicationLifetime;
    private readonly IPublicAddressService _publicAddressService;
    private readonly IMailService _mailService;
    private readonly IFileService _fileService;

    public ConsoleHostedService(
        ILogger<ConsoleHostedService> logger,
        IPublicAddressService publicAddressService,
        IMailService mailService,
        IFileService fileService,
        IHostApplicationLifetime applicationLifetime)
    {
        _logger = logger;
        _applicationLifetime = applicationLifetime;
        _publicAddressService = publicAddressService;
        _mailService = mailService;
        _fileService = fileService;
    }

    public async Task StartAsync(CancellationToken stoppingToken)
    {
        try
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

            var publicAddress = await _publicAddressService.GetPublicAddress();
            _logger.LogInformation($"My public IP address is: {publicAddress}");

            if (!await _fileService.CompareToFile(publicAddress))
            {
                await _fileService.SaveToFile(publicAddress);
                var mailResponse = await _mailService.SendMail(publicAddress);
                _logger.LogInformation($"Response received from Mailgun: {mailResponse.StatusCode}");
            }
        }
        catch (OperationCanceledException)
        {
            _logger.LogWarning("Operation was canceled.");
        }
        finally
        {
            _applicationLifetime.StopApplication();
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
