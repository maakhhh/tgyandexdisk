using Telegram.Bot;
using Telegram.Bot.Types;
using TgYandexBot.Core.Interfaces;

namespace TgYandexBot.CommandHandlers
{
    public class UploadCommandHandler(IYandexDiskService yandexDiskService) : ICommandHandler
    {
        public string GetCommandName() => "/upload";

        public async Task HandleCommand(ITelegramBotClient client, Update update)
        {
            var msg = update.Message;
            var doc = msg.Document;
            if (doc == null)
            {
                return;
            }

            var dir = $"temp/{msg.From.Id.ToString()}";

            var fileId = doc.FileId;
            var fileInfo = await client.GetFile(fileId);
            var filePath = fileInfo.FilePath;

            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            var tempDestination = $"{dir}/{doc.FileName}";
            await using Stream fs = System.IO.File.Create(tempDestination);
            if (filePath != null) await client.DownloadFile(filePath, fs);

            var currentDir = string.Empty;
            await using Stream stream = System.IO.File.OpenRead(tempDestination);
            await yandexDiskService.UploadFileAsync(currentDir, stream.ToString(), (int)msg.From.Id);
            Directory.Delete(tempDestination);
            //Яндекс
        }
    }
}
