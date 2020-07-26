using System;
using cobaef.Models;
using Microsoft.EntityFrameworkCore;

namespace cobaef
{
    class Program
    {
        static void Main(string[] args)
        {

            //Contoh01SatuEntity.coba();
            //Contoh02OneToMany.coba();
            //Contoh03ManyToMany.coba();
            Contoh04OneToOne.coba();
            Console.WriteLine("Hello World!");
        }
    }
}
