using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using _12TPIPROJECT.models;

namespace _12TPIPROJECT.Repositories
{
    public class StorageManager
    {
        private SqlConnection conn;

        public StorageManager(string connectionString)
        {
            conn = new SqlConnection(connectionString);
            conn.Open();
        }

        // User Management
        public bool RegisterUser(string username, string password)
        {
            string checkSql = "SELECT COUNT(*) FROM users WHERE Username = @Username";
            using (SqlCommand checkCmd = new SqlCommand(checkSql, conn))
            {
                checkCmd.Parameters.AddWithValue("@Username", username);
                int count = Convert.ToInt32(checkCmd.ExecuteScalar());
                if (count > 0)
                    return false;
            }

            string insertSql = "INSERT INTO users (Username, Password, IsAdmin) VALUES (@Username, @Password, 0)";
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
            string sql = "SELECT UserID, Username, Password, IsAdmin FROM users WHERE Username = @Username AND Password = @Password";
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new User
                        {
                            UserID = Convert.ToInt32(reader["UserID"]),
                            Username = reader["Username"].ToString(),
                            Password = reader["Password"].ToString(),
                            IsAdmin = Convert.ToBoolean(reader["IsAdmin"])
                        };
                    }
                }
            }
            return null;
        }

        public List<User> GetAllUsers()
        {
            var users = new List<User>();
            string sqlString = "SELECT UserID, Username, Password, IsAdmin FROM users";
            using (SqlCommand cmd = new SqlCommand(sqlString, conn))
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    users.Add(new User
                    {
                        UserID = Convert.ToInt32(reader["UserID"]),
                        Username = reader["Username"].ToString(),
                        Password = reader["Password"].ToString(),
                        IsAdmin = Convert.ToBoolean(reader["IsAdmin"])
                    });
                }
            }
            return users;
        }

        public int InsertUser(User user)
        {
            using (SqlCommand cmd = new SqlCommand(
                "INSERT INTO users (Username, Password, IsAdmin) VALUES (@Username, @Password, @IsAdmin); SELECT SCOPE_IDENTITY();", conn))
            {
                cmd.Parameters.AddWithValue("@Username", user.Username);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                cmd.Parameters.AddWithValue("@IsAdmin", user.IsAdmin);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        // Countries
        public List<Country> GetAllCountries()
        {
            var countries = new List<Country>();
            using (SqlCommand cmd = new SqlCommand("SELECT CountryID, CountryName FROM l_locations.tableCountry", conn))
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    countries.Add(new Country(
                        Convert.ToInt32(reader["CountryID"]),
                        reader["CountryName"].ToString()));
                }
            }
            return countries;
        }

        public int UpdateCountryName(int countryID, string countryName)
        {
            using (SqlCommand cmd = new SqlCommand(
                "UPDATE l_locations.tableCountry SET countryName = @CountryName WHERE countryID = @CountryID", conn))
            {
                cmd.Parameters.AddWithValue("@CountryName", countryName);
                cmd.Parameters.AddWithValue("@CountryID", countryID);
                return cmd.ExecuteNonQuery();
            }
        }

        public int InsertCountry(Country country)
        {
            using (SqlCommand cmd = new SqlCommand(
                "INSERT INTO l_locations.tableCountry (countryName) VALUES (@CountryName); SELECT SCOPE_IDENTITY();", conn))
            {
                cmd.Parameters.AddWithValue("@CountryName", country.CountryName);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int DeleteCountryByName(string countryName)
        {
            using (SqlCommand cmd = new SqlCommand(
                "DELETE FROM l_locations.tableCountry WHERE countryName = @CountryName", conn))
            {
                cmd.Parameters.AddWithValue("@CountryName", countryName);
                return cmd.ExecuteNonQuery();
            }
        }

        // Cities
        public List<City> GetAllCities()
        {
            var cities = new List<City>();
            using (SqlCommand cmd = new SqlCommand("SELECT CityID, CityName FROM l_locations.tableCity", conn))
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    cities.Add(new City(Convert.ToInt32(reader["CityID"]), reader["CityName"].ToString()));
                }
            }
            return cities;
        }

        public int UpdateCityName(int cityID, string cityName)
        {
            using (SqlCommand cmd = new SqlCommand(
                "UPDATE l_locations.tableCity SET cityName = @CityName WHERE cityID = @CityID", conn))
            {
                cmd.Parameters.AddWithValue("@CityName", cityName);
                cmd.Parameters.AddWithValue("@CityID", cityID);
                return cmd.ExecuteNonQuery();
            }
        }

        public int InsertCity(City city)
        {
            using (SqlCommand cmd = new SqlCommand(
                "INSERT INTO l_locations.tableCity (cityName) VALUES (@CityName); SELECT SCOPE_IDENTITY();", conn))
            {
                cmd.Parameters.AddWithValue("@CityName", city.CityName);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int DeleteCityByName(string cityName)
        {
            using (SqlCommand cmd = new SqlCommand(
                "DELETE FROM l_locations.tableCity WHERE cityName = @CityName", conn))
            {
                cmd.Parameters.AddWithValue("@CityName", cityName);
                return cmd.ExecuteNonQuery();
            }
        }

        // Players
        public List<Player> GetAllPlayers()
        {
            var players = new List<Player>();
            using (SqlCommand cmd = new SqlCommand("SELECT PlayerID, PlayerName FROM t_teams.tablePlayer", conn))
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    players.Add(new Player(Convert.ToInt32(reader["PlayerID"]), reader["PlayerName"].ToString()));
                }
            }
            return players;
        }

        public int UpdatePlayerName(int playerID, string playerName)
        {
            using (SqlCommand cmd = new SqlCommand(
                "UPDATE t_teams.tablePlayer SET playerName = @PlayerName WHERE playerID = @PlayerID", conn))
            {
                cmd.Parameters.AddWithValue("@PlayerName", playerName);
                cmd.Parameters.AddWithValue("@PlayerID", playerID);
                return cmd.ExecuteNonQuery();
            }
        }

        public int InsertPlayer(Player player)
        {
            using (SqlCommand cmd = new SqlCommand(
                "INSERT INTO t_teams.tablePlayer (playerName) VALUES (@PlayerName); SELECT SCOPE_IDENTITY();", conn))
            {
                cmd.Parameters.AddWithValue("@PlayerName", player.PlayerName);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int DeletePlayerByName(string playerName)
        {
            using (SqlCommand cmd = new SqlCommand(
                "DELETE FROM t_teams.tablePlayer WHERE playerName = @PlayerName", conn))
            {
                cmd.Parameters.AddWithValue("@PlayerName", playerName);
                return cmd.ExecuteNonQuery();
            }
        }

        // Coaches
        public List<Coach> GetAllCoaches()
        {
            var coaches = new List<Coach>();
            using (SqlCommand cmd = new SqlCommand("SELECT coachID, coachName FROM t_teams.tableCoaches", conn))
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    coaches.Add(new Coach(Convert.ToInt32(reader["coachID"]), reader["coachName"].ToString()));
                }
            }
            return coaches;
        }

        public int InsertCoach(Coach coach)
        {
            using (SqlCommand cmd = new SqlCommand(
                "INSERT INTO t_teams.tableCoaches (coachName) VALUES (@CoachName); SELECT SCOPE_IDENTITY();", conn))
            {
                cmd.Parameters.AddWithValue("@CoachName", coach.CoachName);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int UpdateCoachName(int coachID, string coachName)
        {
            using (SqlCommand cmd = new SqlCommand(
                "UPDATE t_teams.tableCoaches SET coachName = @CoachName WHERE coachID = @CoachID", conn))
            {
                cmd.Parameters.AddWithValue("@CoachName", coachName);
                cmd.Parameters.AddWithValue("@CoachID", coachID);
                return cmd.ExecuteNonQuery();
            }
        }

        public int DeleteCoachByName(string coachName)
        {
            using (SqlCommand cmd = new SqlCommand(
                "DELETE FROM t_teams.tableCoaches WHERE coachName = @CoachName", conn))
            {
                cmd.Parameters.AddWithValue("@CoachName", coachName);
                return cmd.ExecuteNonQuery();
            }
        }

        // Domestic Teams
        public List<DomesticTeam> GetAllDomesticTeams()
        {
            var teams = new List<DomesticTeam>();
            using (SqlCommand cmd = new SqlCommand("SELECT domesticTeamID, domesticTeamName, cityID FROM t_teams.tableDomesticTeam", conn))
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    teams.Add(new DomesticTeam(
                        Convert.ToInt32(reader["domesticTeamID"]),
                        reader["domesticTeamName"].ToString(),
                        Convert.ToInt32(reader["cityID"])));
                }
            }
            return teams;
        }

        public int InsertDomesticTeam(DomesticTeam team)
        {
            using (SqlCommand cmd = new SqlCommand(
                "INSERT INTO t_teams.tableDomesticTeam (domesticTeamName, cityID) VALUES (@TeamName, @CityID); SELECT SCOPE_IDENTITY();", conn))
            {
                cmd.Parameters.AddWithValue("@TeamName", team.DomesticTeamName);
                cmd.Parameters.AddWithValue("@CityID", team.CityID);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int UpdateDomesticTeamName(int teamID, string teamName)
        {
            using (SqlCommand cmd = new SqlCommand(
                "UPDATE t_teams.tableDomesticTeam SET domesticTeamName = @TeamName WHERE domesticTeamID = @TeamID", conn))
            {
                cmd.Parameters.AddWithValue("@TeamName", teamName);
                cmd.Parameters.AddWithValue("@TeamID", teamID);
                return cmd.ExecuteNonQuery();
            }
        }

        public int DeleteDomesticTeamByName(string teamName)
        {
            using (SqlCommand cmd = new SqlCommand(
                "DELETE FROM t_teams.tableDomesticTeam WHERE domesticTeamName = @TeamName", conn))
            {
                cmd.Parameters.AddWithValue("@TeamName", teamName);
                return cmd.ExecuteNonQuery();
            }
        }

        // PlayerAndTeamBridge
        public List<PlayerAndTeamBridge> GetAllPlayerAndTeamBridges()
        {
            var bridges = new List<PlayerAndTeamBridge>();
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM t_teams.tablePlayerAndTeamBridge", conn))
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    bridges.Add(new PlayerAndTeamBridge(
                        Convert.ToInt32(reader["playerAndTeamBridgeID"]),
                        Convert.ToInt32(reader["playerID"]),
                        Convert.ToInt32(reader["domesticTeamID"]),
                        Convert.ToDateTime(reader["dateJoined"]),
                        Convert.ToDateTime(reader["dateLeft"]),
                        Convert.ToInt32(reader["catchesTaken"]),
                        Convert.ToInt32(reader["catchesDropped"]),
                        Convert.ToInt32(reader["totalWickets"]),
                        Convert.ToInt32(reader["totalRuns"])
                    ));
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
            using (SqlCommand cmd = new SqlCommand(
                "DELETE FROM t_teams.tablePlayerAndTeamBridge WHERE playerAndTeamBridgeID = @BridgeID", conn))
            {
                cmd.Parameters.AddWithValue("@BridgeID", bridgeID);
                return cmd.ExecuteNonQuery();
            }
        }

        // CoachAndTeamBridge
        public List<CoachAndTeamBridge> GetAllCoachAndTeamBridges()
        {
            var bridges = new List<CoachAndTeamBridge>();
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM t_teams.tableCoachAndTeamBridge", conn))
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    bridges.Add(new CoachAndTeamBridge(
                        Convert.ToInt32(reader["coachAndTeamBridgeID"]),
                        Convert.ToInt32(reader["domesticTeamID"]),
                        Convert.ToInt32(reader["coachID"]),
                        Convert.ToDateTime(reader["dateJoined"]),
                        Convert.ToDateTime(reader["dateLeft"])
                    ));
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
            using (SqlCommand cmd = new SqlCommand(
                "DELETE FROM t_teams.tableCoachAndTeamBridge WHERE coachAndTeamBridgeID = @BridgeID", conn))
            {
                cmd.Parameters.AddWithValue("@BridgeID", bridgeID);
                return cmd.ExecuteNonQuery();
            }
        }
    }
}