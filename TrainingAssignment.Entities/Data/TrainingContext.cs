using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TrainingAssignment.Entities.Models;

namespace TrainingAssignment.Entities.Data;

public partial class TrainingContext : DbContext
{
    public TrainingContext()
    {
    }

    public TrainingContext(DbContextOptions<TrainingContext> options)
        : base(options)
    {
    }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Gender> Genders { get; set; }

    public virtual DbSet<State> States { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=PCA118\\SQL2017;DataBase=Training;User ID=sa;Password=Tatva@123;encrypt=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.CityId).HasName("PK__city__B4BEB95E61D1A79E");

            entity.ToTable("city");

            entity.Property(e => e.CityId)
                .ValueGeneratedOnAdd()
                .HasColumnName("cityId");
            entity.Property(e => e.CityName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("cityName");
            entity.Property(e => e.CountryId).HasColumnName("countryId");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("createdAt");
            entity.Property(e => e.StateId).HasColumnName("stateId");

            entity.HasOne(d => d.Country).WithMany(p => p.Cities)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("FK__city__countryId__4BAC3F29");

            entity.HasOne(d => d.State).WithMany(p => p.Cities)
                .HasForeignKey(d => d.StateId)
                .HasConstraintName("FK__city__stateId__70DDC3D8");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.CountryId).HasName("PK__country__D32076BC1A1AFF82");

            entity.ToTable("country");

            entity.Property(e => e.CountryId)
                .ValueGeneratedOnAdd()
                .HasColumnName("countryId");
            entity.Property(e => e.CountryName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("countryName");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("createdAt");
        });

        modelBuilder.Entity<Gender>(entity =>
        {
            entity.HasKey(e => e.GenderId).HasName("PK__gender__306E22406BFF2EF8");

            entity.ToTable("gender");

            entity.Property(e => e.GenderId)
                .ValueGeneratedOnAdd()
                .HasColumnName("genderId");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("createdAt");
            entity.Property(e => e.Gender1)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("gender");
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.HasKey(e => e.StateId).HasName("PK__states__A667B9E1FA29E59D");

            entity.ToTable("states");

            entity.Property(e => e.StateId)
                .ValueGeneratedOnAdd()
                .HasColumnName("stateId");
            entity.Property(e => e.CountryId).HasColumnName("countryId");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("createdAt");
            entity.Property(e => e.Statename)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("statename");

            entity.HasOne(d => d.Country).WithMany(p => p.States)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("FK__states__countryI__6FE99F9F");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Userid).HasName("PK__users__CBA1B2576D5AB13B");

            entity.ToTable("users");

            entity.Property(e => e.Userid).HasColumnName("userid");
            entity.Property(e => e.Avatar)
                .IsUnicode(false)
                .HasColumnName("avatar");
            entity.Property(e => e.CityId).HasColumnName("cityId");
            entity.Property(e => e.CountryId).HasColumnName("countryId");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Gender).HasColumnName("gender");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Phonenumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("phonenumber");
            entity.Property(e => e.Role)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("role");
            entity.Property(e => e.StateId).HasColumnName("stateId");
            entity.Property(e => e.Surname)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("surname");
            entity.Property(e => e.Username)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("username");

            entity.HasOne(d => d.City).WithMany(p => p.Users)
                .HasForeignKey(d => d.CityId)
                .HasConstraintName("FK__users__cityId__75A278F5");

            entity.HasOne(d => d.Country).WithMany(p => p.Users)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("FK__users__countryId__74AE54BC");

            entity.HasOne(d => d.GenderNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.Gender)
                .HasConstraintName("FK__users__gender__73BA3083");

            entity.HasOne(d => d.State).WithMany(p => p.Users)
                .HasForeignKey(d => d.StateId)
                .HasConstraintName("FK__users__stateId__76969D2E");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
