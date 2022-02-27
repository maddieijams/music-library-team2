namespace music_library_team2
{
    using music_library_team2.Model;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
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

        /// <summary>
        /// Defines the filePath.
        /// </summary>
        private string filePath;


        /// <summary>
        /// Initializes a new instance of the <see cref="MainPage"/> class.
        /// </summary>
        public MainPage()
        {
            this.InitializeComponent();
            MusicManager.GetAllMusics(Musics);
            MusicManager.GetGenrs(Genres, Musics);
            List<Genre> allGeners = Enum.GetValues(typeof(Genre)).Cast<Genre>().ToList();
            GenreComboBox.ItemsSource = allGeners;
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
            openPicker.SuggestedStartLocation = PickerLocationId.Downloads;
            string[] fileFormats = { ".mp4", ".flac", ".mp3", ".m4a", ".wav", ".wma", ".aac" };
            foreach (string x in fileFormats)
            {
                openPicker.FileTypeFilter.Add(x);
            }


            StorageFile file = await openPicker.PickSingleFileAsync();
            if (file != null)
            {
                // The StorageFile has read/write access to the picked file.
                // See the FileAccess sample for code that uses a StorageFile to read and write.
                OutputTextBlock.Text = "Picked photo: " + file.Name;
                filePath = file.Path;
            }
            else
            {
                OutputTextBlock.Text = "Operation cancelled.";
            }
        }

        /// <summary>
        /// The ContentDialog_PrimaryButtonClick.
        /// </summary>
        /// <param name="sender">The sender<see cref="ContentDialog"/>.</param>
        /// <param name="args">The args<see cref="ContentDialogButtonClickEventArgs"/>.</param>
        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            System.Diagnostics.Debug.WriteLine(GenreComboBox.SelectedItem);
            System.Diagnostics.Debug.WriteLine(args.ToString());
            System.Diagnostics.Debug.WriteLine("hjkjhgkh");

            if (GenreComboBox.SelectedItem != null)
            {
                MusicManager.AddSongtoMusics(new Music(Title.Text, "Frank Ocean", short.Parse(ReleaseYear.Text), (Genre)GenreComboBox.SelectedItem, filePath, CoverPictureFilePath.Text), Musics);
            }
        }

        /// <summary>
        /// The GridView_ItemClick.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="ItemClickEventArgs"/>.</param>
        private void GridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            SoundMedia.Visibility= Visibility.Visible;
            var music = (Music)e.ClickedItem;
            SoundMedia.Source = new Uri(music.FilePath);
        }

        private void GenrList_ItemClick(object sender, ItemClickEventArgs e)
        {
            AllMusicButton.Visibility= Visibility.Visible;
            var menuGenre = (Genre)e.ClickedItem;
            MusicManager.GetMusicsByGenre(Musics, menuGenre);
        }

        private void AllMusicButton_Click(object sender, RoutedEventArgs e)
        {
           
            MusicManager.GetAllMusics(Musics);
            AllMusicButton.Visibility = Visibility.Collapsed;
        }

        
    }
}
