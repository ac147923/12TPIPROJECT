using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12TPIPROJECT
{
    public class ConsoleView
    {
        public string DisplayMenu()
        {
            Console.WriteLine("Welcome to my Cricket Database");
            Console.WriteLine("Menu:");
            Console.WriteLine("1. View All records in Brand");

            return Console.ReadLine();

        }

        public void DisplayBrands(List<Brand> brands)
        {
            foreach (Brand brand in brands)
            {
                Console.WriteLine($"{brand.BrandID}, {brand.BrandName}");
            }
        }









    }
}
