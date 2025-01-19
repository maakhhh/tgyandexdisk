using Telegram.Bot;
using Telegram.Bot.Types;
using TgYandexBot.Core.Interfaces;

namespace TgYandexBot.CommandHandlers;

public class CallbackDownloadCommandHandler(IFileManagerService fileManagerService) : ICommandHandler
{
    public string GetCommandName() => "/cd";

    public async Task HandleCommand(ITelegramBotClient client, Update update)
    {
        client.AnswerCallbackQuery(update.CallbackQuery.Id, "Загрузка...");
        var data = update.CallbackQuery!.Data!;
        var fileName = data.Split()[1];
        
        await using Stream stream = await fileManagerService.DownloadFileAsync($"{fileName}", (int)update.CallbackQuery.From.Id);

        var dir = $"temp/{update.CallbackQuery.From.Id.ToString()}/{fileName}";

        await using FileStream fs = System.IO.File.Create(dir);
        stream.CopyTo(fs);
        fs.Close();
        stream.Close();

        await using FileStream fileToSend = System.IO.File.OpenRead(dir);

        await client.SendDocument(update.CallbackQuery!.Message!.Chat.Id, InputFile.FromStream(fileToSend), "Вот ваш файл:");

        fileToSend.Close();
        System.IO.File.Delete(dir);
    }
}