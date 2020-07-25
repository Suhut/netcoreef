﻿using System;
using cobaef.Models;
using Microsoft.EntityFrameworkCore;

namespace cobaef
{
    public static class SatuEntity
    {
        public static void coba()
        {
            var db = new MyDbContext();

            //-------------------------
            //SATU TABLE SAJA
            //-------------------------
            //INSERT
            using (var context = new MyDbContext())
            {
                var std = new Customer()
                {
                    CustomerCode = "cust001",
                    CustomerName = "cust001 name"
                };
                context.Customers.Add(std);
                context.SaveChanges();

            }

            //UPDATE
            using (var context = new MyDbContext())
            {
                var std = new Customer()
                {
                    CustomerCode = "cust001",
                    CustomerName = "cust001 name update ya"
                };
                context.Customers.Update(std);
                context.SaveChanges();

            }

            // DELETE
            using (var context = new MyDbContext())
            {
                var std = new Customer()
                {
                    CustomerCode = "cust001",
                };
                context.Customers.Remove(std);
                context.SaveChanges();

            }

            //DELETE DENGAN CARA LAIN
            using (var context = new MyDbContext())
            {
                var std = new Customer()
                {
                    CustomerCode = "cust002",
                };
                context.Entry(std).State = EntityState.Deleted;
                context.SaveChanges();

            }
        }
    }
}
