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
            //    SELECT *
            //    FROM Invoice  
            //*/
            //var query001 = from invoice in db.Invoices 
            //            select invoice 
            //          ; 
            //foreach (var invoice in query001)
            //{
            //    Console.WriteLine($"{invoice.TransNo}");
            //}

            ////002 TOP N
            ///*
            //    SELECT TOP 1  *
            //    FROM Invoice  
            //*/
            //var query002 = (from invoice in db.Invoices
            //               select invoice
            //               ).Take(1)
            //          ;
            //foreach (var invoice in query002)
            //{
            //    Console.WriteLine($"{invoice.TransNo}");
            //}


            ////003 TOP N
            ///*
            //    SELECT TOP 1  *
            //    FROM Invoice  
            //*/
            //var query003 = (from invoice in db.Invoices
            //                select invoice
            //               ).Take(1)
            //          ;
            //foreach (var invoice in query003)
            //{
            //    Console.WriteLine($"{invoice.TransNo}");
            //}

            ////004 ORDER BY  
            ///*
            //    SELECT *
            //    FROM Invoice
            //    ORDER BY TransNo ASC
            //*/
            //var query004 = from invoice in db.Invoices
            //                orderby invoice.TransNo ascending //descending
            //                select invoice  
            //          ;

            //foreach (var invoice in query004)
            //{
            //    Console.WriteLine($"{invoice.TransNo}");
            //}

            ////005 WHERE 
            ///*
            //    SELECT *
            //    FROM Invoice
            //    WHERE TransNo='trans02'
            //*/
            //var query005 = from invoice in db.Invoices
            //               where invoice.TransNo == "trans02"
            //               select invoice
            //          ;

            //foreach (var invoice in query005)
            //{
            //    Console.WriteLine($"{invoice.TransNo}");
            //}


            ////006 TOP-ORDER-WHERE 
            ///*
            //    SELECT TOP 1  *
            //    FROM Invoice
            //    WHERE TransNo='trans02'
            //    ORDER BY TransNo ASC

            //*/
            //var query007 = (from invoice in db.Invoices
            //                orderby invoice.TransNo ascending
            //                where invoice.TransNo == "trans02"
            //                select invoice
            //               ).Take(1);
            //          ;

            //foreach (var invoice in query007)
            //{
            //    Console.WriteLine($"{invoice.TransNo}");
            //}

            //008 INNER JOIN 
            /*
                SELECT TOP 1  *
                FROM Invoice
                WHERE TransNo='trans02'
                ORDER BY TransNo ASC

            */
            //var query008 = (from invoice in db.Invoices
            //                orderby invoice.TransNo ascending
            //                where invoice.TransNo == "trans02"
            //                select invoice
            //               ).Take(1);
            //;

            //foreach (var invoice in query008)
            //{
            //    Console.WriteLine($"{invoice.TransNo}");
            //}

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
