using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace ServerBackend
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
        public virtual DbSet<PlayableRace> PlayableRaces { get; set; }
        public virtual DbSet<Realm> Realms { get; set; }

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
            modelBuilder.HasDefaultSchema("api");
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");
            
            
            //Specify tables that shouldn't have the ID column be Identity because we want the value from the API
            #region "Identity Specifications"
            modelBuilder.Entity<PlayableClass>(entity =>
            {
                entity.ToTable("PlayableClass", "api");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");
            });
            
            modelBuilder.Entity<PlayableRace>(entity =>
            {
                entity.ToTable("PlayableRace", "api");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");
            });

            modelBuilder.Entity<Realm>(entity =>
            {
                entity.ToTable("Realm", "api");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");
            });
            #endregion

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
