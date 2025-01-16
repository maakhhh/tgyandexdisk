using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using TgYandexBot.Core.Interfaces;
using YandexDisk.Client.Clients;
using YandexDisk.Client.Http;
using YandexDisk.Client.Protocol;

namespace TgYandexBot.TgServices;

public class YandexDiskService(IUserRepository userRepository, IConfiguration configuration, HttpClient httpClient) : IFileManagerService
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
        var rootFolder = await diskApi.MetaInfo.GetInfoAsync(new ResourceRequest
        {
            Path = "/"
        }, CancellationToken.None);

        var files = rootFolder.Embedded?.Items
            .Where(item => item.Type == ResourceType.File)
            .Select(item => item.Name);

        return files != null ? string.Join("\n", files) : string.Empty;
    }
    
    public async Task<string> ExchangeCodeForTokenAsync(string code)
    {
        const string url = "https://oauth.yandex.ru/token";
        string clientId = configuration["YandexDiskSettings:ClientId"] ?? throw new ArgumentNullException();
        string clientSecret = configuration["YandexDiskSettings:ClientSecret"] ?? throw new ArgumentNullException();
        
        
        var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));
        //httpClient.DefaultRequestHeaders.Add("Authorization", $"Basic {credentials}");
        httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", credentials);


        var requestBody = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("grant_type", "authorization_code"),
            new KeyValuePair<string, string>("code", code),
            new KeyValuePair<string, string>("client_id", clientId),
            new KeyValuePair<string, string>("client_secret", clientSecret)
        });

        var response = await httpClient.PostAsync(url, requestBody);

        if (!response.IsSuccessStatusCode)
        {
            return string.Empty;
        }

        var responseContent = await response.Content.ReadAsStringAsync();
        
        using var jsonDoc = JsonDocument.Parse(responseContent);
        return jsonDoc.RootElement.GetProperty("access_token").GetString() ?? string.Empty;
    }
}
