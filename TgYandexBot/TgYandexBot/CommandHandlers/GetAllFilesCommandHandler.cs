using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TgYandexBot.Core.Interfaces;

namespace TgYandexBot.CommandHandlers;

public class GetAllFilesCommandHandler(IFileManagerService fileManagerService) : ICommandHandler
{
    public string GetCommandName() => "/getall";

    public async Task HandleCommand(ITelegramBotClient client, Update update)
    {
        var message = update.Message;
        var files = await fileManagerService.GetAllFilesAsync((int)message.From.Id);
        var messageText = "Список файлов на диске:\n\n" +
                          string.Join('\n', files) +
                          "\n\nДля загрузки файла выберите его из кнопок ниже:";
        await client.SendMessage(message.Chat.Id,
            messageText, replyMarkup: GetInlineKeyboard(files));
    }

    private InlineKeyboardMarkup GetInlineKeyboard(IEnumerable<string> files)
    {
        var keys = new List<InlineKeyboardButton[]>();
        
        for (var i = 0; i < files.Count(); i += 2)
        {
            var row = new InlineKeyboardButton[2];
            row[0] = InlineKeyboardButton.WithCallbackData(files.ElementAt(i),
                $"/cd {files.ElementAt(i)}");

            if (i + 1 < files.Count())
            {
                row[1] = InlineKeyboardButton.WithCallbackData(files.ElementAt(i + 1), 
                    $"/cd {files.ElementAt(i + 1)}");
            }
            else
            {
                row[1] = InlineKeyboardButton.WithCallbackData(" ", " ");
            }

            keys.Add(row);
        }

        return new InlineKeyboardMarkup(keys);
    }
}
