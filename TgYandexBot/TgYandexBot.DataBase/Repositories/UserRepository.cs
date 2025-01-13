using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using TgYandexBot.Core.Interfaces;
using TgYandexBot.Core.Models;

namespace TgYandexBot.DataBase.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly TgBotDbContext _dbContext;

		public UserRepository(TgBotDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<Result> Add(User user)
		{
			try
			{
				await _dbContext.AddAsync(user);
				await _dbContext.SaveChangesAsync();
				return Result.Success();
			}
			catch (Exception ex)
			{
				return Result.Failure(ex.Message);
			}
		}

		public async Task<Result> Delete(int id)
		{
			try
			{
				await _dbContext.Users
					.Where(x => x.Id == id)
					.ExecuteDeleteAsync();
				return Result.Success();
			}
			catch (Exception ex)
			{
				return Result.Failure(ex.Message);
			}
		}

		public async Task<Result<User>> GetById(int id)
		{
			var user = await _dbContext.Users
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.Id == id);
			if (user == null)
				return Result.Failure<User>("Not Found");
			return Result.Success<User>(user);
		}

		public async Task<Result> Update(int id, User newUser)
		{
			try
			{
				await _dbContext.Users
					.Where(x => x.Id == id)
					.ExecuteUpdateAsync(s => s
						.SetProperty(x => x.AccessToken, x => newUser.AccessToken)
						.SetProperty(x => x.UpdatedAt, x => DateTime.UtcNow));
				return Result.Success();
			}
			catch (Exception ex)
			{
				return Result.Failure(ex.Message);
			}
		}
	}
}
