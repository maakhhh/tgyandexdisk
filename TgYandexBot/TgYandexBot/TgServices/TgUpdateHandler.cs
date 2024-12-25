using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TgYandexBot.CommandHandlers;
using TgYandexBot.Core.Interfaces;

namespace TgYandexBot.TgServices;

public class TgUpdateHandler : ITgUpdateHandler
{
    private readonly CommandHandlerProvider commandProvider;

    public TgUpdateHandler(CommandHandlerProvider commandProvider)
    {
        this.commandProvider = commandProvider;    
    }
    
    public async Task HandleUpdateAsync(ITelegramBotClient client, Update update, CancellationToken cancellationToken)
    {
        if (update.Type != UpdateType.Message)
            return;
        
        var message = update.Message;
        
        if (message?.Text == null)
            return;
        
        var commandHandler = commandProvider.GetCommandHandler(message.Text);
        
        if (commandHandler.HaveValue)
            await commandHandler.Value.HandleCommand(client, update);
        else
        {
            await client.SendMessage(
                chatId: message.Chat.Id,
                text: message.Text,
                cancellationToken: cancellationToken);
        }
    }
}