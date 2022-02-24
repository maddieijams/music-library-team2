namespace music_library_team2
{
    using music_library_team2.Model;
    using System;
    using System.Collections.ObjectModel;
    using Windows.Storage;
    using Windows.Storage.Pickers;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        /// <summary>
        /// Defines the Musics.
        /// </summary>
        private ObservableCollection<Music> Musics = new ObservableCollection<Music>();

        /// <summary>
        /// Defines the Genres.
        /// </summary>
        private ObservableCollection<Genre> Genres = new ObservableCollection<Genre>();

        // private Action<Music, ObservableCollection<Music>> AddSong;
        /// <summary>
        /// Initializes a new instance of the <see cref="MainPage"/> class.
        /// </summary>
        public MainPage()
        {
            this.InitializeComponent();
            MusicManager.GetAllMusics(Musics);
            MusicManager.GetGenrs(this.Genres, this.Musics);
        }

        /// <summary>
        /// The AddSongBtn_Click.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="RoutedEventArgs"/>.</param>
        private async void AddSongBtn_Click(object sender, RoutedEventArgs e)
        {
            await ContentDialog.ShowAsync();
        }

        /// <summary>
        /// The CancelBtn_Click.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="RoutedEventArgs"/>.</param>
        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog.Hide();
        }

        /// <summary>
        /// The AddSong.
        /// </summary>
        /// <param name="song">The song<see cref="Music"/>.</param>
        public void AddSong(Music song)
        {
            MusicManager.AddSongtoMusics(song, Musics);
        }

        /// <summary>
        /// The PickAFileButton_Click.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="RoutedEventArgs"/>.</param>
        private async void PickAFileButton_Click(object sender, RoutedEventArgs e)
        {
            // Clear previous returned file name, if it exists, between iterations of this scenario
            OutputTextBlock.Text = "";

            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".png");
            StorageFile file = await openPicker.PickSingleFileAsync();
            if (file != null)
            {
                // The StorageFile has read/write access to the picked file.
                // See the FileAccess sample for code that uses a StorageFile to read and write.
                OutputTextBlock.Text = "Picked photo: " + file.Name;
            }
            else
            {
                OutputTextBlock.Text = "Operation cancelled.";
            }
        }

        private void GridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var music = (Music)e.ClickedItem;
            SoundMedia.Source = new Uri(music.FilePath);

        }
    }
}
