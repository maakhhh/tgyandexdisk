using System.Data;
using Telegram.Bot.Types;

namespace TgYandexBot.Core.Interfaces;

public interface ITelegramBotService
{
    public Task StartReceivingAsync();
    public void StopReceiving();
}
