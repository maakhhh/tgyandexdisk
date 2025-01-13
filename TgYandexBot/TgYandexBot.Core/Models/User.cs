namespace TgYandexBot.Core.Models
{
	public class User
	{
		public int Id { get; }
		public string? AccessToken { get; private set; }
		public DateTime CreatedAt { get; }
		public DateTime UpdatedAt { get; private set; }

		public User(int id)
		{
			Id = id;
			CreatedAt = DateTime.UtcNow;
			UpdatedAt = CreatedAt;
		}

		public void SetAccessToken(string token)
		{
			AccessToken = token;
			UpdatedAt = DateTime.UtcNow;
		}
	}
}
