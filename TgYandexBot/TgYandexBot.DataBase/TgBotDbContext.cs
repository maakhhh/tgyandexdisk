using Microsoft.EntityFrameworkCore;
using TgYandexBot.Core.Models;
using TgYandexBot.DataBase.Configurations;

namespace TgYandexBot.DataBase
{
	public class TgBotDbContext : DbContext
	{
		public DbSet<User> Users { get; set; }

		public TgBotDbContext()
		{
			Database.EnsureCreated();
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseNpgsql("Host=localhost;Port=54322;Username=postgres;Password=postgres;Database=TgYandex");
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new UserConfiguration());
			base.OnModelCreating(modelBuilder);
		}
	}
}
