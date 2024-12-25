using Telegram.Bot;
using Telegram.Bot.Types;

namespace TgYandexBot.Core.Interfaces;

public interface ICommandHandler
{
    public string GetCommandName();
    public Task HandleCommand(ITelegramBotClient client, Update update);
}