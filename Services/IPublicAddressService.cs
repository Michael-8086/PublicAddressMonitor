namespace PublicAddressMonitor.Services;

public interface IPublicAddressService
{
    Task<string> GetPublicAddress();
}