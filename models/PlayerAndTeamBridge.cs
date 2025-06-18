using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace _12TPIPROJECT.models
{
    public class PlayerAndTeamBridge
    {
        public int PlayerAndTeamBridgeID { get; set; }
        public int PlayerID { get; set; }
        public int DomesticTeamID { get; set; }
        public DateTime DateJoined { get; set; }
        public DateTime DateLeft { get; set; }
        public int CatchesTaken { get; set; }
        public int CatchesDropped { get; set; }
        public int TotalWickets { get; set; }
        public int TotalRuns { get; set; }

        public PlayerAndTeamBridge(int bridgeID, int playerID, int domesticTeamID, DateTime dateJoined, DateTime dateLeft,
            int catchesTaken, int catchesDropped, int totalWickets, int totalRuns)
        {
            PlayerAndTeamBridgeID = bridgeID;
            PlayerID = playerID;
            DomesticTeamID = domesticTeamID;
            DateJoined = dateJoined;
            DateLeft = dateLeft;
            CatchesTaken = catchesTaken;
            CatchesDropped = catchesDropped;
            TotalWickets = totalWickets;
            TotalRuns = totalRuns;
        }
    }
}
