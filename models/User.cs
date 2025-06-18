using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12TPIPROJECT.models
{
    public class User
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Pin { get; set; }
        public string Role { get; set; }

        public User(int userID, string username, string pin, string role)
        {
            UserID = userID;
            Username = username;
            Pin = pin;
            Role = role;
        }
    }
}
