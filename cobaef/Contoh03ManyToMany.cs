using System;
using System.Collections.Generic;
using cobaef.Models;
using Microsoft.EntityFrameworkCore;

namespace cobaef
{
    public static class Contoh03ManyToMany
    {
        public static void coba()
        {

            //INSERT
            //using (var context = new Contoh03Context())
            //{
            //    var std = new List<Uom>{
            //        new Uom()
            //        {
            //            UomCode = "Uom01",
            //            UomName = "Uom01 Name"
            //        },
            //        new Uom()
            //        {
            //            UomCode = "Uom02",
            //            UomName = "Uom02 Name"
            //        }
            //    };
            //    context.Uoms.AddRange(std);

            //    context.SaveChanges();

            //}

            using (var context = new Contoh03Context())
            {
                var std = new Product()
                {
                    ProductCode = "Product01",
                    ProductName = "Product01 Name",
                    ProductUoms = new List<ProductUom>()
                    {
                        new ProductUom
                        {
                            UomCode="Uom01"
                        },
                        new ProductUom
                        {
                            UomCode="Uom02"
                        }
                    }
                };
                context.Products.Add(std);

                context.SaveChanges();

            }

        }
    }
}
