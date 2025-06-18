using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12TPIPROJECT.models
{
        public class Player
        {
            public int PlayerID { get; set; }
            public string PlayerName { get; set; }

            public Player(int playerID, string playerName)
            {
                PlayerID = playerID;
                PlayerName = playerName;
            }
        }
}
