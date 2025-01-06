using Microsoft.Extensions.Configuration;
using Telegram.Bot;
using Telegram.Bot.Polling;
using TgYandexBot.Core.Interfaces;

namespace TgYandexBot.TgServices;

public class TelegramBotService : ITelegramBotService
{
    private readonly ITelegramBotClient client;
    private readonly ITgErrorHandler errorHandler;
    private readonly ITgUpdateHandler updateHandler;
    private readonly CancellationTokenSource cancellationToken;
    
    public TelegramBotService(
        IConfiguration configuration,
        CancellationTokenSource cts,
        ITgErrorHandler errorHandler,
        ITgUpdateHandler updateHandler)
    {
        var token = configuration["TgBotSettings:Token"];
        if (token == null)
            throw new NullReferenceException("Token is null");
        client = new TelegramBotClient(token);
        this.errorHandler = errorHandler;
        this.updateHandler = updateHandler;
        cancellationToken = cts;
    }

    public async Task StartReceivingAsync()
    {
        var receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = []
        };

        client.StartReceiving(
            updateHandler.HandleUpdateAsync,
            errorHandler.HandleErrorAsync,
            receiverOptions, cancellationToken.Token);
        
        var me = await client.GetMe();
        Console.WriteLine($"Start listening for @{me.Username}");
    }

    public void StopReceiving()
    {
        cancellationToken.Cancel();
    }
}
