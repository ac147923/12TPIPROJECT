using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12TPIPROJECT.models
{
    public class City
    {
        public int CityID { get; set; }
        public string CityName { get; set; }

        public City(int cityID, string cityName)
        {
            CityID = cityID;
            CityName = cityName;
        }
    }


}
