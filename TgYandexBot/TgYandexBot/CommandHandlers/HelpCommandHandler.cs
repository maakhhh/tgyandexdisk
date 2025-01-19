using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TgYandexBot.Core.Interfaces;

namespace TgYandexBot.CommandHandlers;

public class HelpCommandHandler : ICommandHandler
{
    public string GetCommandName() => "/help";

    public Task HandleCommand(ITelegramBotClient client, Update update)
    {
        var message = "Список доступных команд:\n" +
                      "/login {Токен} - команда для авторизации в Яндекс\n" +
                      "/start - команда со стартовой информацией\n" +
                      "/getall - команда для получения всех файлов на диске\n" +
                      "/download {имя файла} - команда для загрузки файла с диска\n" +
                      "/upload - команда для загрузки файла на диск (можно просто скинуть файл)";

        var keyboard = new ReplyKeyboardMarkup(
            new List<KeyboardButton[]>
            {
                new KeyboardButton[]
                {
                    new("/start"),
                    new("/help"),
                    new("/getall")
                },
            })
        {
            ResizeKeyboard = true,
        };

        return client.SendMessage(update.Message.Chat.Id, message, replyMarkup: keyboard);
    }
}