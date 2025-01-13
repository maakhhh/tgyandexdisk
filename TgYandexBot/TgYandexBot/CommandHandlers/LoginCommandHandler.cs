using Telegram.Bot;
using Telegram.Bot.Types;
using TgYandexBot.Core.Interfaces;
using User = TgYandexBot.Core.Models.User;

namespace TgYandexBot.CommandHandlers;

public class LoginCommandHandler(IUserRepository userRepository, IYandexDiskService yandexDiskService)
    : ICommandHandler
{
    public string GetCommandName() => "/login";

    public async Task HandleCommand(ITelegramBotClient client, Update update)
    {
        if (!IsValidMessage(update, out var message))
        {
            await SendInvalidCommandMessage(client, update.Message!.Chat.Id);
            return;
        }

        var split = message.Text.Split(" ");
        if (split.Length == 2)
        {
            await ProcessLogin(client, message, split[1]);
        }
        else
        {
            await SendInvalidCommandMessage(client, message.Chat.Id);
        }
    }

    private bool IsValidMessage(Update update, out Message message)
    {
        message = update.Message!;
        return !string.IsNullOrWhiteSpace(message.Text);
    }

    private async Task SendInvalidCommandMessage(ITelegramBotClient client, long chatId)
    {
        const string invalidCommandText = "Некорректная команда.";
        await client.SendMessage(chatId, invalidCommandText);
    }

    private async Task ProcessLogin(ITelegramBotClient client, Message message, string code)
    {
        var authToken = await yandexDiskService.ExchangeCodeForTokenAsync(code);

        if (authToken is null)
        {
            await client.SendMessage(message.Chat.Id,
                "Не удалось получить токен. Проверьте правильность кода подтверждения.");
            return;
        }

        var existingUser = await userRepository.GetById((int)message.From.Id);

        if (!existingUser.IsFailure)
        {
            await client.SendMessage(message.Chat.Id,
                "Ваш токен уже присутствует в системе, вы можете пользоваться ботом.");
        }
        else
        {
            var newUser = new User((int)message.From.Id);
            newUser.SetAccessToken(authToken);
            await userRepository.Add(newUser);

            await client.SendMessage(message.Chat.Id,
                "Токен успешно сохранён! Теперь вы можете пользоваться ботом.");
        }
    }
}
