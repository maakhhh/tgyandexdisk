using Telegram.Bot;
using Telegram.Bot.Types;
using TgYandexBot.Core.Interfaces;

namespace TgYandexBot.CommandHandlers
{
    public class DownloadCommandHandler(IFileManagerService fileManagerService) : ICommandHandler
    {

         public string GetCommandName() => "/download";

        public async Task HandleCommand(ITelegramBotClient client, Update update)
        {
            var msg = update.Message;
            var fileName = msg.Text.Substring(10);
            
            await using Stream stream = await fileManagerService.DownloadFileAsync($"{fileName}", (int)msg.From.Id);

            var dir = $"temp/{msg.From.Id.ToString()}/{fileName}";

            await using FileStream fs = System.IO.File.Create(dir);
            stream.CopyTo(fs);
            fs.Close();
            stream.Close();

            await using FileStream fileToSend = System.IO.File.OpenRead(dir);

            await client.SendDocument(msg.Chat.Id, InputFile.FromStream(fileToSend), "Вот ваш файл:");

            fileToSend.Close();
            System.IO.File.Delete(dir);
        }
    }
}
