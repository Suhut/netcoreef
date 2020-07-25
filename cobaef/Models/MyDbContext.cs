using Microsoft.EntityFrameworkCore;  
using System.ComponentModel.DataAnnotations;

namespace cobaef.Models
{
    public class Customer
    {
        [Key]
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
    }

    public class MyDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connstring = "Server=localhost; Database=cobaef; User=sa; Password=Password_123; MultipleActiveResultSets=true";
            optionsBuilder.UseSqlServer(connstring);
        }

        public DbSet<Customer> Customers { get; set; }
    }
}