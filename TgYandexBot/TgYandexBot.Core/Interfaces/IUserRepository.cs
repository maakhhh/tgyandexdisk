using TgYandexBot.Core.Models;
using CSharpFunctionalExtensions;

namespace TgYandexBot.Core.Interfaces
{
	public interface IUserRepository
	{
		Task<Result> Add(User user);
		Task<Result<User>> GetById(int id);
		Task<Result> Update(int id, User newUser);
		Task<Result> Delete(int id);
	}
}
