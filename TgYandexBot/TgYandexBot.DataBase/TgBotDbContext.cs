using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using TgYandexBot.Core.Models;
using TgYandexBot.DataBase.Configurations;

namespace TgYandexBot.DataBase
{
    public class TgBotDbContext(DbContextOptions<TgBotDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseNpgsql("Host=localhost;Port=54322;Username=postgres;Password=postgres;Database=TgYandex");
        //}

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
            optionsBuilder.UseNpgsql("User ID = postgres; Password = postgres; host = localhost; port = 54322; Database = TgYandex;");

            return new TgBotDbContext(optionsBuilder.Options);
        }
    }
}
