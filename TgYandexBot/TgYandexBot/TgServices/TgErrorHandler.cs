using Telegram.Bot;
using TgYandexBot.Core.Interfaces;

namespace TgYandexBot.TgServices;

public class TgErrorHandler : ITgErrorHandler
{
    public Task HandleErrorAsync(ITelegramBotClient client, Exception exception, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}