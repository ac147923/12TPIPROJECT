using _12TPIPROJECT.view;
using _12TPIPROJECT.Repositories;
using _12TPIPROJECT.models;
namespace _12TPIPROJECT
{
    public class Program
    {
        static StorageManager storageManager;
        static ConsoleView view;
        static User currentUser = null;


        static void Main(string[] args)
        {
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\ac147923\\OneDrive - Avondale College\\Documents\\.2025Code\\12TPIPROJECT\\DB\\12TpiCricketDatabase.mdf\";Integrated Security=True;Connect Timeout=30";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            storageManager = new StorageManager(conn);
            while (true)
            {
                Console.Clear();
                if (currentUser == null)
                {
                    ConsoleView.DisplayMainMenu();
                    switch (Console.ReadLine())
                    {
                        case "1": Login(); break;
                        case "2": Register(); break;
                        case "0":
                            conn.Close();
                            return;
                        default:
                            ConsoleView.ShowInvalidChoice();
                            break;
                    }
                }
                else
                {
                    if (currentUser != null && currentUser.IsAdmin)
                    {
                        ShowAdminMenu();
                    }
                    else if (currentUser != null)
                    {
                        ShowUserMenu();
                    }
                }
            }


            static void Login()
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("Login:");

                    string username;
                    do
                    {
                        Console.Write("Username: ");
                        username = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(username))
                        {
                            Console.Clear();
                            Console.WriteLine("Username cannot be blank. Please enter a valid username.");
                        }
                    } while (string.IsNullOrWhiteSpace(username));

                    string password;
                    do
                    {
                        Console.Write("Password: ");
                        password = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(password))
                        {
                            Console.Clear();
                            Console.WriteLine("Password cannot be blank. Please enter a valid password.");
                        }
                    } while (string.IsNullOrWhiteSpace(password));

