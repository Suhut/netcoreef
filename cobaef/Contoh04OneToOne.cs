using System;
using System.Collections.Generic;
using cobaef.Models;
using Microsoft.EntityFrameworkCore;

namespace cobaef
{
    public static class Contoh04OneToOne
    {
        public static void coba()
        {
            using (var context = new Contoh04Context())
            {
                var std = new Supplier()
                {
                    SupplierCode = "Supp01",
                    SupplierName = "Supp01 Name",
                    Address = new SupplierAddress()
                    {
                        Street = "Jalan mantap"

                    }
                };
                context.Suppliers.Add(std);

                context.SaveChanges();

            }

        }
    }
}
