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
            using (SqlCommand cmd = new SqlCommand("DELTE FROM l_locations.tableCountry WHERE countryName = @CountryName", conn))
            {
                cmd.Parameters.AddWithValue("@CountryName", countryName);
                return cmd.ExecuteNonQuery();
            }
        }

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
        //---------------------------------------------------------------------------------------------------------------------------------------------------
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
            using (SqlCommand cmd = new SqlCommand("DELTE FROM l_locations.tableCity WHERE cityName = @CityName", conn))
            {
                cmd.Parameters.AddWithValue("@CityName", cityName);
                return cmd.ExecuteNonQuery();
            }
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

