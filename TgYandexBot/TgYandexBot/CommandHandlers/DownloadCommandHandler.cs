using Telegram.Bot;
using Telegram.Bot.Types;
using TgYandexBot.Core.Interfaces;

namespace TgYandexBot.CommandHandlers
{
    public class DownloadCommandHandler(IYandexDiskService yandexDiskService) : ICommandHandler
    {

         public string GetCommandName() => "/download";

        public async Task HandleCommand(ITelegramBotClient client, Update update)
        {
            var msg = update.Message;
            var fileName = msg.Text.Substring(10);
            
            await using Stream stream = await yandexDiskService.DownloadFileAsync($"{fileName}", (int)msg.From.Id);

            var botMsg = await client.SendDocument(msg.Chat.Id, InputFile.FromStream(stream), "Вот ваш файл:");
        }
    }
}
