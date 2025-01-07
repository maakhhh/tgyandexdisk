using Telegram.Bot;
using Telegram.Bot.Types;
using TgYandexBot.Core.Interfaces;

namespace TgYandexBot.CommandHandlers;

public class StartCommandHandler : ICommandHandler
{
    public string GetCommandName() => "/start";

    public async Task HandleCommand(ITelegramBotClient client, Update update)
    {
        var msg = update.Message;
        var splitted = msg.Text.Split();
        if (splitted.Length < 2 ) //Проверка на диплинки
            await client.SendMessage(update.Message!.Chat.Id, "Привет! Это стартовое сообщение, я пока в разработке.");
        else
        {
            var someTextFromYandex = splitted[1]; //Это инфа с диплинка, так можно будет с Yandex OAuth вытаскивать инфу
            await client.SendMessage(msg.Chat.Id, someTextFromYandex);
        }
    }
}