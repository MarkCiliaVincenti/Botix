using System;
using Botix.Bot.Core.Domains;
using Microsoft.EntityFrameworkCore;

namespace Botix.Bot.Infrastructure.DataBase
{
    public class BotDbContext : DbContext
    {
        public BotDbContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<CallBackGroup> CallBackGroups { get; set; }

        public DbSet<CallBack> CallBacks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users", "api");

                entity.Property(x => x.Id)
                    .UseIdentityAlwaysColumn();

                entity.Property(x => x.Identifier)
                    .IsRequired();

                entity.Property(x => x.Username)
                    .IsRequired();

                entity.Property(x => x.CreatedAt)
                    .HasConversion(
                        dateTime => dateTime.ToUniversalTime(),
                        dateTime => DateTime.SpecifyKind(dateTime, DateTimeKind.Utc));

                entity.Property(x => x.UpdateAt)
                    .HasConversion(
                        dateTime => dateTime.ToUniversalTime(),
                        dateTime => DateTime.SpecifyKind(dateTime, DateTimeKind.Utc));
            });

            modelBuilder.Entity<CallBack>()
                .ToTable("CallBacks", "api")
                .HasOne(x => x.CallBackGroup)
                .WithMany(x => x.CallBacks)
                .HasForeignKey(x => x.CallBackGroupId);

            modelBuilder.Entity<CallBackGroup>()
                .ToTable("CallBackGroups", "api");
        }
    }
}
