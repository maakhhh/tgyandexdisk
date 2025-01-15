using Telegram.Bot;
using Telegram.Bot.Types;
using TgYandexBot.Core.Interfaces;

namespace TgYandexBot.CommandHandlers;

public class GetAllFilesCommandHandler(IFileManagerService fileManagerService) : ICommandHandler
{
    public string GetCommandName() => "/getall";

    public async Task HandleCommand(ITelegramBotClient client, Update update)
    {
        var message = update.Message;
        var files = await fileManagerService.GetAllFilesAsync((int)message.From.Id);
        await client.SendMessage(message.Chat.Id,
            $"Вот ваши файлы: {files}");
    }
}
