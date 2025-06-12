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
            Console.WriteLine("1. View All records in Country");
            Console.WriteLine("2. Update a country's name by countryID");
            Console.WriteLine("3. Insert a new country");
            Console.WriteLine("4. Delete a country by countryName");
            Console.WriteLine("Select an option: ");
            return Console.ReadLine();

        }

        public void DisplayCountries(List<Country> countries)
        {
            foreach (Country country in countries)
            {
                Console.WriteLine($"{country.CountryID}, {country.CountryName}");
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
