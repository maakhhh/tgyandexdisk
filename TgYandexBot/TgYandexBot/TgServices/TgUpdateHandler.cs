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
        var command = string.Empty;

        switch (message.Type)
        {
            case MessageType.Text:
                command = message.EntityValues.First();
                break;
            case MessageType.Document:
                command = message.Caption;
                break;
        }
        
        var commandHandler = commandProvider.GetCommandHandler(command);
        
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