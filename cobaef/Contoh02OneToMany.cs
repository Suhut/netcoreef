using System;
using System.Collections.Generic;
using cobaef.Models;
using Microsoft.EntityFrameworkCore;

namespace cobaef
{
    public static class Contoh02OneToMany
    {
        public static void coba()
        { 

            //INSERT
            using (var context = new Contoh02Context())
            {
                var std = new SalesOrder()
                {
                    TransNo = "trans01",
                    TransDate = DateTime.Now,
                    Items = new List<SalesOrderItem>()
                    {
                        new SalesOrderItem
                        {
                            ItemCode="item01",
                            Qty=1
                        },
                        new SalesOrderItem
                        {
                            ItemCode="item02",
                            Qty=2
                        }
                    }
                };
                context.SalesOrders.Add(std);
                context.SaveChanges();

            }

            //DELETE
            using (var context = new Contoh02Context())
            {
                var std = new SalesOrder()
                {
                    Id = 1
                };
                context.SalesOrders.Remove(std);
                context.SaveChanges();

            }
        }
    }
}
