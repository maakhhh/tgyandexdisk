namespace TgYandexBot.Core.Interfaces;

public interface IFileManagerService
{
    Task UploadFileAsync(string localPath, string remotePath, int userId);

    Task<Stream> DownloadFileAsync(string filePath, int userId);

    public Task<string> GetAllFilesAsync(int userId);

    public Task<string> ExchangeCodeForTokenAsync(string code);
}
