using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12TPIPROJECT.models
{
    public class Country
    {
        public int CountryID { get; set; }
        public string CountryName { get; set; }

        public Country(int countryID, string countryName)
        {
            CountryID = countryID;
            CountryName = countryName;
        }
    }

    
}

