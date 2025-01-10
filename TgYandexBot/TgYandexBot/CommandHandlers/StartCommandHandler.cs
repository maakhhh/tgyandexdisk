using System.Text;
using System.Text.Json;
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
            var clientId = "тут клиент_id";
            var authUrl = $"https://oauth.yandex.ru/authorize?response_type=code&client_id={clientId}";

            await client.SendTextMessageAsync(msg.Chat.Id,
                $"Для работы с ботом авторизуйтесь через Яндекс: {authUrl} " +
                $"После чего введите команду /login и код подтверждения через пробел");
        }
    }
    
}
