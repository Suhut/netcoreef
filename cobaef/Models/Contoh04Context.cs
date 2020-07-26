using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace cobaef.Models
{

    public class Supplier
    {
        [Key]
        public string SupplierCode { get; set; }
        public string SupplierName { get; set; }
        public SupplierAddress  Address { get; set; }
    }

    public class SupplierAddress
    {
        [Key]
        public int Id { get; set; }
        public string AddressName { get; set; }
        public string Street { get; set; }
        public string SupplierCode { get; set; }
        public Supplier Supplier { get; set; }
    }
    
    public class Contoh04Context : DbContext
    {
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<SupplierAddress> SupplierAddresses { get; set; } 

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connstring = "Server=localhost; Database=cobaef; User=sa; Password=Password_123; MultipleActiveResultSets=true";
            optionsBuilder.UseSqlServer(connstring);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 

            modelBuilder.Entity<Supplier>()
                .HasOne(bc => bc.Address)
                .WithOne(b => b.Supplier)
                .HasForeignKey<SupplierAddress>(bc => bc.SupplierCode); 
        }

    }


}