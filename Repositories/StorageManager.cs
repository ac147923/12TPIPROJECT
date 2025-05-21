using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _12TPIPROJECT.models;
using Microsoft.Data.SqlClient;

namespace _12TPIPROJECT.Repositories
{
    private SqlConnection conn;
    
    public storageManager(string connectionString)
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

    public List<Brand> GetBrand()
    {
        List<Brand> brands = new List<Brand>();
        string sqlString = "SELECT * From dbo.tblBrand";
        using (SqlCommand cmd = new SqlCommand)
    }
}

