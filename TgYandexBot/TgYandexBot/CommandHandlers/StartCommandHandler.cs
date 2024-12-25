using Telegram.Bot;
using Telegram.Bot.Types;
using TgYandexBot.Core.Interfaces;

namespace TgYandexBot.CommandHandlers;

public class StartCommandHandler : ICommandHandler
{
    public string GetCommandName() => "/start";

    public async Task HandleCommand(ITelegramBotClient client, Update update)
    {
        await client.SendMessage(update.Message!.Chat.Id, "Привет! Это стартовое сообщение, я пока в разработке.");
    }
}