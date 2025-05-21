namespace _12TPIPROJECT
{
    public class Program
    {
        private static StorageManager storageManager;
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
                default:
                    Console.WriteLine("Invalid option. Please try" +
                        "again");
                    break;
            }
        }
    }
}
