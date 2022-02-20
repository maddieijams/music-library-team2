using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace music_library_team2.Model
{
    public static class MusicManager
    {




        //public ObservableCollection<Music> Music { get; set; }

        public static void GetAllMusics(ObservableCollection<Music>musics)
        {
            musics.Clear();
            var musicsLists = GetMusicLists();
            musicsLists.ForEach(m=> musics.Add(m));
        }

        public static void GetMusicsByGenre(ObservableCollection<Music> musics, Genre genre)
        {
            musics.Clear();
            var musicsLists = GetMusicLists();
           var filtredMusics= musicsLists.Where(m => m.Genre == genre).ToList();
            filtredMusics.ForEach(m=> musics.Add(m));
        }

        private static List<Music> GetMusicLists()
        {
            var musics = new List<Music>()
            {

                new Music("I can fly on sky", new Singer("Mickel", "Jakson"), 1984, Genre.country, @"https://file-examples-com.github.io/uploads/2017/11/file_example_WAV_1MG.wav", @"https://cdn.pixabay.com/photo/2017/03/19/17/50/guitar-2157112_960_720.jpg"),
                new Music("Take My Breath", new Singer("The weeknd", "Tesfaye"), 2022, Genre.Jazz, @"http://codeskulptor-demos.commondatastorage.googleapis.com/GalaxyInvaders/theme_01.mp3", @"https://assets.capitalxtra.com/2015/41/the-weeknd--1444904446-view-1.jpg"),
                new Music("Don't Matter", new Singer("Akon", "Aliaune"), 2009, Genre.hiphop, @"http://commondatastorage.googleapis.com/codeskulptor-demos/DDR_assets/Sevish_-__nbsp_.mp3", @"https://m.media-amazon.com/images/I/51VWlORLtVL._SX425_.jpg"),
            };
            return musics;
        }
       
    }
}
