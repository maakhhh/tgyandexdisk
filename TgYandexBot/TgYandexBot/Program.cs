using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TgYandexBot.Core.Interfaces;

namespace TgYandexBot;

public static class Program
{
    public static async Task Main()
    {
        var services = new ServiceCollection()
            .UseHandlers()
            .UseTelegramService(BuildConfiguration())
            .BuildServiceProvider();

        var client = services.GetRequiredService<ITelegramBotService>();
        
        await client.StartReceivingAsync();
        
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
        
        client.StopReceiving();
    }

    private static IConfiguration BuildConfiguration()
    {
        return new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("settings.json", optional: false, reloadOnChange: true)
            .Build();
    }
}