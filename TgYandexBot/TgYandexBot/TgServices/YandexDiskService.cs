using TgYandexBot.Core.Interfaces;
using YandexDisk.Client;
using YandexDisk.Client.Clients;
using YandexDisk.Client.Http;
using YandexDisk.Client.Protocol;

namespace TgYandexBot.TgServices
{
    public class YandexDiskService(IUserRepository userRepository) : IYandexDiskService
    {
        public async Task UploadFileAsync(string localPath, string remotePath, int userId)
        {
            var user = await userRepository.GetById(userId);
            var token = user.Value.AccessToken;
            var diskApi = new DiskHttpApi(token);
            await diskApi.Files.UploadFileAsync(
                path: remotePath,
                overwrite: true,
                localFile: localPath,
                cancellationToken: CancellationToken.None);
        }

        public async Task<Stream> DownloadFileAsync(string filePath, int userId)
        {
            var user = await userRepository.GetById(userId);
            var token = user.Value.AccessToken;
            var diskApi = new DiskHttpApi(token);
            var response = await diskApi.Files.DownloadFileAsync(filePath, CancellationToken.None);
            return response;
        }

        public async Task<string> GetAllFilesAsync(int userId)
        {
            var user = await userRepository.GetById(userId);
            var token = user.Value.AccessToken;
            var diskApi = new DiskHttpApi(token);
            // Получаем метаинформацию о содержимом корневой директории
            var rootFolder = await diskApi.MetaInfo.GetInfoAsync(new ResourceRequest
            {
                Path = "/", // Указываем путь к корневой директории
            }, CancellationToken.None);

            // Сохраняем имена только файлов
            var files = rootFolder.Embedded?.Items
                .Where(item => item.Type == ResourceType.File) // Оставляем только файлы
                .Select(item => item.Name);

            // Возвращаем объединённую строку или пустую строку, если файлов нет
            return files != null ? string.Join(", ", files) : string.Empty;
        }


            
    }
}
