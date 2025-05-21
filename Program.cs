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
            string connectionString = "Data Source=(localdb)\\ProjectModels;Initial Catalog=TPISQL2025;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

            storageManager = new StorageManager(connectionString);
            ConsoleView view = new ConsoleView();
            string choice = view.DisplayMenu();

            switch (choice)
            {
                case "1":
                    {
                        List<Brand> brands =
                            storageManager.GetAllBrands();
                        view.DisplayBrands(brands);

                    }
                    break;
                case "2":
                    UpdateBrandName();
                    break;
                case "3":
                    //InsertNewBrand();
                    break;
                case "4":
                    //DeleteBrandByName();
                    break;

                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }

        private static void UpdateBrandName()
        {
            view.DisplayMessage("Enter the brand_id to update:");
            int brandID = view.GetIntInput();
            view.DisplayMessage("Enter the new brand name:");
            string brandName = view.GetInput();
            int rowsAffected = storageManager.UpdateBrandName(brandID, brandName);
            view.DisplayMessage($"Rows affected: {rowsAffected}");

        }
    }
}
