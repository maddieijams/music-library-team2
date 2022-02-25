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
    /* public enum Genre
    {
        Rock ,
        Jazz ,
        Soul,
        Pop,
        Hiphop,
        Country

    } */

    public class Music
    {

        public Music(string  title, string singer,  int relaseYear, string genre, string filePath, string CoverPictureFilePath)
        {
            this.Title = title;
            this.Singer = singer;   
            this.RelaseYear = relaseYear;
            this.Genre = genre;
            this.CoverPictureFilePath= CoverPictureFilePath;
            this.FilePath = filePath;

        }
        public string Title { get; set; }
        public string Singer { get; set; }
        public int RelaseYear{ get; set; }
        public string FilePath { get; set; }    
        public string CoverPictureFilePath { get; set; }
        public string Genre { get; set; }




    }
}
