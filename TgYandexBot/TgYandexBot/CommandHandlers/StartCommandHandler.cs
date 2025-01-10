using Telegram.Bot;
using Telegram.Bot.Types;
using TgYandexBot.Core.Interfaces;
using User = TgYandexBot.Core.Models.User;

namespace TgYandexBot.CommandHandlers;

public class StartCommandHandler : ICommandHandler
{
    public string GetCommandName() => "/start";

    public async Task HandleCommand(ITelegramBotClient client, Update update)
    {
        var msg = update.Message;

        if (msg == null || string.IsNullOrWhiteSpace(msg.Text))
        {
            await client.SendTextMessageAsync(update.Message!.Chat.Id, "Некорректная команда.");
            return;
        }

        var splitted = msg.Text.Split(" ");
        if (splitted.Length < 2)
        {
            var clientId = "50e3d2c5f9e74287828351381e04fec7";
            var authUrl = $"https://oauth.yandex.ru/authorize?response_type=code&client_id={clientId}";

            await client.SendTextMessageAsync(msg.Chat.Id,
                $"Для работы с ботом авторизуйтесь через Яндекс: [Авторизоваться]({authUrl})");
        }
        else
        {
            string authToken;
            try
            {
                authToken = ExtractAccessToken(splitted[1]); // auth_token из диплинка
            }
            catch (Exception ex) { }
            
            await client.SendTextMessageAsync(msg.Chat.Id,
                $"Токен успешно сохранён! Теперь вы можете пользоваться ботом.");
            await client.SendMessage(msg.Chat.Id, splitted[1]);
        }
    }

    public static string ExtractAccessToken(string url)
    {
        var uri = new Uri(url);
        var fragment = uri.Fragment;
        var queryParams = fragment.TrimStart('#').Split('&');
        var tokenParam = queryParams.FirstOrDefault(p => p.StartsWith("access_token="));
        return tokenParam?.Split('=')[1];
    }

}
