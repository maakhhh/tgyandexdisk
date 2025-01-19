using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TgYandexBot.CommandHandlers;
using TgYandexBot.Core.Interfaces;
using TgYandexBot.TgServices;
using TgYandexBot.DataBase.Repositories;
using TgYandexBot.DataBase;
using Microsoft.EntityFrameworkCore;

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

    public static IServiceCollection UseFileManagerService(this IServiceCollection services)
    {
        return services
            .AddHttpClient()
            .AddScoped<IFileManagerService, YandexDiskService>();
    }

    public static IServiceCollection UseCommandHandlers(this IServiceCollection services)
    {
        return services
            .AddSingleton<ICommandHandler, StartCommandHandler>()
            .AddSingleton<ICommandHandler, UploadCommandHandler>()
            .AddSingleton<ICommandHandler, DownloadCommandHandler>()
            .AddSingleton<ICommandHandler, LoginCommandHandler>()
            .AddSingleton<ICommandHandler, GetAllFilesCommandHandler>()
            .AddSingleton<ICommandHandler, HelpCommandHandler>()
            .AddSingleton<ICommandHandler, CallbackDownloadCommandHandler>()
            .AddSingleton<CommandHandlerProvider>();
    }
    public static IServiceCollection UseRepositories(this IServiceCollection services, IConfiguration configuration) 
    {
        return services
            .AddDbContext<TgBotDbContext>(options =>
            {
                var test = configuration.GetConnectionString(nameof(TgBotDbContext));
                options.UseNpgsql(configuration.GetConnectionString(nameof(TgBotDbContext)));
            })
            .AddScoped<IUserRepository, UserRepository>();
    }
}
