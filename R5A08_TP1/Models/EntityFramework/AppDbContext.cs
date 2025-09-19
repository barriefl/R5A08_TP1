using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using R5A08_TP1.Models.EntityFramework;
using System;
using System.Collections.Generic;

namespace R5A08_TP1.Models.EntityFramework;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public virtual DbSet<Brand> Brands { get; set; }
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<ProductType> ProductsTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Server=localhost;port=5432;Database=ProduitsDB;uid=postgres;password=postgres;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("public");

        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(brand => brand.IdBrand).HasName("brand_pkey");

            entity.HasMany(brand => brand.RelatedProductsBrand)
                .WithOne(product => product.BrandNavigation)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(product => product.IdProduct).HasName("product_pkey");

            entity.HasOne(product => product.BrandNavigation)
                .WithMany(brand => brand.RelatedProductsBrand)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_products_brand");

            entity.HasOne(product => product.ProductTypeNavigation)
                .WithMany(productType => productType.RelatedProductsProductType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_products_product_type");
        });

        modelBuilder.Entity<ProductType>(entity =>
        {
            entity.HasKey(productType => productType.IdProductType).HasName("producttype_pkey");

            entity.HasMany(productType => productType.RelatedProductsProductType)
                .WithOne(product => product.ProductTypeNavigation)
                .OnDelete(DeleteBehavior.ClientSetNull);        
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}