using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _12TPIPROJECT.models;

namespace _12TPIPROJECT.view
{
    public class ConsoleView
    {
        public string DisplayMenu()
        {
            Console.WriteLine("Welcome to my Cricket Database");
            Console.WriteLine("Menu:");
            Console.WriteLine("1. View All records in Brand");
            Console.WriteLine("2. Update a brand's name by brand_id");
            Console.WriteLine("3. Insert a new brand");
            Console.WriteLine("4. Delete a brand by brand_name");
            Console.WriteLine("Select an option: ");
            return Console.ReadLine();

        }

        public void DisplayBrands(List<Brand> brands)
        {
            foreach (Brand brand in brands)
            {
                Console.WriteLine($"{brand.BrandID}, {brand.BrandName}");
            }
        }

        public void DisplayMessage(string message)
        {
            Console.WriteLine(message);

        }

        public string GetInput()
        {
            return Console.ReadLine();

        }

        public int GetIntInput()
        {
            return int.Parse(Console.ReadLine());

        }









    }
}
