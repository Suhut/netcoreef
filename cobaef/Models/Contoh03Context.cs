using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace cobaef.Models
{

    public class Product
    {
        [Key]
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public ICollection<ProductUom> ProductUoms { get; set; }
    }

    public class Uom
    {
        [Key]
        public string UomCode { get; set; }
        public string UomName { get; set; }
        public ICollection<ProductUom> ProductUoms { get; set; }
    }

    public class ProductUom
    {
        public string ProductCode { get; set; }
        public Product Product { get; set; }
        public string UomCode { get; set; }
        public Uom Uom { get; set; }
    }

    public class Contoh03Context : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Uom> Uoms { get; set; }
        public DbSet<ProductUom> ProductUoms { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connstring = "Server=localhost; Database=cobaef; User=sa; Password=Password_123; MultipleActiveResultSets=true";
            optionsBuilder.UseSqlServer(connstring);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductUom>()
            .HasKey(bc => new { bc.ProductCode, bc.UomCode });

            modelBuilder.Entity<ProductUom>()
                .HasOne(bc => bc.Product)
                .WithMany(b => b.ProductUoms)
                .HasForeignKey(bc => bc.ProductCode);
            modelBuilder.Entity<ProductUom>()
                .HasOne(bc => bc.Uom)
                .WithMany(c => c.ProductUoms)
                .HasForeignKey(bc => bc.UomCode);
        }

    }


}