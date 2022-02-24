namespace music_library_team2.Model
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="MusicManager" />.
    /// </summary>
    public static class MusicManager
    {
        /// <summary>
        /// The AddSongtoMusics.
        /// </summary>
        /// <param name="song">The song<see cref="Music"/>.</param>
        /// <param name="musics">The musics<see cref="ObservableCollection{Music}"/>.</param>
        public static void AddSongtoMusics(Music song, ObservableCollection<Music> musics)
        {
            musics.Add(new Music(song.Title, song.Singer, song.RelaseYear, song.Genre, song.FilePath, song.CoverPictureFilePath));
        }

        /// <summary>
        /// The GetAllMusics.
        /// </summary>
        /// <param name="musics">The musics<see cref="ObservableCollection{Music}"/>.</param>
        public static void GetAllMusics(ObservableCollection<Music> musics)
        {
            musics.Clear();
            var musicsLists = GetMusicLists();
            musicsLists.ForEach(m => musics.Add(m));
        }

        /// <summary>
        /// The GetGenrs.
        /// </summary>
        /// <param name="genrs">The genrs<see cref="ObservableCollection{Genre}"/>.</param>
        /// <param name="music">The music<see cref="ObservableCollection{Music}"/>.</param>
        public static void GetGenrs(ObservableCollection<Genre> genrs, ObservableCollection<Music> music)
        {
            genrs.Clear();
            var distinctGenrs = music.Select(m => m.Genre).Distinct().ToList();
            distinctGenrs.ForEach(g => genrs.Add(g));
        }

        /// <summary>
        /// The GetMusicsByGenre.
        /// </summary>
        /// <param name="musics">The musics<see cref="ObservableCollection{Music}"/>.</param>
        /// <param name="genre">The genre<see cref="Genre"/>.</param>
        public static void GetMusicsByGenre(ObservableCollection<Music> musics, Genre genre)
        {
            musics.Clear();
            var musicsLists = GetMusicLists();
            var filtredMusics = musicsLists.Where(m => m.Genre == genre).ToList();
            filtredMusics.ForEach(m => musics.Add(m));
        }

        /// <summary>
        /// The GetMusicLists.
        /// </summary>
        /// <returns>The <see cref="List{Music}"/>.</returns>
        private static List<Music> GetMusicLists()
        {
            var musics = new List<Music>()
            {

                new Music("I can fly on sky", new Singer("Mickel", "Jakson"), 1984, Genre.Country, @"https://file-examples-com.github.io/uploads/2017/11/file_example_WAV_1MG.wav", @"https://cdn.pixabay.com/photo/2017/03/19/17/50/guitar-2157112_960_720.jpg"),
                new Music("Take My Breath", new Singer("The weeknd", "Tesfaye"), 2022, Genre.Jazz, @"http://codeskulptor-demos.commondatastorage.googleapis.com/GalaxyInvaders/theme_01.mp3", @"https://assets.capitalxtra.com/2015/41/the-weeknd--1444904446-view-1.jpg"),
                new Music("Don't Matter", new Singer("Akon", "Aliaune"), 2009, Genre.Hiphop, @"http://commondatastorage.googleapis.com/codeskulptor-demos/DDR_assets/Sevish_-__nbsp_.mp3", @"https://m.media-amazon.com/images/I/51VWlORLtVL._SX425_.jpg"),
            };
            return musics;
        }
    }
}
