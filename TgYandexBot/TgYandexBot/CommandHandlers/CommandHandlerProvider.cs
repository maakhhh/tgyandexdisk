using TgYandexBot.Core.Interfaces;
using TgYandexBot.Core.Results;

namespace TgYandexBot.CommandHandlers;

public class CommandHandlerProvider(IEnumerable<ICommandHandler> commandHandlers)
{
    public Result<ICommandHandler> GetCommandHandler(string commandName)
    {
        return commandHandlers.FirstOrDefault(c => c.GetCommandName() == commandName).AsResult();
    }

    public bool IsCommandExists(string commandName)
    {
        return commandHandlers.Any(c => c.GetCommandName() == commandName);
    }
}