using System.Text;
using System.Text.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using TgYandexBot.Core.Interfaces;
using User = TgYandexBot.Core.Models.User;

namespace TgYandexBot.CommandHandlers;

public class LoginCommandHandler : ICommandHandler
{
    private IUserRepository _userRepository;

    public LoginCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public string GetCommandName() => "/login";

    public async Task HandleCommand(ITelegramBotClient client, Update update)
    {
        var message = update.Message;
        if (message == null || string.IsNullOrWhiteSpace(message.Text))
        {
            await client.SendTextMessageAsync(update.Message!.Chat.Id, "Некорректная команда.");
            return;
        }

        var splitted = message.Text.Split(" ");
        if (splitted.Length == 2)
        {
            var code = splitted[1];
            var authToken = await ExchangeCodeForTokenAsync(code);
            var user = new User((int)message.From.Id);
            user.SetAccessToken(authToken);
            await _userRepository.Add(user);
            await client.SendTextMessageAsync(message.Chat.Id,
                $"Токен успешно сохранён! Теперь вы можете пользоваться ботом. {authToken}");
        }
    }
    
    private async Task<string> ExchangeCodeForTokenAsync(string code)
    {
        const string url = "https://oauth.yandex.ru/token";
        const string clientId = "тут клиент айди";
        const string clientSecret = "тут сикрет клиент";

        using var httpClient = new HttpClient();
        
        var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));
        httpClient.DefaultRequestHeaders.Add("Authorization", $"Basic {credentials}");
        
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
