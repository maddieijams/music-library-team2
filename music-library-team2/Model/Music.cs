using Windows.System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using System;

namespace music_library_team2.Model
{
    // There are more genre but these are enough for our project.
    // enum not best approach since we want the strings, Could figure out how to use a static class of constants instead
     public enum Genre
    {
        Rock,
        Jazz,
        Soul,
        Pop,
        Hiphop,
        Country

    } 

    public class Music
    {

        public Music(string  title, string singer,  int relaseYear, Genre genre, string filePath, string coverPictureFilePath)
        {
            Title = title;
            Singer = singer;
            RelaseYear = relaseYear;
            Genre = genre;
            CoverPictureFilePath = coverPictureFilePath;
            FilePath = filePath;

        }
        public string Title { get; set; }
        public string Singer { get; set; }
        public int RelaseYear{ get; set; }
        public string FilePath { get; set; }    
        public string CoverPictureFilePath { get; set; }
        public Genre Genre { get; set; }




    }
}
