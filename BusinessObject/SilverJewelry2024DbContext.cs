using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BusinessObject;

public partial class SilverJewelry2024DbContext : DbContext
{
    public SilverJewelry2024DbContext()
    {
    }

    public SilverJewelry2024DbContext(DbContextOptions<SilverJewelry2024DbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BranchAccount> BranchAccounts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<SilverJewelry> SilverJewelries { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(GetConnectionString());


    private string GetConnectionString()
    {
        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .Build();
        var strConn = config.GetConnectionString("DefaultConnection");
        return strConn;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BranchAccount>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__BranchAc__349DA586594E6CF1");

            entity.ToTable("BranchAccount");

            entity.HasIndex(e => e.EmailAddress, "UQ__BranchAc__49A14740928503E3").IsUnique();

            entity.Property(e => e.AccountId)
                .ValueGeneratedNever()
                .HasColumnName("AccountID");
            entity.Property(e => e.AccountPassword).HasMaxLength(40);
            entity.Property(e => e.EmailAddress).HasMaxLength(60);
            entity.Property(e => e.FullName).HasMaxLength(60);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Category__19093A0B5AB01FC6");

            entity.ToTable("Category");

            entity.Property(e => e.CategoryId).HasMaxLength(30);
            entity.Property(e => e.CategoryDescription).HasMaxLength(250);
            entity.Property(e => e.CategoryName).HasMaxLength(100);
            entity.Property(e => e.FromCountry).HasMaxLength(160);
        });

        modelBuilder.Entity<SilverJewelry>(entity =>
        {
            entity.HasKey(e => e.SilverJewelryId).HasName("PK__SilverJe__1F127197699419C7");

            entity.ToTable("SilverJewelry");

            entity.Property(e => e.SilverJewelryId).HasMaxLength(200);
            entity.Property(e => e.CategoryId).HasMaxLength(30);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.MetalWeight).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.SilverJewelryDescription).HasMaxLength(250);
            entity.Property(e => e.SilverJewelryName).HasMaxLength(100);

            entity.HasOne(d => d.Category).WithMany(p => p.SilverJewelries)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__SilverJew__Categ__3C69FB99");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