                    var user = storageManager.AuthenticateUser(username, password);
                    if (user != null)
                    {
                        currentUser = user;
                        Console.WriteLine("Login successful. Press Enter to continue.");
                        Console.ReadLine();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid credentials. Press Enter to try again.");
                        Console.ReadLine();
                    }
                }
            }

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\nWhich table would you like to manage?");
                Console.WriteLine("1. Country");
                Console.WriteLine("2. City");
                Console.WriteLine("3. Player");
                Console.WriteLine("4. Coach");
                Console.WriteLine("5. Domestic Team");
                Console.WriteLine("6. Player-Team Bridge");
                Console.WriteLine("7. Coach-Team Bridge");
                Console.WriteLine("8. User");
                Console.WriteLine("0. Exit");
                Console.Write("Select an option: ");
                string mainChoice = Console.ReadLine();

                switch (mainChoice)
                {
                    case "1":
                        CountryMenu();
                        break;
                    case "2":
                        CityMenu();
                        break;
                    case "3":
                        PlayerMenu();
                        break;
                    case "4":
                        CoachMenu();
                        break;
                    case "5":
                        DomesticTeamMenu();
                        break;
                    case "6":
                        PlayerAndTeamBridgeMenu();
                        break;
                    case "7":
                        CoachAndTeamBridgeMenu();
                        break;
                    case "8":
                        UserMenu();
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        Console.Clear();
                        view.DisplayMessage("Invalid option. Please try again.");
                        break;
                }
            }
            
        }

     
        // Country Menu
        private static void CountryMenu()
        {
            bool exit = false;
            while (!exit)
            {
                string choice = view.DisplayCountryMenu();
                switch (choice)
                {
                    case "1":
                        var countries = storageManager.GetAllCountries();
                        view.DisplayCountries(countries);
                        break;
                    case "2":
                        if (IsAdmin())
                            UpdateCountryName();
                        else
                            view.DisplayMessage("Only admins can update records.");
                        break;
                    case "3":
                        if (IsAdmin())
                            InsertNewCountry();
                        else
                            view.DisplayMessage("Only admins can insert records.");
                        break;
                    case "4":
                        if (IsAdmin())
                            DeleteCountryByName();
                        else
                            view.DisplayMessage("Only admins can delete records.");
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        view.DisplayMessage("Invalid option. Please try again.");
                        break;
                }
            }
        }

        // City Menu
        private static void CityMenu()
        {
            bool exit = false;
            while (!exit)
            {
                string choice = view.DisplayCityMenu();
                switch (choice)
                {
                    case "1":
                        var cities = storageManager.GetAllCities();
                        foreach (var city in cities)
                            Console.WriteLine($"{city.CityID}, {city.CityName}");
                        break;
                    case "2":
                        if (IsAdmin())
                        {
                            view.DisplayMessage("Enter the city_id to update:");
                            int cityID = view.GetIntInput();
                            view.DisplayMessage("Enter the new city name:");
                            string cityName = view.GetInput();
                            int rowsAffected = storageManager.UpdateCityName(cityID, cityName);
                            view.DisplayMessage($"Rows affected: {rowsAffected}");
                        }
                        else
                        {
                            view.DisplayMessage("Only admins can update records.");
                        }
                        break;
                    case "3":
                        if (IsAdmin())
                        {
                            view.DisplayMessage("Enter new city name:");
                            string newCityName = view.GetInput();
                            int newCityID = 0;
                            City city1 = new City(newCityID, newCityName);
                            int generatedID = storageManager.InsertCity(city1);
                            view.DisplayMessage($"New city inserted with ID: {generatedID}");
                        }
                        else
                        {
                            view.DisplayMessage("Only admins can insert records.");
                        }
                        break;
                    case "4":
                        if (IsAdmin())
                        {
                            view.DisplayMessage("Enter the city name to delete: ");
                            string delCityName = view.GetInput();
                            int delRows = storageManager.DeleteCityByName(delCityName);
                            view.DisplayMessage($"Rows affected: {delRows}");
                        }
                        else
                        {
                            view.DisplayMessage("Only admins can delete records.");
                        }
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        view.DisplayMessage("Invalid option. Please try again.");
                        break;
                }
            }
        }

        // Player Menu
        private static void PlayerMenu()
        {
            bool exit = false;
            while (!exit)
            {
                string choice = view.DisplayPlayerMenu();
                switch (choice)
                {
                    case "1":
                        var players = storageManager.GetAllPlayers();
                        foreach (var player in players)
                            Console.WriteLine($"{player.PlayerID}, {player.PlayerName}");
                        break;
                    case "2":
                        if (IsAdmin())
                        {
                            view.DisplayMessage("Enter the player_id to update:");
                            int playerID = view.GetIntInput();
                            view.DisplayMessage("Enter the new player name:");
                            string playerName = view.GetInput();
                            int rowsAffected = storageManager.UpdatePlayerName(playerID, playerName);
                            view.DisplayMessage($"Rows affected: {rowsAffected}");
                        }
                        else
                        {
                            view.DisplayMessage("Only admins can update records.");
                        }
                        break;
                    case "3":
                        if (IsAdmin())
                        {
                            view.DisplayMessage("Enter new player name:");
                            string newPlayerName = view.GetInput();
                            int newPlayerID = 0;
                            Player player1 = new Player(newPlayerID, newPlayerName);
                            int generatedID = storageManager.InsertPlayer(player1);
                            view.DisplayMessage($"New player inserted with ID: {generatedID}");
                        }
                        else
                        {
                            view.DisplayMessage("Only admins can insert records.");
                        }
                        break;
                    case "4":
                        if (IsAdmin())
                        {
                            view.DisplayMessage("Enter the player name to delete: ");
                            string delPlayerName = view.GetInput();
                            int delRows = storageManager.DeletePlayerByName(delPlayerName);
                            view.DisplayMessage($"Rows affected: {delRows}");
                        }
                        else
                        {
                            view.DisplayMessage("Only admins can delete records.");
                        }
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        view.DisplayMessage("Invalid option. Please try again.");
                        break;
                }
            }
        }

        // Coach Menu
        private static void CoachMenu()
        {
            bool exit = false;
            while (!exit)
            {
                string choice = view.DisplayCoachMenu();
                switch (choice)
                {
                    case "1":
                        var coaches = storageManager.GetAllCoaches();
                        foreach (var coach in coaches)
                            Console.WriteLine($"{coach.CoachID}, {coach.CoachName}");
                        break;
                    case "2":
                        if (IsAdmin())
                        {
                            view.DisplayMessage("Enter the coach_id to update:");
                            int coachID = view.GetIntInput();
                            view.DisplayMessage("Enter the new coach name:");
                            string coachName = view.GetInput();
                            int rowsAffected = storageManager.UpdateCoachName(coachID, coachName);
                            view.DisplayMessage($"Rows affected: {rowsAffected}");
                        }
                        else
                        {
                            view.DisplayMessage("Only admins can update records.");
                        }
                        break;
                    case "3":
                        if (IsAdmin())
                        {
                            view.DisplayMessage("Enter new coach name:");
                            string newCoachName = view.GetInput();
                            int newCoachID = 0;
                            Coach coach1 = new Coach(newCoachID, newCoachName);
                            int generatedID = storageManager.InsertCoach(coach1);
                            view.DisplayMessage($"New coach inserted with ID: {generatedID}");
                        }
                        else
                        {
                            view.DisplayMessage("Only admins can insert records.");
                        }
                        break;
                    case "4":
                        if (IsAdmin())
                        {
                            view.DisplayMessage("Enter the coach name to delete: ");
                            string delCoachName = view.GetInput();
                            int delRows = storageManager.DeleteCoachByName(delCoachName);
                            view.DisplayMessage($"Rows affected: {delRows}");
                        }
                        else
                        {
                            view.DisplayMessage("Only admins can delete records.");
                        }
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        view.DisplayMessage("Invalid option. Please try again.");
                        break;
                }
            }
        }

        // Domestic Team Menu
        private static void DomesticTeamMenu()
        {
            bool exit = false;
            while (!exit)
            {
                string choice = view.DisplayDomesticTeamMenu();
                switch (choice)
                {
                    case "1":
                        var teams = storageManager.GetAllDomesticTeams();
                        foreach (var team in teams)
                            Console.WriteLine($"{team.DomesticTeamID}, {team.DomesticTeamName}, CityID: {team.CityID}");
                        break;
                    case "2":
                        if (IsAdmin())
                        {
                            view.DisplayMessage("Enter the team_id to update:");
                            int teamID = view.GetIntInput();
                            view.DisplayMessage("Enter the new team name:");
                            string teamName = view.GetInput();
                            int rowsAffected = storageManager.UpdateDomesticTeamName(teamID, teamName);
                            view.DisplayMessage($"Rows affected: {rowsAffected}");
                        }
                        else
                        {
                            view.DisplayMessage("Only admins can update records.");
                        }
                        break;
                    case "3":
                        if (IsAdmin())
                        {
                            view.DisplayMessage("Enter new team name:");
                            string newTeamName = view.GetInput();
                            view.DisplayMessage("Enter city ID for the team:");
                            int cityID = view.GetIntInput();
                            int newTeamID = 0;
                            DomesticTeam team1 = new DomesticTeam(newTeamID, newTeamName, cityID);
                            int generatedID = storageManager.InsertDomesticTeam(team1);
                            view.DisplayMessage($"New team inserted with ID: {generatedID}");
                        }
                        else
                        {
                            view.DisplayMessage("Only admins can insert records.");
                        }
                        break;
                    case "4":
                        if (IsAdmin())
                        {
                            view.DisplayMessage("Enter the team name to delete: ");
                            string delTeamName = view.GetInput();
                            int delRows = storageManager.DeleteDomesticTeamByName(delTeamName);
                            view.DisplayMessage($"Rows affected: {delRows}");
                        }
                        else
                        {
                            view.DisplayMessage("Only admins can delete records.");
                        }
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        view.DisplayMessage("Invalid option. Please try again.");
                        break;
                }
            }
        }

        // PlayerAndTeamBridge Menu
        private static void PlayerAndTeamBridgeMenu()
        {
            bool exit = false;
            while (!exit)
            {
                string choice = view.DisplayPlayerAndTeamBridgeMenu();
                switch (choice)
                {
                    case "1":
                        var bridges = storageManager.GetAllPlayerAndTeamBridges();
                        foreach (var bridge in bridges)
                            Console.WriteLine($"{bridge.PlayerAndTeamBridgeID}, PlayerID: {bridge.PlayerID}, TeamID: {bridge.DomesticTeamID}, Joined: {bridge.DateJoined}, Left: {bridge.DateLeft}");
                        break;
                    case "2":
                        if (IsAdmin())
                        {
                            view.DisplayMessage("Enter the bridge_id to update:");
                            int bridgeID = view.GetIntInput();
                            view.DisplayMessage("Enter player ID:");
                            int playerID = view.GetIntInput();
                            view.DisplayMessage("Enter team ID:");
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
                            PlayerAndTeamBridge bridge1 = new PlayerAndTeamBridge(bridgeID, playerID, teamID, dateJoined, dateLeft, catchesTaken, catchesDropped, totalWickets, totalRuns);
                            int rowsAffected = storageManager.UpdatePlayerAndTeamBridge(bridge1);
                            view.DisplayMessage($"Rows affected: {rowsAffected}");
                        }
                        else
                        {
                            view.DisplayMessage("Only admins can update records.");
                        }
                        break;
                    case "3":
                        if (IsAdmin())
                        {
                            view.DisplayMessage("Enter player ID:");
                            int newPlayerID = view.GetIntInput();
                            view.DisplayMessage("Enter team ID:");
                            int newTeamID = view.GetIntInput();
                            view.DisplayMessage("Enter date joined (yyyy-MM-dd):");
                            DateTime newDateJoined = DateTime.Parse(view.GetInput());
                            view.DisplayMessage("Enter date left (yyyy-MM-dd):");
                            DateTime newDateLeft = DateTime.Parse(view.GetInput());
                            view.DisplayMessage("Enter catches taken:");
                            int newCatchesTaken = view.GetIntInput();
                            view.DisplayMessage("Enter catches dropped:");
                            int newCatchesDropped = view.GetIntInput();
                            view.DisplayMessage("Enter total wickets:");
                            int newTotalWickets = view.GetIntInput();
                            view.DisplayMessage("Enter total runs:");
                            int newTotalRuns = view.GetIntInput();
                            int newBridgeID = 0;
                            PlayerAndTeamBridge newBridge = new PlayerAndTeamBridge(newBridgeID, newPlayerID, newTeamID, newDateJoined, newDateLeft, newCatchesTaken, newCatchesDropped, newTotalWickets, newTotalRuns);
                            int generatedID = storageManager.InsertPlayerAndTeamBridge(newBridge);
                            view.DisplayMessage($"New player-team bridge inserted with ID: {generatedID}");
                        }
                        else
                        {
                            view.DisplayMessage("Only admins can insert records.");
                        }
                        break;
                    case "4":
                        if (IsAdmin())
                        {
                            view.DisplayMessage("Enter the bridge ID to delete: ");
                            int delBridgeID = view.GetIntInput();
                            int delRows = storageManager.DeletePlayerAndTeamBridge(delBridgeID);
                            view.DisplayMessage($"Rows affected: {delRows}");
                        }
                        else
                        {
                            view.DisplayMessage("Only admins can delete records.");
                        }
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        view.DisplayMessage("Invalid option. Please try again.");
                        break;
                }
            }
        }

        // CoachAndTeamBridge Menu
        private static void CoachAndTeamBridgeMenu()
        {
            bool exit = false;
            while (!exit)
            {
                string choice = view.DisplayCoachAndTeamBridgeMenu();
                switch (choice)
                {
                    case "1":
                        var bridges = storageManager.GetAllCoachAndTeamBridges();
                        foreach (var bridge in bridges)
                            Console.WriteLine($"{bridge.CoachAndTeamBridgeID}, TeamID: {bridge.DomesticTeamID}, CoachID: {bridge.CoachID}, Joined: {bridge.DateJoined}, Left: {bridge.DateLeft}");
                        break;
                    case "2":
                        if (IsAdmin())
                        {
                            view.DisplayMessage("Enter the bridge_id to update:");
                            int bridgeID = view.GetIntInput();
                            view.DisplayMessage("Enter team ID:");
                            int teamID = view.GetIntInput();
                            view.DisplayMessage("Enter coach ID:");
                            int coachID = view.GetIntInput();
                            view.DisplayMessage("Enter date joined (yyyy-MM-dd):");
                            DateTime dateJoined = DateTime.Parse(view.GetInput());
                            view.DisplayMessage("Enter date left (yyyy-MM-dd):");
                            DateTime dateLeft = DateTime.Parse(view.GetInput());
                            CoachAndTeamBridge bridge1 = new CoachAndTeamBridge(bridgeID, teamID, coachID, dateJoined, dateLeft);
                            int rowsAffected = storageManager.UpdateCoachAndTeamBridge(bridge1);
                            view.DisplayMessage($"Rows affected: {rowsAffected}");
                        }
                        else
                        {
                            view.DisplayMessage("Only admins can update records.");
                        }
                        break;
                    case "3":
                        if (IsAdmin())
                        {
                            view.DisplayMessage("Enter team ID:");
                            int newTeamID = view.GetIntInput();
                            view.DisplayMessage("Enter coach ID:");
                            int newCoachID = view.GetIntInput();
                            view.DisplayMessage("Enter date joined (yyyy-MM-dd):");
                            DateTime newDateJoined = DateTime.Parse(view.GetInput());
                            view.DisplayMessage("Enter date left (yyyy-MM-dd):");
                            DateTime newDateLeft = DateTime.Parse(view.GetInput());
                            int newBridgeID = 0;
                            CoachAndTeamBridge newBridge = new CoachAndTeamBridge(newBridgeID, newTeamID, newCoachID, newDateJoined, newDateLeft);
                            int generatedID = storageManager.InsertCoachAndTeamBridge(newBridge);
                            view.DisplayMessage($"New coach-team bridge inserted with ID: {generatedID}");
                        }
                        else
                        {
                            view.DisplayMessage("Only admins can insert records.");
                        }
                        break;
                    case "4":
                        if (IsAdmin())
                        {
                            view.DisplayMessage("Enter the bridge ID to delete: ");
                            int delBridgeID = view.GetIntInput();
                            int delRows = storageManager.DeleteCoachAndTeamBridge(delBridgeID);
                            view.DisplayMessage($"Rows affected: {delRows}");
                        }
                        else
                        {
                            view.DisplayMessage("Only admins can delete records.");
                        }
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        view.DisplayMessage("Invalid option. Please try again.");
                        break;
                }
            }
        }

        // User Menu
        private static void UserMenu()
        {
            bool exit = false;
            while (!exit)
            {
                string choice = view.DisplayUserMenu();
                switch (choice)
                {
                    case "1":
                        var users = storageManager.GetAllUsers();
                        foreach (var user in users)
                            Console.WriteLine($"{user.UserID}, {user.Username}, {user.Pin}, {user.Role}");
                        break;
                    case "2":
                        if (IsAdmin())
                        {
                            view.DisplayMessage("Enter username:");
                            string username = view.GetInput();
                            view.DisplayMessage("Enter pin:");
                            string pin = view.GetInput();
                            view.DisplayMessage("Enter role:");
                            string role = view.GetInput();
                            int userID = 0;
                            User user1 = new User(userID, username, pin, role);
                            int generatedID = storageManager.InsertUser(user1);
                            view.DisplayMessage($"New user inserted with ID: {generatedID}");
                        }
                        else
                        {
                            view.DisplayMessage("Only admins can insert users.");
                        }
                        break;
                    case "3":
                        view.DisplayMessage("Enter username:");
                        string findUsername = view.GetInput();
                        view.DisplayMessage("Enter pin:");
                        string findPin = view.GetInput();
                        var foundUser = storageManager.GetUserByUsernameAndPin(findUsername, findPin);
                        if (foundUser != null)
                            Console.WriteLine($"{foundUser.UserID}, {foundUser.Username}, {foundUser.Pin}, {foundUser.Role}");
                        else
                            view.DisplayMessage("User not found.");
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        view.DisplayMessage("Invalid option. Please try again.");
                        break;
                }
            }
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

        private static void Register()
        {
            Console.Clear();
            view.DisplayMessage("----Registration Page----\n");
            view.DisplayMessage("Enter your username: ");
            string username = view.GetInput();
            view.DisplayMessage("Enter your password: ");
            string password = view.GetInput();

            bool registered = storageManager.RegisterUser(username, password);
            if (!registered)
            {
                view.DisplayMessage("Username already exists. Please choose a different username.");
            }
            else
            {
                view.DisplayMessage("Registration successful!");
            }
        }

        private static User Login()
        {
            Console.Clear();
            view.DisplayMessage("----Login Page----\n");
            view.DisplayMessage("Enter your username: ");
            string username = view.GetInput();
            view.DisplayMessage("Enter your password: ");
            string password = view.GetInput();

            var user = storageManager.AuthenticateUser(username, password);
            if (user != null)
            {
                view.DisplayMessage("Login successful!");
                return user;
            }
            else
            {
                view.DisplayMessage("Invalid username or password. Please try again.");
                return null;
            }
        }

        private static void Exit()
        {
            view.DisplayMessage("Exiting the application. Goodbye!");
            Environment.Exit(0);
        }
    }
}