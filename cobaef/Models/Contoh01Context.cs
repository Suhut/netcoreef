using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace cobaef.Models
{
    //Satu Entity Saja
    public class Customer
    {
        [Key]
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
    } 

    public class Contoh01Context : DbContext
    {
        
        public DbSet<Customer> Customers { get; set; } 

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connstring = "Server=localhost; Database=cobaef; User=sa; Password=Password_123; MultipleActiveResultSets=true";
            optionsBuilder.UseSqlServer(connstring);
        } 
       
    }


}