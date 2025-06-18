using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12TPIPROJECT.models
{
    public class DomesticTeam
    {
        public int DomesticTeamID { get; set; }
        public string DomesticTeamName { get; set; }
        public int CityID { get; set; }

        public DomesticTeam(int domesticTeamID, string domesticTeamName, int cityID)
        {
            DomesticTeamID = domesticTeamID;
            DomesticTeamName = domesticTeamName;
            CityID = cityID;
        }
    }
}
