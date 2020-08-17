using System;
using System.Collections.Generic;
using cobaef.Models;
using System.Linq;

namespace cobaef
{
    public static class Contoh06Linq
    {


        public static void coba()
        {
            //cobaInsert(); //kalo data belum ada insert dulu ya
            cobaQuery();

        }

        public static void cobaQuery()
        {
            var db = new Contoh06Context();

            //001 Query biasa
            /*
                SELECT T0.*
                FROM Invoices T0 
            */
            var query001 = from T0 in db.Invoices
                           select T0
                      ;
            foreach (var invoice in query001)
            {
                Console.WriteLine($"001 -> {invoice.TransNo}");
            }

            //002 TOP N
            /*
                SELECT TOP 1  T0.*
                FROM Invoices T0 
            */
            var query002 = (from T0 in db.Invoices
                            select T0
                           ).Take(1)
                      ;
            foreach (var invoice in query002)
            {
                Console.WriteLine($"002 -> {invoice.TransNo}");
            }


            //003 ORDER BY  
            /*
                SELECT T0.*
                FROM Invoices T0
                ORDER BY T0.TransNo ASC
            */
            var query003 = from T0 in db.Invoices
                           orderby T0.TransNo ascending  //descending
                           select T0
                      ;

            foreach (var invoice in query003)
            {
                Console.WriteLine($"003 -> {invoice.TransNo}");
            }

            //004 WHERE 
            /*
                SELECT T0.*
                FROM Invoices T0
                WHERE T0.TransNo='trans02'
            */
            var query004 = from T0 in db.Invoices
                           where T0.TransNo == "trans02"
                           select T0
                      ;

            foreach (var invoice in query004)
            {
                Console.WriteLine($"004 -> {invoice.TransNo}");
            }


            //005 TOP-ORDER-WHERE 
            /*
                SELECT TOP 1 T0.*
                FROM Invoices T0
                WHERE T0.TransNo='trans02'
                ORDER BY T0.TransNo ASC

            */
            var query005 = (from T0 in db.Invoices
                            orderby T0.TransNo ascending
                            where T0.TransNo == "trans02"
                            select T0
                           ).Take(1)
            ;

            foreach (var invoice in query005)
            {
                Console.WriteLine($"005 -> {invoice.TransNo}");
            }

            //006 Custom Column name
            /*
                SELECT TOP 1 T0.TransNo AS NomerInvoice 
                FROM Invoices T0
                WHERE T0.TransNo='trans02'
                ORDER BY T0.TransNo ASC

            */
            var query006 = (from T0 in db.Invoices
                            orderby T0.TransNo ascending
                            where T0.TransNo == "trans02"
                            select new
                            {
                                NomerInvoice = T0.TransNo
                            }
                           ).Take(1)
            ;

            foreach (var invoice in query006)
            {
                Console.WriteLine($"006 -> {invoice.NomerInvoice}");
            }

            //007 INNER JOIN 
            /*
                SELECT TOP 1  T0.TransNo AS NomerInvoice, T1.ItemCode AS KodeBarang 
                FROM Invoices T0
                INNER JOIN InvoiceItems T1 ON T0.Id=T1.Id 
                WHERE TransNo='trans02'
                ORDER BY TransNo ASC

            */
            var query007 = (from T0 in db.Invoices
                            join T1 in db.InvoiceItems on T0.Id equals T1.InvoiceId
                            orderby T0.TransNo ascending
                            where T0.TransNo == "trans02"
                            select new
                            {
                                NomerInvoice = T0.TransNo,
                                KodeBarang = T1.ItemCode
                            }
                           ).Take(1)
            ;

            foreach (var invoice in query007)
            {
                Console.WriteLine($"007 -> {invoice.NomerInvoice}-{invoice.KodeBarang}");
            }

            //008 LEFT JOIN -- RIGHT JOIN (tidak mendukung dan kudu di akalin dengan left join
            /*
                SELECT TOP 1  T0.TransNo AS NomerInvoice, T1.ItemCode AS KodeBarang 
                FROM Invoices T0
                INNER JOIN InvoiceItems T1 ON T0.Id=T1.Id 
                WHERE TransNo='trans02'
                ORDER BY TransNo ASC

            */
            var query008 = (from T0 in db.Invoices
                            join T1 in db.InvoiceItems on T0.Id equals T1.InvoiceId into J01
                            from T1_0 in J01.DefaultIfEmpty()
                            orderby T0.TransNo ascending
                            where T0.TransNo == "trans02"
                            select new
                            {
                                NomerInvoice = T0.TransNo,
                                KodeBarang = T1_0.ItemCode
                            }
                           ).Take(1)
            ;

            foreach (var invoice in query008)
            {
                Console.WriteLine($"008 -> {invoice.NomerInvoice}-{invoice.KodeBarang}");
            }

            //009 CROSS JOIN
            /*
                SELECT TOP 1000  T0.TransNo AS NomerInvoice, T1.ItemCode AS KodeBarang 
                FROM Invoices T0
                CROSS JOIN InvoiceItems T1  
                WHERE TransNo='trans02'
                ORDER BY TransNo ASC

            */
            var query009 = (from T0 in db.Invoices
                            from T1 in db.InvoiceItems
                            orderby T0.TransNo ascending
                            where T0.TransNo == "trans02"
                            select new
                            {
                                NomerInvoice = T0.TransNo,
                                KodeBarang = T1.ItemCode
                            }
                           ).Take(1000)
            ;

            foreach (var invoice in query009)
            {
                Console.WriteLine($"009 -> {invoice.NomerInvoice}-{invoice.KodeBarang}");
            }


            //010 GROUP BY dengan satu table
            /*
                SELECT T0.ItemCode AS KodeBarang , SUM(T0.Qty) AS TotalQty 
                FROM InvoiceItems T0 
                GROUP BY T0.ItemCode

            */
            var query010 = from T0 in db.InvoiceItems
                           group T0 by T0.ItemCode into GRP
                           select new
                           {
                               KodeBarang = GRP.Key,
                               TotalQty = GRP.Sum(x => x.Qty)
                           }
            ;

            foreach (var invoice in query010)
            {
                Console.WriteLine($"010 -> {invoice.KodeBarang}-{invoice.TotalQty}");
            }

            //011 GROUP BY dengan inner join
            /*
                SELECT  T0.TransNo AS NomerInvoice, T1.ItemCode AS KodeBarang, SUM(T1.Qty) AS TotalQty 
                FROM Invoices T0
                INNER JOIN InvoiceItems T1 ON T0.Id=T1.Id 
                GROUP BY T0.TransNo, T1.ItemCode 

            */
            var query011 = from T0 in db.Invoices
                           join T1 in db.InvoiceItems on T0.Id equals T1.InvoiceId
                           group new { T0, T1 } by new { T0.TransNo, T1.ItemCode } into GRP
                           select new
                           {
                               NomerInvoice = GRP.Key.TransNo,
                               KodeBarang = GRP.Key.ItemCode,
                               TotalQty = GRP.Sum(x => x.T1.Qty)
                           }
            ;

            foreach (var invoice in query011)
            {
                Console.WriteLine($"011 -> {invoice.NomerInvoice}-{invoice.KodeBarang}-{invoice.TotalQty}");
            }

            //012 HAVING
            /*
                SELECT  T0.TransNo AS NomerInvoice, T1.ItemCode AS KodeBarang, SUM(T0.Qty) AS TotalQty 
                FROM Invoices T0
                INNER JOIN InvoiceItems T1 ON T0.Id=T1.Id 
                GROUP BY T0.TransNo, T1.ItemCode
                HAVING  SUM(T0.Qty)>2

            */
            var query012 = from T0 in db.Invoices
                           join T1 in db.InvoiceItems on T0.Id equals T1.InvoiceId
                           group new { T0, T1 } by new { T0.TransNo, T1.ItemCode } into GRP
                           where GRP.Sum(x => x.T1.Qty) > 2
                           select new
                           {
                               NomerInvoice = GRP.Key.TransNo,
                               KodeBarang = GRP.Key.ItemCode,
                               TotalQty = GRP.Sum(x => x.T1.Qty)
                           }
            ;

            foreach (var invoice in query012)
            {
                Console.WriteLine($"012 -> {invoice.NomerInvoice}-{invoice.KodeBarang}-{invoice.TotalQty}");
            }

            //013 HAVING--WHERE--ORDERBY
            /*
                SELECT  T0.TransNo AS NomerInvoice, T1.ItemCode AS KodeBarang, SUM(T1.Qty) AS TotalQty 
                FROM Invoices T0
                INNER JOIN InvoiceItems T1 ON T0.Id=T1.Id
                WHERE T0.TransNo='trans02' 
                GROUP BY T0.TransNo, T1.ItemCode
                HAVING  SUM(T1.Qty)>2
                ORDER BY T1.ItemCode DESC

            */
            var query013 = (
                           from T0 in db.Invoices
                           join T1 in db.InvoiceItems on T0.Id equals T1.InvoiceId
                           where T0.TransNo == "trans02"
                           group new { T0, T1 } by new { T0.TransNo, T1.ItemCode } into GRP
                           where GRP.Sum(x => x.T1.Qty) > 2
                           orderby GRP.Key.ItemCode descending
                           select new
                           {
                               NomerInvoice = GRP.Key.TransNo,
                               KodeBarang = GRP.Key.ItemCode,
                               TotalQty = GRP.Sum(x => x.T1.Qty)
                           } 
                           ).Take(2)
                            //kalau di taruh di sini akan jadi sub query
                            //.OrderBy(p => p.KodeBarang )   
                            //.OrderByDescending(p => p.KodeBarang)
            ;

            //foreach (var invoice in query013)
            //{
            //    Console.WriteLine($"013 -> {invoice.NomerInvoice}-{invoice.KodeBarang}-{invoice.TotalQty}");
            //}

            //013 HAVING--WHERE--ORDERBY--CASE
            /*
                SELECT  T0.TransNo AS NomerInvoice, T1.ItemCode AS KodeBarang, SUM(CASE WHEN T1.Qty<1 THEN 0 ELSE T1.Qty END) AS TotalQty 
                FROM Invoices T0
                INNER JOIN InvoiceItems T1 ON T0.Id=T1.Id
                WHERE T0.TransNo='trans02' 
                GROUP BY T0.TransNo, T1.ItemCode
                HAVING  SUM(CASE WHEN T1.Qty<1 THEN 0 ELSE T1.Qty END)>0
                ORDER BY T1.ItemCode DESC

            */
            var query014 = (
                           from T0 in db.Invoices
                           join T1 in db.InvoiceItems on T0.Id equals T1.InvoiceId
                           where T0.TransNo == "trans02"
                           group new { T0, T1 } by new { T0.TransNo, T1.ItemCode } into GRP
                           where GRP.Sum(x => x.T1.Qty < 2 ? 0 : x.T1.Qty) > 0
                           orderby GRP.Key.ItemCode descending
                           select new
                           {
                               NomerInvoice = GRP.Key.TransNo,
                               KodeBarang = GRP.Key.ItemCode,
                               TotalQty = GRP.Sum(x => x.T1.Qty < 2 ? 0 : x.T1.Qty)
                           }

                           ).Take(2)
            ;

            foreach (var invoice in query014)
            {
                Console.WriteLine($"014 -> {invoice.NomerInvoice}-{invoice.KodeBarang}-{invoice.TotalQty}");
            }

            Console.WriteLine("END YA");

        }

        public static void cobaInsert()
        {

            //INSERT
            using (var context = new Contoh06Context())
            {
                var std = new Invoice()
                {
                    TransNo = "trans01",
                    TransDate = DateTime.Now,
                    Items = new List<InvoiceItem>()
                    {
                        new InvoiceItem
                        {
                            ItemCode="item01",
                            Qty=1
                        },
                        new InvoiceItem
                        {
                            ItemCode="item02",
                            Qty=2
                        }
                    }
                };
                context.Invoices.Add(std);

                var std2 = new Invoice()
                {
                    TransNo = "trans02",
                    TransDate = DateTime.Now,
                    Items = new List<InvoiceItem>()
                    {
                        new InvoiceItem
                        {
                            ItemCode="item01",
                            Qty=3
                        },
                        new InvoiceItem
                        {
                            ItemCode="item02",
                            Qty=4
                        }
                    }
                };
                context.Invoices.Add(std2);

                context.SaveChanges();

            }

        }



    }
}
