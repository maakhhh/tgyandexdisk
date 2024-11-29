namespace TgYandexBot.Core.Models
{
	public class User
	{
		public int Id { get; }
		public string? OAuthToken { get; private set; }
		public string? AccessToken { get; private set; }
		public DateTime CreatedAt { get; }
		public DateTime UpdatedAt { get; }

		public User(int id)
		{
			Id = id;
			CreatedAt = DateTime.UtcNow;
			UpdatedAt = CreatedAt;
		}

		public void SetOauthToken(string token)
		{
			OAuthToken = token;
		}

		public void SetAccessToken(string token)
		{
			AccessToken = token;
		}
	}
}
