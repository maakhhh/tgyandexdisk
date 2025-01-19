using Microsoft.Extensions.Configuration;
using Telegram.Bot;
using Telegram.Bot.Types;
using TgYandexBot.Core.Interfaces;

namespace TgYandexBot.CommandHandlers;

public class StartCommandHandler : ICommandHandler
{
    private readonly IConfiguration _configuration;
    public StartCommandHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GetCommandName() => "/start";

    public async Task HandleCommand(ITelegramBotClient client, Update update)
    {
        if (!IsValidMessage(update, out var message))
        {
            await SendInvalidCommandMessage(client, update.Message!.Chat.Id);
            return;
        }
        await SendAuthorizationInstructions(client, message.Chat.Id, _configuration["YandexDiskSettings:ClientId"]);
    }

    private bool IsValidMessage(Update update, out Message message)
    {
        message = update.Message!;
        return !string.IsNullOrWhiteSpace(message.Text);
    }

    private async Task SendInvalidCommandMessage(ITelegramBotClient client, long chatId)
    {
        const string invalidCommandText = "Некорректная команда.";
        await client.SendMessage(chatId, invalidCommandText);
    }

    private async Task SendAuthorizationInstructions(ITelegramBotClient client, long chatId, string clientId)
    {
        //const string clientId = "d865d50859174d828f62e1844f2bc69e";
        var authUrl = $"https://oauth.yandex.ru/authorize?response_type=code&client_id={clientId}";
        var instructions = $"Для работы с ботом авторизуйтесь через Яндекс по ссылке ниже:\n\n" +
                           $"{authUrl}\n\n" +
                           "После чего введите команду /login и код подтверждения через пробел\n" +
                           "Например /login мойтокен222\n" +
                           "Список всех команд - /help";

        await client.SendMessage(chatId, instructions);
    }

    
}
