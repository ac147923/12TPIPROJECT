using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12TPIPROJECT.models
{
    public class CoachAndTeamBridge
    {
        public int CoachAndTeamBridgeID { get; set; }
        public int DomesticTeamID { get; set; }
        public int CoachID { get; set; }
        public DateTime DateJoined { get; set; }
        public DateTime DateLeft { get; set; }

        public CoachAndTeamBridge(int bridgeID, int domesticTeamID, int coachID, DateTime dateJoined, DateTime dateLeft)
        {
            CoachAndTeamBridgeID = bridgeID;
            DomesticTeamID = domesticTeamID;
            CoachID = coachID;
            DateJoined = dateJoined;
            DateLeft = dateLeft;
        }
    }
}
