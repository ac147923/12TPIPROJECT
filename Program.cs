using _12TPIPROJECT.view;
using _12TPIPROJECT.Repositories;
using _12TPIPROJECT.models;
namespace _12TPIPROJECT
{
    public class Program
    {
        private static StorageManager storageManager;
        private static ConsoleView view;
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\ac147923\\OneDrive - Avondale College\\Documents\\.2025Code\\12TPIPROJECT\\DB\\12TpiCricketDatabase.mdf\";Integrated Security=True;Connect Timeout=30";

            storageManager = new StorageManager(connectionString);
            ConsoleView view = new ConsoleView();
            string choice = view.DisplayMenu();

            switch (choice)
            {
                case "1":
                    {
                        List<Country> countries =
                            storageManager.GetAllCountries();
                        view.DisplayCountries(countries);

                    }
                    break;
                case "2":
                    UpdateCountryName();
                    break;
                case "3":
                    InsertNewCountry();
                    break;
                case "4":
                    DeleteCountryByName();
                    break;

                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
            storageManager.CloseConnection();
        }

        private static void UpdateCountryName()
        {
            view.DisplayMessage("Enter the country_id to update:");
            int countryID = view.GetIntInput();
            view.DisplayMessage("Enter the new country name:");
            string countryName = view.GetInput();
            int rowsAffected = storageManager.UpdateCountryName(countryID, countryName);
            view.DisplayMessage($"Rows affected: {rowsAffected}");

        }

        private static void InsertNewCountry()
        {
            view.DisplayMessage("Enter new country name:");
            string countryName = view.GetInput();
            int countryID = 0; 
            Country country1 = new Country(countryID, countryName);
            int generatedID = storageManager.InsertCountry(country1);
            view.DisplayMessage($"New country insrted with ID: {generatedID}");

        }

        private static void DeleteCountryByName()
        {
            view.DisplayMessage("Enter the country name to delete: ");
            string countryName = view.GetInput();
            int rowsAffected = storageManager.DeleteCountryByName(countryName);
            view.DisplayMessage($"Rows affected: {rowsAffected}");
        }


    }
}
