using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TgYandexBot.CommandHandlers;
using TgYandexBot.Core.Interfaces;
using TgYandexBot.TgServices;

namespace TgYandexBot;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection UseHandlers(this IServiceCollection services)
    {
        return services
            .AddSingleton<ITgUpdateHandler, TgUpdateHandler>()
            .AddSingleton<ITgErrorHandler, TgErrorHandler>();
    }

    public static IServiceCollection UseTelegramService(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddSingleton(configuration)
            .AddSingleton<CancellationTokenSource>()
            .AddSingleton<ITelegramBotService, TelegramBotService>();
    }

    public static IServiceCollection UseCommandHandlers(this IServiceCollection services)
    {
        return services
            .AddSingleton<ICommandHandler, StartCommandHandler>()
            .AddSingleton<CommandHandlerProvider>();
    }
}