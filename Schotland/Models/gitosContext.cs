using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Schotland.Models
{
    public partial class gitosContext : DbContext
    {
        public virtual DbSet<Places> Places { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        public gitosContext(DbContextOptions<gitosContext> options)
           : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Places>(entity =>
            {
                entity.ToTable("places");

                entity.Property(e => e.Info)
                    .HasColumnName("info")
                    .HasColumnType("text");

                entity.Property(e => e.Lat).HasColumnName("lat");

                entity.Property(e => e.Lon).HasColumnName("lon");

                entity.Property(e => e.Picture).HasColumnName("picture");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Password).HasColumnName("password");

                entity.Property(e => e.Username).HasColumnName("username");
            });
        }
    }
}
