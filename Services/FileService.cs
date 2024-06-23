namespace PublicAddressMonitor.Services;

public class FileService : IFileService
{
    private readonly string _filepath = Path.Combine(Path.GetTempPath(), "public-address");

    public FileService()
    {
        if (!File.Exists(_filepath))
        {
            File.WriteAllText(_filepath, "");
        }
    }

    public async Task<bool> CompareToFile(string stringToCompare)
    {
        var storedPublicAddress = await File.ReadAllTextAsync(_filepath);

        return String.Equals(storedPublicAddress, stringToCompare, StringComparison.Ordinal);
    }

    public async Task SaveToFile(string stringToSave)
    {
        await File.WriteAllTextAsync(_filepath, stringToSave);
    }
}