using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TgYandexBot.Core.Interfaces;

namespace TgYandexBot.TgServices;

public class TgUpdateHandler : ITgUpdateHandler
{
    public async Task HandleUpdateAsync(ITelegramBotClient client, Update update, CancellationToken cancellationToken)
    {
        if (update.Type != UpdateType.Message)
            return;
        
        var message = update.Message;
        
        if (message?.Text == null)
            return;

        await client.SendMessage(
            chatId: message.Chat.Id,
            text: message.Text,
            cancellationToken: cancellationToken);
    }
}