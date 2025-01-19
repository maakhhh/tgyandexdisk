using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using TgYandexBot.Core.Models;
using TgYandexBot.DataBase.Configurations;

namespace TgYandexBot.DataBase
{
    public class TgBotDbContext(DbContextOptions<TgBotDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Username=postgres;Password=95702314;Database=TgYandex");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }

    public class TgBotDbContextFactory : IDesignTimeDbContextFactory<TgBotDbContext>
    {
        public TgBotDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TgBotDbContext>();
            optionsBuilder.UseNpgsql("User ID = postgres; Password = 95702314; host = localhost; port = 5432; Database = TgYandex;");

            return new TgBotDbContext(optionsBuilder.Options);
        }
    }
}
