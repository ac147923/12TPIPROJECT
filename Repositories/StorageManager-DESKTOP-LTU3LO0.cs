using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _12TPIPROJECT.models;
using _12TPIPROJECT.view;
using _12TPIPROJECT.Repositories;
using Microsoft.Data.SqlClient;

namespace _12TPIPROJECT.Repositories
{

    public class StorageManager
    {
        private SqlConnection conn;
        public bool RegisterUser(string username, string password)
        {

            string checkSql = "SELECT COUNT(*) FROM tblUser WHERE Username = @Username";
            using (SqlCommand checkCmd = new SqlCommand(checkSql, conn))
            {
                checkCmd.Parameters.AddWithValue("@Username", username);
                int count = (int)checkCmd.ExecuteScalar();
                if (count > 0)
                    return false;
            }


            string insertSql = "INSERT INTO tblUser (Username, Password, IsAdmin) VALUES (@Username, @Password, 0)";
            using (SqlCommand insertCmd = new SqlCommand(insertSql, conn))
            {
                insertCmd.Parameters.AddWithValue("@Username", username);
                insertCmd.Parameters.AddWithValue("@Password", password);
                insertCmd.ExecuteNonQuery();
            }
            return true;
        }

        public User AuthenticateUser(string username, string password)
        {
            string sql = "SELECT UserID, Username, Password, IsAdmin FROM tblUser WHERE Username = @Username AND Password = @Password";
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int userID = (int)reader["UserID"];
                        string usernameDb = reader["Username"].ToString();
                        string pin = reader["Password"].ToString();
                        bool isAdmin = (bool)reader["IsAdmin"];
                        string role = isAdmin ? "Admin" : "User";
                        return new User(userID, usernameDb, pin, role);
                    }
                }
            }
            return null;
        }

        public StorageManager(string connectionString)
        {
            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();
                Console.WriteLine("connection successfull");
            }
            catch (SqlException e)
            {
                Console.WriteLine("the connection was unsucessfull");
                Console.WriteLine(e.Message);

            }
            catch (Exception ex)
            {
                Console.WriteLine("the connection was successful");
                Console.WriteLine(ex.Message);
            }
        }

        public List<Country> GetAllCountries()
        {
            List<Country> countries = new List<Country>();
            string sqlString = "SELECT * From l_locations.tableCountry";
            using (SqlCommand cmd = new SqlCommand(sqlString, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int CountryID = Convert.ToInt32(reader["CountryID"]);
                        string CountryName = reader["CountryName"].ToString();
                        countries.Add(new Country(CountryID, CountryName));
                    }
                }
            }
            return countries;
        }

        public int UpdateCountryName(int countryID, string countryName)
        {
            using (SqlCommand cmd = new SqlCommand($"UPDATE l_locations.tableCountry SET countryName = @CountryName WHERE countryID = @CountryID", conn))
            {

                cmd.Parameters.AddWithValue("@CountryName", countryName);
                cmd.Parameters.AddWithValue("@CountryID", countryID);
                return cmd.ExecuteNonQuery();


            }


        }

        public int InsertCountry(Country countrytemp)
        {
            using (SqlCommand cmd = new SqlCommand("INSERT INTO l_locations.tableCountry (countryName) VALUES (@CountryName); SELECT SCOPE_IDENTITY();", conn))
            {
                cmd.Parameters.AddWithValue("@CountryName", countrytemp.CountryName);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }

        }

        public int DeleteCountryByName(string countryName)
        {
            using (SqlCommand cmd = new SqlCommand("DELETE FROM l_locations.tableCountry WHERE countryName = @CountryName", conn))
            {
                cmd.Parameters.AddWithValue("@CountryName", countryName);
                return cmd.ExecuteNonQuery();
            }
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------
        public List<City> GetAllCities()
        {
            List<City> cities = new List<City>();
            string sqlString = "SELECT * From l_locations.tableCity";
            using (SqlCommand cmd = new SqlCommand(sqlString, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int CityID = Convert.ToInt32(reader["CityID"]);
                        string CityName = reader["CityName"].ToString();
                        cities.Add(new City(CityID, CityName));
                    }
                }
            }
            return cities;
        }

        public int UpdateCityName(int cityID, string cityName)
        {
            using (SqlCommand cmd = new SqlCommand($"UPDATE l_locations.tableCity SET cityName = @CityName WHERE cityID = @CityID", conn))
            {

                cmd.Parameters.AddWithValue("@CityName", cityName);
                cmd.Parameters.AddWithValue("@CityID", cityID);
                return cmd.ExecuteNonQuery();


            }


        }

        public int InsertCity(City citytemp)
        {
            using (SqlCommand cmd = new SqlCommand("INSERT INTO l_locations.tableCity (cityName) VALUES (@CityName); SELECT SCOPE_IDENTITY();", conn))
            {
                cmd.Parameters.AddWithValue("@CityName", citytemp.CityName);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }

        }

        public int DeleteCityByName(string cityName)
        {
            using (SqlCommand cmd = new SqlCommand("DELETE FROM l_locations.tableCity WHERE cityName = @CityName", conn))
            {
                cmd.Parameters.AddWithValue("@CityName", cityName);
                return cmd.ExecuteNonQuery();
            }
        }

        public List<Player> GetAllPlayers()
        {
            List<Player> players = new List<Player>();
            string sqlString = "SELECT * From t_teams.tablePlayer";
            using (SqlCommand cmd = new SqlCommand(sqlString, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int PlayerID = Convert.ToInt32(reader["PlayerID"]);
                        string PlayerName = reader["PlayerName"].ToString();
                        players.Add(new Player(PlayerID, PlayerName));
                    }
                }
            }
            return players;
        }

        public int UpdatePlayerName(int playerID, string playerName)
        {
            using (SqlCommand cmd = new SqlCommand($"UPDATE t_teams.tablePlayer SET playerName = @PlayerName WHERE playerID = @PlayerID", conn))
            {

                cmd.Parameters.AddWithValue("@PlayerName", playerName);
                cmd.Parameters.AddWithValue("@PlayerID", playerID);
                return cmd.ExecuteNonQuery();


            }


        }

        public int InsertPlayer(Player playertemp)
        {
            using (SqlCommand cmd = new SqlCommand("INSERT INTO t_teams.tablePlayer (playerName) VALUES (@PlayerName); SELECT SCOPE_IDENTITY();", conn))
            {
                cmd.Parameters.AddWithValue("@PlayerName", playertemp.PlayerName);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }

        }

        public int DeletePlayerByName(string playerName)
        {
            using (SqlCommand cmd = new SqlCommand("DELETE FROM t_teams.tablePlayer WHERE playerName = @PlayerName", conn))
            {
                cmd.Parameters.AddWithValue("@PlayerName", playerName);
                return cmd.ExecuteNonQuery();
            }
        }

        public List<Coach> GetAllCoaches()
        {
            List<Coach> coaches = new List<Coach>();
            string sqlString = "SELECT * FROM t_teams.tableCoaches";
            using (SqlCommand cmd = new SqlCommand(sqlString, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int coachID = Convert.ToInt32(reader["coachID"]);
                        string coachName = reader["coachName"].ToString();
                        coaches.Add(new Coach(coachID, coachName));
                    }
                }
            }
            return coaches;
        }

        public int InsertCoach(Coach coach)
        {
            using (SqlCommand cmd = new SqlCommand("INSERT INTO t_teams.tableCoaches (coachName) VALUES (@CoachName); SELECT SCOPE_IDENTITY();", conn))
            {
                cmd.Parameters.AddWithValue("@CoachName", coach.CoachName);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int UpdateCoachName(int coachID, string coachName)
        {
            using (SqlCommand cmd = new SqlCommand("UPDATE t_teams.tableCoaches SET coachName = @CoachName WHERE coachID = @CoachID", conn))
            {
                cmd.Parameters.AddWithValue("@CoachName", coachName);
                cmd.Parameters.AddWithValue("@CoachID", coachID);
                return cmd.ExecuteNonQuery();
            }
        }

        public int DeleteCoachByName(string coachName)
        {
            using (SqlCommand cmd = new SqlCommand("DELETE FROM t_teams.tableCoaches WHERE coachName = @CoachName", conn))
            {
                cmd.Parameters.AddWithValue("@CoachName", coachName);
                return cmd.ExecuteNonQuery();
            }
        }

        public List<DomesticTeam> GetAllDomesticTeams()
        {
            List<DomesticTeam> teams = new List<DomesticTeam>();
            string sqlString = "SELECT * FROM t_teams.tableDomesticTeam";
            using (SqlCommand cmd = new SqlCommand(sqlString, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int domesticTeamID = Convert.ToInt32(reader["domesticTeamID"]);
                        string domesticTeamName = reader["domesticTeamName"].ToString();
                        int cityID = Convert.ToInt32(reader["cityID"]);
                        teams.Add(new DomesticTeam(domesticTeamID, domesticTeamName, cityID));
                    }
                }
            }
            return teams;
        }

        public int InsertDomesticTeam(DomesticTeam team)
        {
            using (SqlCommand cmd = new SqlCommand("INSERT INTO t_teams.tableDomesticTeam (domesticTeamName, cityID) VALUES (@TeamName, @CityID); SELECT SCOPE_IDENTITY();", conn))
            {
                cmd.Parameters.AddWithValue("@TeamName", team.DomesticTeamName);
                cmd.Parameters.AddWithValue("@CityID", team.CityID);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int UpdateDomesticTeamName(int teamID, string teamName)
        {
            using (SqlCommand cmd = new SqlCommand("UPDATE t_teams.tableDomesticTeam SET domesticTeamName = @TeamName WHERE domesticTeamID = @TeamID", conn))
            {
                cmd.Parameters.AddWithValue("@TeamName", teamName);
                cmd.Parameters.AddWithValue("@TeamID", teamID);
                return cmd.ExecuteNonQuery();
            }
        }

        public int DeleteDomesticTeamByName(string teamName)
        {
            using (SqlCommand cmd = new SqlCommand("DELETE FROM t_teams.tableDomesticTeam WHERE domesticTeamName = @TeamName", conn))
            {
                cmd.Parameters.AddWithValue("@TeamName", teamName);
                return cmd.ExecuteNonQuery();
            }
        }

        public List<PlayerAndTeamBridge> GetAllPlayerAndTeamBridges()
        {
            List<PlayerAndTeamBridge> bridges = new List<PlayerAndTeamBridge>();
            string sqlString = "SELECT * FROM t_teams.tablePlayerAndTeamBridge";
            using (SqlCommand cmd = new SqlCommand(sqlString, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int bridgeID = Convert.ToInt32(reader["playerAndTeamBridgeID"]);
                        int playerID = Convert.ToInt32(reader["playerID"]);
                        int domesticTeamID = Convert.ToInt32(reader["domesticTeamID"]);
                        DateTime dateJoined = Convert.ToDateTime(reader["dateJoined"]);
                        DateTime dateLeft = Convert.ToDateTime(reader["dateLeft"]);
                        int catchesTaken = Convert.ToInt32(reader["catchesTaken"]);
                        int catchesDropped = Convert.ToInt32(reader["catchesDropped"]);
                        int totalWickets = Convert.ToInt32(reader["totalWickets"]);
                        int totalRuns = Convert.ToInt32(reader["totalRuns"]);
                        bridges.Add(new PlayerAndTeamBridge(
                            bridgeID,
                            playerID,
                            domesticTeamID,
                            dateJoined,
                            dateLeft,
                            catchesTaken,
                            catchesDropped,
                            totalWickets,
                            totalRuns
                        ));
                    }
                }
            }
            return bridges;
        }

        public int InsertPlayerAndTeamBridge(PlayerAndTeamBridge bridge)
        {
            using (SqlCommand cmd = new SqlCommand(
                @"INSERT INTO t_teams.tablePlayerAndTeamBridge 
        (playerID, domesticTeamID, dateJoined, dateLeft, catchesTaken, catchesDropped, totalWickets, totalRuns)
        VALUES (@PlayerID, @TeamID, @DateJoined, @DateLeft, @CatchesTaken, @CatchesDropped, @TotalWickets, @TotalRuns);
        SELECT SCOPE_IDENTITY();", conn))
            {
                cmd.Parameters.AddWithValue("@PlayerID", bridge.PlayerID);
                cmd.Parameters.AddWithValue("@TeamID", bridge.DomesticTeamID);
                cmd.Parameters.AddWithValue("@DateJoined", bridge.DateJoined);
                cmd.Parameters.AddWithValue("@DateLeft", bridge.DateLeft);
                cmd.Parameters.AddWithValue("@CatchesTaken", bridge.CatchesTaken);
                cmd.Parameters.AddWithValue("@CatchesDropped", bridge.CatchesDropped);
                cmd.Parameters.AddWithValue("@TotalWickets", bridge.TotalWickets);
                cmd.Parameters.AddWithValue("@TotalRuns", bridge.TotalRuns);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int UpdatePlayerAndTeamBridge(PlayerAndTeamBridge bridge)
        {
            using (SqlCommand cmd = new SqlCommand(
                @"UPDATE t_teams.tablePlayerAndTeamBridge SET 
          playerID = @PlayerID, domesticTeamID = @TeamID, dateJoined = @DateJoined, dateLeft = @DateLeft,
          catchesTaken = @CatchesTaken, catchesDropped = @CatchesDropped, 
          totalWickets = @TotalWickets, totalRuns = @TotalRuns
        WHERE playerAndTeamBridgeID = @BridgeID", conn))
            {
                cmd.Parameters.AddWithValue("@PlayerID", bridge.PlayerID);
                cmd.Parameters.AddWithValue("@TeamID", bridge.DomesticTeamID);
                cmd.Parameters.AddWithValue("@DateJoined", bridge.DateJoined);
                cmd.Parameters.AddWithValue("@DateLeft", bridge.DateLeft);
                cmd.Parameters.AddWithValue("@CatchesTaken", bridge.CatchesTaken);
                cmd.Parameters.AddWithValue("@CatchesDropped", bridge.CatchesDropped);
                cmd.Parameters.AddWithValue("@TotalWickets", bridge.TotalWickets);
                cmd.Parameters.AddWithValue("@TotalRuns", bridge.TotalRuns);
                cmd.Parameters.AddWithValue("@BridgeID", bridge.PlayerAndTeamBridgeID);
                return cmd.ExecuteNonQuery();
            }
        }

        public int DeletePlayerAndTeamBridge(int bridgeID)
        {
            using (SqlCommand cmd = new SqlCommand("DELETE FROM t_teams.tablePlayerAndTeamBridge WHERE playerAndTeamBridgeID = @BridgeID", conn))
            {
                cmd.Parameters.AddWithValue("@BridgeID", bridgeID);
                return cmd.ExecuteNonQuery();
            }
        }

        public List<CoachAndTeamBridge> GetAllCoachAndTeamBridges()
        {
            List<CoachAndTeamBridge> bridges = new List<CoachAndTeamBridge>();
            string sqlString = "SELECT * FROM t_teams.tableCoachAndTeamBridge";
            using (SqlCommand cmd = new SqlCommand(sqlString, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int bridgeID = Convert.ToInt32(reader["coachAndTeamBridgeID"]);
                        int domesticTeamID = Convert.ToInt32(reader["domesticTeamID"]);
                        int coachID = Convert.ToInt32(reader["coachID"]);
                        DateTime dateJoined = Convert.ToDateTime(reader["dateJoined"]);
                        DateTime dateLeft = Convert.ToDateTime(reader["dateLeft"]);
                        bridges.Add(new CoachAndTeamBridge(
                            bridgeID,
                            domesticTeamID,
                            coachID,
                            dateJoined,
                            dateLeft
                        ));
                    }
                }
            }
            return bridges;
        }

        public int InsertCoachAndTeamBridge(CoachAndTeamBridge bridge)
        {
            using (SqlCommand cmd = new SqlCommand(
                @"INSERT INTO t_teams.tableCoachAndTeamBridge 
        (domesticTeamID, coachID, dateJoined, dateLeft)
        VALUES (@TeamID, @CoachID, @DateJoined, @DateLeft);
        SELECT SCOPE_IDENTITY();", conn))
            {
                cmd.Parameters.AddWithValue("@TeamID", bridge.DomesticTeamID);
                cmd.Parameters.AddWithValue("@CoachID", bridge.CoachID);
                cmd.Parameters.AddWithValue("@DateJoined", bridge.DateJoined);
                cmd.Parameters.AddWithValue("@DateLeft", bridge.DateLeft);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int UpdateCoachAndTeamBridge(CoachAndTeamBridge bridge)
        {
            using (SqlCommand cmd = new SqlCommand(
                @"UPDATE t_teams.tableCoachAndTeamBridge SET 
          domesticTeamID = @TeamID, coachID = @CoachID, dateJoined = @DateJoined, dateLeft = @DateLeft
        WHERE coachAndTeamBridgeID = @BridgeID", conn))
            {
                cmd.Parameters.AddWithValue("@TeamID", bridge.DomesticTeamID);
                cmd.Parameters.AddWithValue("@CoachID", bridge.CoachID);
                cmd.Parameters.AddWithValue("@DateJoined", bridge.DateJoined);
                cmd.Parameters.AddWithValue("@DateLeft", bridge.DateLeft);
                cmd.Parameters.AddWithValue("@BridgeID", bridge.CoachAndTeamBridgeID);
                return cmd.ExecuteNonQuery();
            }
        }

        public int DeleteCoachAndTeamBridge(int bridgeID)
        {
            using (SqlCommand cmd = new SqlCommand("DELETE FROM t_teams.tableCoachAndTeamBridge WHERE coachAndTeamBridgeID = @BridgeID", conn))
            {
                cmd.Parameters.AddWithValue("@BridgeID", bridgeID);
                return cmd.ExecuteNonQuery();
            }
        }

        public List<User> GetAllUsers()
        {
            List<User> users = new List<User>();
            string sqlString = "SELECT * FROM users";
            using (SqlCommand cmd = new SqlCommand(sqlString, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int userID = Convert.ToInt32(reader["userID"]);
                        string username = reader["username"].ToString();
                        string pin = reader["password"].ToString();
                        string role = reader["role"].ToString();
                        users.Add(new User(userID, username, pin, role));
                    }
                }
            }
            return users;
        }

        public int InsertUser(User user)
        {
            using (SqlCommand cmd = new SqlCommand("INSERT INTO users (Username, Password, Role) VALUES (@Username, @Pin, @Role); SELECT SCOPE_IDENTITY();", conn))
            {
                cmd.Parameters.AddWithValue("@Username", user.Username);
                cmd.Parameters.AddWithValue("@Pin", user.Pin);
                cmd.Parameters.AddWithValue("@Role", user.Role);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public User GetUserByUsernameAndPin(string username, string pin)
        {
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM users WHERE Username = @Username AND Password = @Pin", conn))
            {
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Pin", pin);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new User(
                            Convert.ToInt32(reader["UserID"]),
                            reader["Username"].ToString(),
                            reader["Password"].ToString(),
                            reader["Role"].ToString()
                        );
                    }
                }
            }
            return null;
        }

        public void CloseConnection()
        {
            if (conn != null && conn.State == ConnectionState.Open)
            {
                conn.Close();
                Console.WriteLine("Connection closed");
            }
        }
    }
}

