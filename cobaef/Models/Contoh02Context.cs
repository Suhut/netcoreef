using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace cobaef.Models
{  

    public class SalesOrder
    {
        [Key]
        public int Id { get; set; }
        public string TransNo { get; set; }
        public DateTime? TransDate { get; set; }
        public List<SalesOrderItem> Items { get; set; }
    }

    public class SalesOrderItem
    {
        [Key]
        public int Id { get; set; }
        public string ItemCode { get; set; }
        public int? Qty { get; set; }
        public SalesOrder SalesOrder { get; set; }
    }


    public class Contoh02Context : DbContext
    {  
        public DbSet<SalesOrder> SalesOrders { get; set; }
        public DbSet<SalesOrderItem> SalesOrderItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connstring = "Server=localhost; Database=cobaef; User=sa; Password=Password_123; MultipleActiveResultSets=true";
            optionsBuilder.UseSqlServer(connstring);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SalesOrderItem>()
               .HasOne(e => e.SalesOrder)
               .WithMany(c => c.Items)
               .OnDelete(DeleteBehavior.Cascade); 
        } 
       
    }


}