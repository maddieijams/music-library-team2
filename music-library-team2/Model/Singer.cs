using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace music_library_team2.Model
{
    public class Singer
    {
        public Singer(string firstName, string lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;   
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public String FullName => $"{this.FirstName} {this.LastName}";

    }
}
