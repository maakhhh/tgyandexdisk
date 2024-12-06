namespace TgYandexBot.Core.Interfaces;

/// <summary>
/// Основной интерфейс взаимодействия с Яндекс.Диском
/// </summary>
public interface IYandexDiskService
{
    /// <summary>
    /// Загрузить файл
    /// </summary>
    /// <param name="filepath">Путь к файлу</param>
    /// <param name="filestream">Поток данных файла, который будет загружен</param>
    /// <returns></returns>
    Task UploadFileAsync(string filepath, Stream filestream);

    /// <summary>
    /// Скачивает файл с Яндекс.Диска
    /// </summary>
    /// <param name="filePath">Путь к файлу</param>
    /// <returns></returns>
    Task<Stream> DownloadFileAsync(string filePath);

    /// <summary>
    /// Удаляет файл с Яндекс.Диска
    /// </summary>
    /// <param name="filepath">Путь к файлу</param>
    /// <returns></returns>
    Task DeleteFileAsync(string filepath);

    /// <summary>
    /// Возвращает список файлов в указанной папке
    /// </summary>
    /// <param name="folderPath">Путь к папке</param>
    /// <returns>Коллекция названий файлов в указанной папке</returns>
    Task<IEnumerable<string>> FileListAsync(string folderPath);
    
    /// <summary>
    /// Проверяет, существует ли файл
    /// </summary>
    /// <param name="filepath">Путь к файлу</param>
    /// <returns>(true/false) Существует ли файл</returns>
    Task<bool> FileExistsAsync(string filepath);
}