namespace TgYandexBot.Core.Interfaces;

/// <summary>
/// Основной интерфейс взаимодействия с Яндекс.Диском
/// </summary>
public interface IYandexDiskService
{
    /// <summary>
    /// Загрузить файл
    /// </summary>
    /// <param name="localPath">Путь к загруженному файлу</param>
    /// <param name="remotePath">Путь в которой загружается файл</param>
    /// <param name="userId">Id пользователя</param>
    /// <returns></returns>
    Task UploadFileAsync(string localPath, string remotePath, int userId);

    /// <summary>
    /// Скачивает файл с Яндекс.Диска
    /// </summary>
    /// <param name="filePath">Путь к файлу</param>
    /// <param name="userId">Id пользователя</param>
    /// <returns></returns>
    Task<Stream> DownloadFileAsync(string filePath, int userId);
}
