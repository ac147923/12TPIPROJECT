using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _12TPIPROJECT.models;
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
                conn.open();
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

        public List<Brand> GetAllBrands()
        {
            List<Brand> brands = new List<Brand>();
            string sqlString = "SELECT * From dbo.tblBrand";
            using (SqlCommand cmd = new SqlCommand(sqlString, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.read())
                    {
                        int BrandID = Convert.ToInt32(reader["BrandID"]);
                        string BrandName = reader["BrandName"].ToString();
                        brands.Add(new)
                    }
                }
            }
        }

        public int UpdateBrandName(int brandId, string brandName)
        {
            using (SqlCommand cmd = new SqlCommand($"UPDATE production.Brands SET BRAND_NAME = @BrandName WHERE BRAND_ID = @BrandID", conn)
            {
                cmd.Parameters.AddWithValue("@BrandName", brandName),
                cmd.Parameters.AddWithValue("@BrandId", brandId);
                return cmd.ExecuteNonQuery();
            }
        }
    } }

