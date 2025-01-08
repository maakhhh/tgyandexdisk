using TgYandexBot.Core.Interfaces;
using YandexDisk.Client;
using YandexDisk.Client.Clients;
using YandexDisk.Client.Http;

namespace TgYandexBot.TgServices
{
    public class YandexDiskService(IUserRepository userRepository, IDiskApi diskApi) : IYandexDiskService
    {
        public async Task UploadFileAsync(string localPath, string remotePath, int userId)
        {
            var user = await userRepository.GetById(userId);
            var token = user.Value.AccessToken;
            diskApi = new DiskHttpApi(token);
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
            diskApi = new DiskHttpApi(token);
            var response = await diskApi.Files.DownloadFileAsync(filePath, CancellationToken.None);
            return response;
        }
    }
}
