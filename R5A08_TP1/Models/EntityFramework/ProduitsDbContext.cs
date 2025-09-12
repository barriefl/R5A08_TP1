using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using R5A08_TP1.Models.EntityFramework;
using System;
using System.Collections.Generic;

namespace R5A08_TP1.Models.EntityFramework;

public partial class ProduitsDbContext : DbContext
{
    public ProduitsDbContext()
    {
    }

    public ProduitsDbContext(DbContextOptions<ProduitsDbContext> options) : base(options)
    {
    }

    public virtual DbSet<Marque> Marques { get; set; }
    public virtual DbSet<Produit> Produits { get; set; }
    public virtual DbSet<TypeProduit> TypesProduits { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Server=localhost;port=5432;Database=ProduitsDB; uid=postgres;password=postgres;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("public");

        modelBuilder.Entity<Marque>(entity =>
        {
            entity.HasKey(m => m.IdMarque).HasName("marque_pkey");

            entity.HasMany(m => m.ProduitsAssociesMarques)
                .WithOne(p => p.MarqueNavigation)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Produit>(entity =>
        {
            entity.HasKey(p => p.IdProduit).HasName("produit_pkey");

            entity.HasOne(p => p.MarqueNavigation)
                .WithMany(m => m.ProduitsAssociesMarques)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_produits_marque");

            entity.HasOne(p => p.TypeProduitNavigation)
                .WithMany(tp => tp.ProduitsAssociesTypesProduit)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_produits_type_produit");
        });

        modelBuilder.Entity<TypeProduit>(entity =>
        {
            entity.HasKey(e => e.IdTypeProduit).HasName("typeproduit_pkey");

            entity.HasMany(tp => tp.ProduitsAssociesTypesProduit)
                .WithOne(p => p.TypeProduitNavigation)
                .OnDelete(DeleteBehavior.ClientSetNull);        
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}