using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace cobaef.Models
{

    public class PurchaseOrder
    {
        [Key]
        public Guid Id { get; set; }
        public string TransNo { get; set; }
        public DateTime? TransDate { get; set; }

    }

    public class AuditTrail
    {
        [Key]
        public Guid Id { get; set; }
        public string Action { get; set; }
        public string TableName { get; set; }
        public string PrimaryKey { get; set; }
        public string ColumnName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public DateTime Date { get; set; }
        public Guid UserId { get; set; }
    }

    public class Contoh05Context : DbContext
    {
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<AuditTrail> AuditTrails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connstring = "Server=localhost; Database=cobaef; User=sa; Password=Password_123; MultipleActiveResultSets=true";
            optionsBuilder.UseSqlServer(connstring);
        }



        // This is overridden to prevent someone from calling SaveChanges without specifying the user making the change
        public override int SaveChanges()
        {
            throw new InvalidOperationException("User ID must be provided");
        }

        public int SaveChanges(Guid userId)
        {
            Audit(ChangeTracker.Entries(), userId);

            // Call the original SaveChanges(), which will save both the changes made and the audit records
            return base.SaveChanges();
        }

        private void Audit(IEnumerable<EntityEntry> entries, Guid userId)
        {
            entries.Where(p => (p.State == EntityState.Added) || (p.State == EntityState.Modified) || (p.State == EntityState.Deleted)).ToList().ForEach(entry =>
            {
                Audit(entry, userId);
            });
        }

        private void Audit(EntityEntry entry, Guid userId)
        {

            var tableName = entry.Metadata.GetTableName();
            var pk = GetPrimaryKeys(entry);
            if (entry.State == EntityState.Added)
            {
                foreach (var property in entry.Properties)
                {
                    var auditEntry = new AuditTrail
                    {
                        Id = Guid.NewGuid(),
                        Action = "I",
                        TableName = tableName,
                        PrimaryKey = pk,
                        ColumnName = property.Metadata.GetColumnName(),
                        OldValue = (property.OriginalValue == null) ? null : property.OriginalValue.ToString(),
                        NewValue = (property.CurrentValue == null) ? null : property.CurrentValue.ToString(),
                        Date = DateTime.Now,
                        UserId = userId
                    };
                    this.AuditTrails.Add(auditEntry);
                }
            }
            else if (entry.State == EntityState.Modified)
            {
                foreach (var property in entry.Properties.Where(p => (p.IsModified == true)))
                {
                    var auditEntry = new AuditTrail
                    {
                        Id = Guid.NewGuid(),
                        Action = "M",
                        TableName = tableName,
                        PrimaryKey = pk,
                        ColumnName = property.Metadata.GetColumnName(),
                        OldValue = (property.OriginalValue == null) ? null : property.OriginalValue.ToString(),
                        NewValue = (property.CurrentValue == null) ? null : property.CurrentValue.ToString(),
                        Date = DateTime.Now,
                        UserId = userId
                    };
                    this.AuditTrails.Add(auditEntry);
                }
            }
            else if (entry.State == EntityState.Deleted)
            {
                foreach (var property in entry.Properties)
                {
                    var auditEntry = new AuditTrail
                    {
                        Id = Guid.NewGuid(),
                        Action = "D",
                        TableName = tableName,
                        PrimaryKey = pk,
                        ColumnName = property.Metadata.GetColumnName(),
                        OldValue = (property.OriginalValue == null) ? null : property.OriginalValue.ToString(),
                        NewValue = (property.CurrentValue == null) ? null : property.CurrentValue.ToString(),
                        Date = DateTime.Now,
                        UserId = userId
                    };
                    this.AuditTrails.Add(auditEntry);
                }
            }

        }

        private string GetPrimaryKeys(EntityEntry entry)
        {
            string pk = string.Empty;
            foreach (var property in entry.Metadata.FindPrimaryKey().Properties)
            {
                var prop = entry.Properties.Where(p => (p.Metadata.Name == property.Name)).First();
                pk += string.Format("{0}={1};", property.GetColumnName(), prop.CurrentValue.ToString());
            }
            return pk;

            //PAKAI DI BAWAH INI JG BISA
            //string pk = string.Empty;
            //foreach (PropertyEntry property in entry.Properties.Where(p => (p.Metadata.IsPrimaryKey() == true)))
            //{
            //    if (property.Metadata.IsPrimaryKey())
            //    {
            //        pk += string.Format("{0}={1};", property.Metadata.GetColumnName(), property.CurrentValue.ToString());
            //    }
            //}

            //return pk;

        }


    }



}