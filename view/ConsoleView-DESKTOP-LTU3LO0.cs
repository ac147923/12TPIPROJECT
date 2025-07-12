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

        public string DisplayMainMenu()
        {
            Console.WriteLine("Cricket Database");
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Register");
            Console.WriteLine("0. Exit");
            Console.Write("Select an option: ");
            return Console.ReadLine();
        }

        public static void ShowInvalidChoice()
        {
            Console.WriteLine("Invalid choice. Press Enter to continue.");
            Console.ReadLine();
        }
        public string DisplayCountryMenu()
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

        public string DisplayCityMenu()
        {
            Console.WriteLine("City Menu:");
            Console.WriteLine("1. View All records in City");
            Console.WriteLine("2. Update a city's name by cityID");
            Console.WriteLine("3. Insert a new city");
            Console.WriteLine("4. Delete a city by cityName");
            Console.Write("Select an option: ");
            return Console.ReadLine();
        }

        public string DisplayPlayerMenu()
        {
            Console.WriteLine("Player Menu:");
            Console.WriteLine("1. View All records in Player");
            Console.WriteLine("2. Update a player's name by playerID");
            Console.WriteLine("3. Insert a new player");
            Console.WriteLine("4. Delete a player by playerName");
            Console.Write("Select an option: ");
            return Console.ReadLine();
        }

        public string DisplayCoachMenu()
        {
            Console.WriteLine("Coach Menu:");
            Console.WriteLine("1. View All records in Coach");
            Console.WriteLine("2. Update a coach's name by coachID");
            Console.WriteLine("3. Insert a new coach");
            Console.WriteLine("4. Delete a coach by coachName");
            Console.Write("Select an option: ");
            return Console.ReadLine();
        }

        public string DisplayDomesticTeamMenu()
        {
            Console.WriteLine("Domestic Team Menu:");
            Console.WriteLine("1. View All records in Domestic Team");
            Console.WriteLine("2. Update a team's name by teamID");
            Console.WriteLine("3. Insert a new domestic team");
            Console.WriteLine("4. Delete a domestic team by teamName");
            Console.Write("Select an option: ");
            return Console.ReadLine();
        }

        public string DisplayPlayerAndTeamBridgeMenu()
        {
            Console.WriteLine("Player-Team Bridge Menu:");
            Console.WriteLine("1. View All records in PlayerAndTeamBridge");
            Console.WriteLine("2. Update a bridge record by bridgeID");
            Console.WriteLine("3. Insert a new player-team bridge");
            Console.WriteLine("4. Delete a player-team bridge by bridgeID");
            Console.Write("Select an option: ");
            return Console.ReadLine();
        }

        public string DisplayCoachAndTeamBridgeMenu()
        {
            Console.WriteLine("Coach-Team Bridge Menu:");
            Console.WriteLine("1. View All records in CoachAndTeamBridge");
            Console.WriteLine("2. Update a bridge record by bridgeID");
            Console.WriteLine("3. Insert a new coach-team bridge");
            Console.WriteLine("4. Delete a coach-team bridge by bridgeID");
            Console.Write("Select an option: ");
            return Console.ReadLine();
        }

        public string DisplayUserMenu()
        {
            Console.WriteLine("User Menu:");
            Console.WriteLine("1. View All users");
            Console.WriteLine("2. Insert a new user");
            Console.WriteLine("3. Find user by username and pin");
            Console.Write("Select an option: ");
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
