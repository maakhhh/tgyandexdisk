using Telegram.Bot;
using Telegram.Bot.Types;
using TgYandexBot.Core.Interfaces;

namespace TgYandexBot.CommandHandlers
{
    public class UploadCommandHandler(IFileManagerService fileManagerService) : ICommandHandler
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
            await using FileStream fs = System.IO.File.Create(tempDestination);
            var localPath = fs.Name;
            if (filePath != null) await client.DownloadFile(filePath, fs);
            fs.Close();

            var currentDir = $"/{doc.FileName}";
            await fileManagerService.UploadFileAsync(localPath, currentDir, (int)msg.From.Id);

            System.IO.File.Delete(tempDestination);
            //Яндекс
        }
    }
}
