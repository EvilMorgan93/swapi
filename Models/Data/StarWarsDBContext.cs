using System;
using ApiLibrary;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace StarWarsApiApp.Models.Data
{
    public partial class StarWarsDBContext : DbContext
    {
        public StarWarsDBContext()
        {
        }

        public StarWarsDBContext(DbContextOptions<StarWarsDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<People> People { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-B4CMOIR;Database=StarWarsDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<People>(entity =>
            {
                entity.HasKey(e => e.PeopleId)
                    .HasName("People_pk")
                    .IsClustered(false);

                entity.ToTable("People", "sw");

                entity.Property(e => e.PeopleId).HasColumnName("people_id");

                entity.Property(e => e.Birth_Year)
                    .IsRequired()
                    .HasColumnName("Birth_Year")
                    .HasMaxLength(50);

                entity.Property(e => e.CurrentDate)
                    .HasColumnName("Current_Date")
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Height)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Mass)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
