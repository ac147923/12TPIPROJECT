using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12TPIPROJECT.models
{
    public class Coach
    {
        public int CoachID { get; set; }
        public string CoachName { get; set; }

        public Coach(int coachID, string coachName)
        {
            CoachID = coachID;
            CoachName = coachName;
        }
    }
}