﻿using Microsoft.EntityFrameworkCore;
using AppControle.Shared.Entities;

namespace AppControle.API.Context;
public class DataContext : DbContext //IdentityDbContext<User>
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }

    //public DbSet<Country>? Countries { get; set; }
    //public DbSet<City>? Cities { get; set; }
    //public DbSet<State>? States { get; set; }
    //public DbSet<Client> Clients { get; set; }
    //public DbSet<ProductImage> ProductImages { get; set; }
    //public DbSet<ClientService> ClientService { get; set; }
    //public DbSet<MonthlyFee> MonthlyFee { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<ProductCategory>()
        //    .HasKey(cp => new { cp.ProductId, cp.CategoryId });

        //modelBuilder.Entity<ProductCategory>()
        //    .HasOne(cp => cp.Product)
        //    .WithMany(p => p.lProductCategories)
        //    .HasForeignKey(cp => cp.ProductId);

        //modelBuilder.Entity<ProductCategory>()
        //    .HasOne(cp => cp.Category)
        //    .WithMany(c => c.lProductCategories)
        //    .HasForeignKey(cp => cp.CategoryId);

        //modelBuilder.HasCharSet(null, DelegationModes.ApplyToDatabases);
        //base.OnModelCreating(modelBuilder);

        //modelBuilder.Entity<Country>().HasIndex(x => x.Name).IsUnique();
        //modelBuilder.Entity<State>().HasIndex("CountryId", "Name").IsUnique();
        //modelBuilder.Entity<City>().HasIndex("StateId", "Name").IsUnique();
        //modelBuilder.Entity<User>().HasIndex("Cpf_Cnpj").IsUnique();

        //modelBuilder.Entity<Client>()
        //    .HasAlternateKey(x => new { x.Cpf_Cnpj, x.UserId });
        //modelBuilder.Entity<Product>()
        //    .HasAlternateKey(x => new { x.Name, x.UserId });
        //modelBuilder.Entity<Category>()
        //    .HasAlternateKey(x => new { x.Name, x.UserId });



    }
}