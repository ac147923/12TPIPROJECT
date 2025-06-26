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
            string countryChoice = view.DisplayCountryMenu();
            switch (countryChoice)
            {
                case "1":
                    List<Country> countries = storageManager.GetAllCountries();
                    view.DisplayCountries(countries);
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

            string cityChoice = view.DisplayCityMenu();
            switch (cityChoice)
            {
                case "1":
                    List<City> cities = storageManager.GetAllCities();
                    view.DisplayCities(cities);
                    break;
                case "2":
                    UpdateCityName();
                    break;
                case "3":
                    InsertNewCity();
                    break;
                case "4":
                    DeleteCityByName();
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
            storageManager.CloseConnection();

            string playerChoice = view.DisplayPlayerMenu();
            switch (playerChoice)
            {
                case "1":
                    List<Player> players = storageManager.GetAllPlayers();
                    view.DisplayPlayers(players);
                    break;
                case "2":
                    UpdatePlayerName();
                    break;
                case "3":
                    InsertNewPlayer();
                    break;
                case "4":
                    DeletePlayerByName();
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
            storageManager.CloseConnection();

            string coachChoice = view.DisplayCoachMenu();
            switch (coachChoice)
            {
                case "1":
                    List<Coach> coaches = storageManager.GetAllCoaches();
                    view.DisplayCoaches(coaches);
                    break;
                case "2":
                    UpdateCoachName();
                    break;
                case "3":
                    InsertNewCoach();
                    break;
                case "4":
                    DeleteCoachByName();
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
            storageManager.CloseConnection();

            string teamChoice = view.DisplayDomesticTeamMenu();
            switch (teamChoice)
            {
                case "1":
                    List<DomesticTeam> teams = storageManager.GetAllDomesticTeams();
                    view.DisplayDomesticTeams(teams);
                    break;
                case "2":
                    UpdateDomesticTeamName();
                    break;
                case "3":
                    InsertNewDomesticTeam();
                    break;
                case "4":
                    DeleteDomesticTeamByName();
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
            storageManager.CloseConnection();

            string ptBridgeChoice = view.DisplayPlayerAndTeamBridgeMenu();
            switch (ptBridgeChoice)
            {
                case "1":
                    List<PlayerAndTeamBridge> ptBridges = storageManager.GetAllPlayerAndTeamBridges();
                    view.DisplayPlayerAndTeamBridges(ptBridges);
                    break;
                case "2":
                    UpdatePlayerAndTeamBridge();
                    break;
                case "3":
                    InsertNewPlayerAndTeamBridge();
                    break;
                case "4":
                    DeletePlayerAndTeamBridge();
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
            storageManager.CloseConnection();

            string ctBridgeChoice = view.DisplayCoachAndTeamBridgeMenu();
            switch (ctBridgeChoice)
            {
                case "1":
                    List<CoachAndTeamBridge> ctBridges = storageManager.GetAllCoachAndTeamBridges();
                    view.DisplayCoachAndTeamBridges(ctBridges);
                    break;
                case "2":
                    UpdateCoachAndTeamBridge();
                    break;
                case "3":
                    InsertNewCoachAndTeamBridge();
                    break;
                case "4":
                    DeleteCoachAndTeamBridge();
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
            storageManager.CloseConnection();

            string userChoice = view.DisplayUserMenu();
            switch (userChoice)
            {
                case "1":
                    List<User> users = storageManager.GetAllUsers();
                    view.DisplayUsers(users);
                    break;
                case "2":
                    InsertNewUser();
                    break;
                case "3":
                    UpdateUser();
                    break;
                case "4":
                    DeleteUser();
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
            view.DisplayMessage($"New country inserted with ID: {generatedID}");
        }

        private static void DeleteCountryByName()
        {
            view.DisplayMessage("Enter the country name to delete: ");
            string countryName = view.GetInput();
            int rowsAffected = storageManager.DeleteCountryByName(countryName);
            view.DisplayMessage($"Rows affected: {rowsAffected}");
        }

        private static void UpdateCityName()
        {
            view.DisplayMessage("Enter the city_id to update:");
            int cityID = view.GetIntInput();
            view.DisplayMessage("Enter the new city name:");
            string cityName = view.GetInput();
            int rowsAffected = storageManager.UpdateCityName(cityID, cityName);
            view.DisplayMessage($"Rows affected: {rowsAffected}");
        }

        private static void InsertNewCity()
        {
            view.DisplayMessage("Enter new city name:");
            string cityName = view.GetInput();
            int cityID = 0;
            City city1 = new City(cityID, cityName);
            int generatedID = storageManager.InsertCity(city1);
            view.DisplayMessage($"New city inserted with ID: {generatedID}");
        }

        private static void DeleteCityByName()
        {
            view.DisplayMessage("Enter the city name to delete: ");
            string cityName = view.GetInput();
            int rowsAffected = storageManager.DeleteCityByName(cityName);
            view.DisplayMessage($"Rows affected: {rowsAffected}");
        }

        private static void UpdatePlayerName()
        {
            view.DisplayMessage("Enter the player_id to update:");
            int playerID = view.GetIntInput();
            view.DisplayMessage("Enter the new player name:");
            string playerName = view.GetInput();
            int rowsAffected = storageManager.UpdatePlayerName(playerID, playerName);
            view.DisplayMessage($"Rows affected: {rowsAffected}");
        }

        private static void InsertNewPlayer()
        {
            view.DisplayMessage("Enter new player name:");
            string playerName = view.GetInput();
            int playerID = 0;
            Player player1 = new Player(playerID, playerName);
            int generatedID = storageManager.InsertPlayer(player1);
            view.DisplayMessage($"New player inserted with ID: {generatedID}");
        }

        private static void DeletePlayerByName()
        {
            view.DisplayMessage("Enter the player name to delete: ");
            string playerName = view.GetInput();
            int rowsAffected = storageManager.DeletePlayerByName(playerName);
            view.DisplayMessage($"Rows affected: {rowsAffected}");
        }


        private static void UpdateCoachName()
        {
            view.DisplayMessage("Enter the coach_id to update:");
            int coachID = view.GetIntInput();
            view.DisplayMessage("Enter the new coach name:");
            string coachName = view.GetInput();
            int rowsAffected = storageManager.UpdateCoachName(coachID, coachName);
            view.DisplayMessage($"Rows affected: {rowsAffected}");
        }

        private static void InsertNewCoach()
        {
            view.DisplayMessage("Enter new coach name:");
            string coachName = view.GetInput();
            int coachID = 0;
            Coach coach1 = new Coach(coachID, coachName);
            int generatedID = storageManager.InsertCoach(coach1);
            view.DisplayMessage($"New coach inserted with ID: {generatedID}");
        }

        private static void DeleteCoachByName()
        {
            view.DisplayMessage("Enter the coach name to delete: ");
            string coachName = view.GetInput();
            int rowsAffected = storageManager.DeleteCoachByName(coachName);
            view.DisplayMessage($"Rows affected: {rowsAffected}");
        }

        private static void UpdateDomesticTeamName()
        {
            view.DisplayMessage("Enter the domestic team id to update:");
            int teamID = view.GetIntInput();
            view.DisplayMessage("Enter the new team name:");
            string teamName = view.GetInput();
            int rowsAffected = storageManager.UpdateDomesticTeamName(teamID, teamName);
            view.DisplayMessage($"Rows affected: {rowsAffected}");
        }

        private static void InsertNewDomesticTeam()
        {
            view.DisplayMessage("Enter new team name:");
            string teamName = view.GetInput();
            view.DisplayMessage("Enter city id for the team:");
            int cityID = view.GetIntInput();
            DomesticTeam team = new DomesticTeam(0, teamName, cityID);
            int generatedID = storageManager.InsertDomesticTeam(team);
            view.DisplayMessage($"New domestic team inserted with ID: {generatedID}");
        }

        private static void DeleteDomesticTeamByName()
        {
            view.DisplayMessage("Enter the team name to delete: ");
            string teamName = view.GetInput();
            int rowsAffected = storageManager.DeleteDomesticTeamByName(teamName);
            view.DisplayMessage($"Rows affected: {rowsAffected}");
        }


        private static void UpdatePlayerAndTeamBridge()
        {
            view.DisplayMessage("Enter the bridge ID to update:");
            int bridgeID = view.GetIntInput();
            view.DisplayMessage("Enter player ID:");
            int playerID = view.GetIntInput();
            view.DisplayMessage("Enter domestic team ID:");
            int teamID = view.GetIntInput();
            view.DisplayMessage("Enter date joined (yyyy-MM-dd):");
            DateTime dateJoined = DateTime.Parse(view.GetInput());
            view.DisplayMessage("Enter date left (yyyy-MM-dd):");
            DateTime dateLeft = DateTime.Parse(view.GetInput());
            view.DisplayMessage("Enter catches taken:");
            int catchesTaken = view.GetIntInput();
            view.DisplayMessage("Enter catches dropped:");
            int catchesDropped = view.GetIntInput();
            view.DisplayMessage("Enter total wickets:");
            int totalWickets = view.GetIntInput();
            view.DisplayMessage("Enter total runs:");
            int totalRuns = view.GetIntInput();

            var bridge = new PlayerAndTeamBridge(bridgeID, playerID, teamID, dateJoined, dateLeft, catchesTaken, catchesDropped, totalWickets, totalRuns);
            int rowsAffected = storageManager.UpdatePlayerAndTeamBridge(bridge);
            view.DisplayMessage($"Rows affected: {rowsAffected}");
        }

        private static void InsertNewPlayerAndTeamBridge()
        {
            view.DisplayMessage("Enter player ID:");
            int playerID = view.GetIntInput();
            view.DisplayMessage("Enter domestic team ID:");
            int teamID = view.GetIntInput();
            view.DisplayMessage("Enter date joined (yyyy-MM-dd):");
            DateTime dateJoined = DateTime.Parse(view.GetInput());
            view.DisplayMessage("Enter date left (yyyy-MM-dd):");
            DateTime dateLeft = DateTime.Parse(view.GetInput());
            view.DisplayMessage("Enter catches taken:");
            int catchesTaken = view.GetIntInput();
            view.DisplayMessage("Enter catches dropped:");
            int catchesDropped = view.GetIntInput();
            view.DisplayMessage("Enter total wickets:");
            int totalWickets = view.GetIntInput();
            view.DisplayMessage("Enter total runs:");
            int totalRuns = view.GetIntInput();

            var bridge = new PlayerAndTeamBridge(0, playerID, teamID, dateJoined, dateLeft, catchesTaken, catchesDropped, totalWickets, totalRuns);
            int generatedID = storageManager.InsertPlayerAndTeamBridge(bridge);
            view.DisplayMessage($"New bridge inserted with ID: {generatedID}");
        }

        private static void DeletePlayerAndTeamBridge()
        {
            view.DisplayMessage("Enter the bridge ID to delete:");
            int bridgeID = view.GetIntInput();
            int rowsAffected = storageManager.DeletePlayerAndTeamBridge(bridgeID);
            view.DisplayMessage($"Rows affected: {rowsAffected}");
        }

        private static void UpdateCoachAndTeamBridge()
        {
            view.DisplayMessage("Enter the bridge ID to update:");
            int bridgeID = view.GetIntInput();
            view.DisplayMessage("Enter domestic team ID:");
            int teamID = view.GetIntInput();
            view.DisplayMessage("Enter coach ID:");
            int coachID = view.GetIntInput();
            view.DisplayMessage("Enter date joined (yyyy-MM-dd):");
            DateTime dateJoined = DateTime.Parse(view.GetInput());
            view.DisplayMessage("Enter date left (yyyy-MM-dd):");
            DateTime dateLeft = DateTime.Parse(view.GetInput());

            var bridge = new CoachAndTeamBridge(bridgeID, teamID, coachID, dateJoined, dateLeft);
            int rowsAffected = storageManager.UpdateCoachAndTeamBridge(bridge);
            view.DisplayMessage($"Rows affected: {rowsAffected}");
        }

        private static void InsertNewCoachAndTeamBridge()
        {
            view.DisplayMessage("Enter domestic team ID:");
            int teamID = view.GetIntInput();
            view.DisplayMessage("Enter coach ID:");
            int coachID = view.GetIntInput();
            view.DisplayMessage("Enter date joined (yyyy-MM-dd):");
            DateTime dateJoined = DateTime.Parse(view.GetInput());
            view.DisplayMessage("Enter date left (yyyy-MM-dd):");
            DateTime dateLeft = DateTime.Parse(view.GetInput());

            var bridge = new CoachAndTeamBridge(0, teamID, coachID, dateJoined, dateLeft);
            int generatedID = storageManager.InsertCoachAndTeamBridge(bridge);
            view.DisplayMessage($"New bridge inserted with ID: {generatedID}");
        }

        private static void DeleteCoachAndTeamBridge()
        {
            view.DisplayMessage("Enter the bridge ID to delete:");
            int bridgeID = view.GetIntInput();
            int rowsAffected = storageManager.DeleteCoachAndTeamBridge(bridgeID);
            view.DisplayMessage($"Rows affected: {rowsAffected}");
        }


    }
}
