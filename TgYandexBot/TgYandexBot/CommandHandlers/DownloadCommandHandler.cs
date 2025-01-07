using Telegram.Bot;
using Telegram.Bot.Types;
using TgYandexBot.Core.Interfaces;

namespace TgYandexBot.CommandHandlers
{
    public class DownloadCommandHandler : ICommandHandler
    {
        //private IYandexDiskService _yandexDiskService;
        
        //public DownloadCommandHandler(IYandexDiskService yandexDiskService)
        //{
        //    _yandexDiskService = yandexDiskService;
        //}
        //Тоже для Яндекса

        public string GetCommandName() => "/download";

        public async Task HandleCommand(ITelegramBotClient client, Update update)
        {
            var msg = update.Message;
            var fileName = msg.Text.Split()[1];

            await using Stream stream = System.IO.File.OpenRead($"temp/{msg.From.Id}/{fileName}");

            //var currentDir = string.Empty;
            //await using Stream stream = _yandexDiskService.DownloadFileAsync($"{currentDir}/{fileName}")
            //Это для Яндекса, все файлы пока локально загружаются

            var botMsg = await client.SendDocument(msg.Chat.Id, InputFile.FromStream(stream), "Вот ваш файл:");
        }
    }
}
