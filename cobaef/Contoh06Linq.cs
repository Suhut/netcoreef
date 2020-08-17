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
            //cobaInsert();
            cobaQuery();

        }

        public static void cobaQuery()
        {
            var db = new Contoh06Context();

            ////001 Query biasa
            ///*
            //    SELECT T0.*
            //    FROM Invoices T0 
            //*/
            //var query001 = from T0 in db.Invoices
            //               select T0
            //          ;
            //foreach (var invoice in query001)
            //{
            //    Console.WriteLine($"001 -> {invoice.TransNo}");
            //}

            ////002 TOP N
            ///*
            //    SELECT TOP 1  T0.*
            //    FROM Invoices T0 
            //*/
            //var query002 = (from T0 in db.Invoices
            //                select T0
            //               ).Take(1)
            //          ;
            //foreach (var invoice in query002)
            //{
            //    Console.WriteLine($"002 -> {invoice.TransNo}");
            //}


            ////003 ORDER BY  
            ///*
            //    SELECT T0.*
            //    FROM Invoices T0
            //    ORDER BY T0.TransNo ASC
            //*/
            //var query003 = from T0 in db.Invoices
            //               orderby T0.TransNo ascending  //descending
            //               select T0
            //          ;

            //foreach (var invoice in query003)
            //{
            //    Console.WriteLine($"003 -> {invoice.TransNo}");
            //}

            ////004 WHERE 
            ///*
            //    SELECT T0.*
            //    FROM Invoices T0
            //    WHERE T0.TransNo='trans02'
            //*/
            //var query004 = from T0 in db.Invoices
            //               where T0.TransNo == "trans02"
            //               select T0
            //          ;

            //foreach (var invoice in query004)
            //{
            //    Console.WriteLine($"004 -> {invoice.TransNo}");
            //}


            ////005 TOP-ORDER-WHERE 
            ///*
            //    SELECT TOP 1 T0.*
            //    FROM Invoices T0
            //    WHERE T0.TransNo='trans02'
            //    ORDER BY T0.TransNo ASC

            //*/
            //var query005 = (from T0 in db.Invoices
            //                orderby T0.TransNo ascending
            //                where T0.TransNo == "trans02"
            //                select T0
            //               ).Take(1);
            //;

            //foreach (var invoice in query005)
            //{
            //    Console.WriteLine($"005 -> {invoice.TransNo}");
            //}

            ////006 Custom Column name
            ///*
            //    SELECT TOP 1 T0.TransNo AS NomerInvoice 
            //    FROM Invoices T0
            //    WHERE T0.TransNo='trans02'
            //    ORDER BY T0.TransNo ASC

            //*/
            //var query006 = (from T0 in db.Invoices
            //                orderby T0.TransNo ascending
            //                where T0.TransNo == "trans02"
            //                select new
            //                {
            //                    NomerInvoice=T0.TransNo
            //                }
            //               ).Take(1);
            //;

            //foreach (var invoice in query006)
            //{
            //    Console.WriteLine($"006 -> {invoice.NomerInvoice}");
            //}

            ////007 INNER JOIN 
            ///*
            //    SELECT TOP 1  T0.TransNo AS NomerInvoice, T1.ItemCode AS KodeBarang 
            //    FROM Invoices T0
            //    INNER JOIN InvoiceItems T1 ON T0.Id=T1.Id 
            //    WHERE TransNo='trans02'
            //    ORDER BY TransNo ASC

            //*/
            //var query007 = (from T0 in db.Invoices
            //                join T1 in db.InvoiceItems on T0.Id equals T1.Id
            //                orderby T0.TransNo ascending
            //                where T0.TransNo == "trans02"
            //                select new
            //                {
            //                    NomerInvoice = T0.TransNo,
            //                    KodeBarang=T1.ItemCode
            //                }
            //               ).Take(1);
            //;

            //foreach (var invoice in query007)
            //{
            //    Console.WriteLine($"007 -> {invoice.NomerInvoice}-{invoice.KodeBarang}");
            //}

            ////008 LEFT JOIN -- RIGHT JOIN (tidak mendukung dan kudu di akalin dengan left join
            ///*
            //    SELECT TOP 1  T0.TransNo AS NomerInvoice, T1.ItemCode AS KodeBarang 
            //    FROM Invoices T0
            //    INNER JOIN InvoiceItems T1 ON T0.Id=T1.Id 
            //    WHERE TransNo='trans02'
            //    ORDER BY TransNo ASC

            //*/
            //var query008 = (from T0 in db.Invoices
            //                join T1 in db.InvoiceItems on T0.Id equals T1.Id into J01
            //                from T1_0 in J01.DefaultIfEmpty()
            //                orderby T0.TransNo ascending
            //                where T0.TransNo == "trans02"
            //                select new
            //                {
            //                    NomerInvoice = T0.TransNo,
            //                    KodeBarang = T1_0.ItemCode
            //                }
            //               ).Take(1);
            //;

            //foreach (var invoice in query008)
            //{
            //    Console.WriteLine($"008 -> {invoice.NomerInvoice}-{invoice.KodeBarang}");
            //}

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
                           ).Take(1000);
            ;

            foreach (var invoice in query009)
            {
                Console.WriteLine($"009 -> {invoice.NomerInvoice}-{invoice.KodeBarang}");
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
