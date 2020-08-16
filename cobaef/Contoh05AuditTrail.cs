using System;
using System.Collections.Generic;
using cobaef.Models;
using Microsoft.EntityFrameworkCore;

namespace cobaef
{
    public static class Contoh05AuditTrail
    {
        public static void coba()
        {
            //INSERT
            using (var context = new Contoh05Context())
            {
                var std = new PurchaseOrder()
                {
                    Id = Guid.NewGuid(),
                    TransNo = "Trans02",
                    TransDate = DateTime.Now
                };
                context.PurchaseOrders.Add(std);

                
                context.SaveChanges(Guid.NewGuid());

            }

        }
    }
}
