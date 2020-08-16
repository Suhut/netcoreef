using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace cobaef.Models
{

    public class Invoice
    {
        [Key]
        public int Id { get; set; }
        public string TransNo { get; set; }
        public DateTime? TransDate { get; set; }
        public List<InvoiceItem> Items { get; set; }
    }

    public class InvoiceItem
    {
        [Key]
        public int Id { get; set; }
        public string ItemCode { get; set; }
        public int? Qty { get; set; }
        public Invoice Invoice { get; set; }
    }


    public class Contoh06Context : DbContext
    {
        //public static readonly ILoggerFactory MyLoggerFactory
        //            = LoggerFactory.Create(builder => { builder.AddConsole(); });

        public static readonly ILoggerFactory MyLoggerFactory
                = LoggerFactory.Create(builder =>
                {
                    builder
                        .AddFilter((category, level) =>
                            category == DbLoggerCategory.Database.Command.Name
                            && level == LogLevel.Information)
                        .AddConsole();
                });

        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connstring = "Server=localhost; Database=cobaef; User=sa; Password=Password_123; MultipleActiveResultSets=true";
            optionsBuilder
                .UseLoggerFactory(MyLoggerFactory) // Warning: Do not create a new ILoggerFactory instance each time
                .UseSqlServer(connstring);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InvoiceItem>()
               .HasOne(e => e.Invoice)
               .WithMany(c => c.Items)
               .OnDelete(DeleteBehavior.Cascade);
        }

    }


}