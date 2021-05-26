using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace ServerBackend.Models
{
    public partial class WoWGuildContext : DbContext
    {
        
                
        public WoWGuildContext()
        {
        }

        public WoWGuildContext(DbContextOptions<WoWGuildContext> options)
            : base(options)
        {
        }

        public virtual DbSet<PlayableClass> PlayableClasses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .AddUserSecrets<Program>()
                .Build();

            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = configuration["ConnectionStrings:WoWGuildWebsite"];
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<PlayableClass>(entity =>
            {
                entity.ToTable("PlayableClass", "api");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
