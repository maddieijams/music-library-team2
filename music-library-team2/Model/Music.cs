using Windows.System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using System;

namespace music_library_team2.Model
{
    public enum Genre
    {
        Rock,
        Jazz,
        Soul,
        pop,
        hiphop,
        country
    }
    public  class Music
    {

        public Music(string  title, Singer singer,  int relaseYear, Genre genre)
        {
            this.Title = title;
            this.Singer = singer;   
            this.RelaseYear = relaseYear;
            this.Genre = genre;

        }
        public string Title { get; set; }
        public Singer Singer { get; set; }
        public int RelaseYear{ get; set; }
        public string FilePath { get; set; }    
        public string Cover { get; set; }
        public Genre Genre { get; set; }




    }
}
