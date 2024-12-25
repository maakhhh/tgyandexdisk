using Telegram.Bot;

namespace TgYandexBot.Core.Interfaces;

public interface ITgErrorHandler
{
    public Task HandleErrorAsync(ITelegramBotClient client, Exception exception, CancellationToken cancellationToken);
}