using Telegram.Bot;
using Telegram.Bot.Types;

namespace TgYandexBot.Core.Interfaces;

public interface ITgUpdateHandler
{
    public Task HandleUpdateAsync(ITelegramBotClient client, Update update, CancellationToken cancellationToken);
}