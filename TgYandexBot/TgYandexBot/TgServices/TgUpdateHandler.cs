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
        if (update.Type is not (UpdateType.Message or UpdateType.CallbackQuery))
            return;
    
        var command = update.Type switch
        {
            UpdateType.Message => GetCommandFromMessage(update),
            UpdateType.CallbackQuery => GetCommandFromCallbackQuery(update),
        };

        var message = update.Type switch
        {
            UpdateType.Message => update.Message,
            UpdateType.CallbackQuery => update.CallbackQuery!.Message,
        };

        var commandHandler = commandProvider.GetCommandHandler(command);
        
        if (commandHandler.HaveValue)
            await commandHandler.Value.HandleCommand(client, update);
        else
        {
            await client.SendMessage(
                chatId: message.Chat.Id,
                text: "Для получения списка доступных команд используйте команду /help",
                cancellationToken: cancellationToken);
        }
    }

    private string GetCommandFromMessage(Update update)
    {
        if (update.Type != UpdateType.Message)
            throw new ArgumentException();
        
        var message = update.Message;

        return message.Type switch
        {
            MessageType.Text => message.EntityValues?.First() ?? message.Text,
            MessageType.Document => "/upload",
            _ => string.Empty
        };
    }

    private string GetCommandFromCallbackQuery(Update update)
    {
        if (update.Type != UpdateType.CallbackQuery)
            throw new ArgumentException();
        
        return update.CallbackQuery!.Data!.Split()[0];
    }
}