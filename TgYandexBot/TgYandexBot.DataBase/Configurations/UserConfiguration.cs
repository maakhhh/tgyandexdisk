using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TgYandexBot.Core.Models;

namespace TgYandexBot.DataBase.Configurations
{
	public class UserConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).ValueGeneratedOnAdd();

			builder.Property(x => x.AccessToken).HasColumnType("text");
			builder.Property(x => x.CreatedAt).HasColumnType("timestamptz");
			builder.Property(x => x.UpdatedAt).HasColumnType("timestamptz");
		}
	}
}
