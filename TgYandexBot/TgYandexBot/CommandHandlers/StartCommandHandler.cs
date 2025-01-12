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
        if (!IsValidMessage(update, out var message))
        {
            await SendInvalidCommandMessage(client, update.Message!.Chat.Id);
            return;
        }
        await SendAuthorizationInstructions(client, message.Chat.Id);
    }

    private bool IsValidMessage(Update update, out Message message)
    {
        message = update.Message!;
        return !string.IsNullOrWhiteSpace(message.Text);
    }

    private async Task SendInvalidCommandMessage(ITelegramBotClient client, long chatId)
    {
        const string invalidCommandText = "Некорректная команда.";
        await client.SendTextMessageAsync(chatId, invalidCommandText);
    }

    private async Task SendAuthorizationInstructions(ITelegramBotClient client, long chatId)
    {
        const string clientId = "d865d50859174d828f62e1844f2bc69e";
        var authUrl = $"https://oauth.yandex.ru/authorize?response_type=code&client_id={clientId}";
        var instructions = $"Для работы с ботом авторизуйтесь через Яндекс: {authUrl} " +
                           "После чего введите команду /login и код подтверждения через пробел";

        await client.SendTextMessageAsync(chatId, instructions);
    }

    
}
