using PublicAddressMonitor.Services;

namespace PublicAddressMonitor;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IPublicAddressService _publicAddressService;

    public Worker(ILogger<Worker> logger, IPublicAddressService publicAddressService)
    {
        _logger = logger;
        _publicAddressService = publicAddressService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }

            var publicAddress = await _publicAddressService.GetPublicAddress();
            Console.WriteLine($"My public IP address is: {publicAddress}");
            
            await Task.Delay(1000, stoppingToken);
        }
    }
}
