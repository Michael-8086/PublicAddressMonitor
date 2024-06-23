namespace PublicAddressMonitor.Services;

public interface IFileService
{
    /// <summary>
    /// Compares a given string to a string from a file.
    /// </summary>
    /// <param name="stringToCompare"></param>
    Task<bool> CompareToFile(string stringToCompare);

    /// <summary>
    /// Saves a given string to a file. 
    /// </summary>
    /// <param name="stringToSave"></param>
    Task SaveToFile(string stringToSave);
}