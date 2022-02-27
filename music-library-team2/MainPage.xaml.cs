namespace music_library_team2
{
    using music_library_team2.Model;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.IO;
    using Windows.Media.Core;
    using Windows.Media.Playback;
    using Windows.Storage;
    using Windows.Storage.Pickers;
    using Windows.Storage.Streams;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Navigation;

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
        private string fileName;
        private string imgPath;


        /// <summary>
        /// Initializes a new instance of the <see cref="MainPage"/> class.
        /// </summary>
        public MainPage()
        {
            this.InitializeComponent();
            MusicManager.GetAllMusics(Musics);
            MusicManager.GetGenrs(Genres, Musics);
            GenreComboBox.ItemsSource = Enum.GetValues(typeof(Genre));
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
        /// The PickAFileButton_Click.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="RoutedEventArgs"/>.</param>
        private async void PickMusicFileButton_Click(object sender, RoutedEventArgs e)
        {
            // Clear previous returned file name, if it exists, between iterations of this scenario
            MusicOutputTextBlock.Text = "";

            FileOpenPicker openPicker = new FileOpenPicker
            {
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = PickerLocationId.MusicLibrary
            };

            string[] fileFormats = { ".mp4", ".flac", ".mp3", ".m4a", ".wav", ".wma", ".aac" };
            foreach (string x in fileFormats)
            {
                openPicker.FileTypeFilter.Add(x);
            }

            StorageFile file = await openPicker.PickSingleFileAsync();
            if (file != null)
            {
                // The StorageFile has read/write access to the picked file.
                MusicOutputTextBlock.Text = "Picked music file: " + file.Name;
                filePath = file.Path;
                fileName = file.Name;
            }
            else
            {
                MusicOutputTextBlock.Text = "Operation cancelled.";
            }
        }

        //private async void PickImgFileButton_Click(object sender, RoutedEventArgs e)
        //{
        //    // Clear previous returned file name, if it exists, between iterations of this scenario
        //    ImageOutputTextBlock.Text = "";

        //    FileOpenPicker openPicker = new FileOpenPicker
        //    {
        //        ViewMode = PickerViewMode.Thumbnail,
        //        SuggestedStartLocation = PickerLocationId.PicturesLibrary
        //    };

        //    string[] fileFormats = { ".tif", ".jpg", ".png", ".gif" };
        //    foreach (string x in fileFormats)
        //    {
        //        openPicker.FileTypeFilter.Add(x);
        //    }

        //    StorageFile file = await openPicker.PickSingleFileAsync();
        //    if (file != null)
        //    {
        //        // The StorageFile has read/write access to the picked file.
        //        ImageOutputTextBlock.Text = "Picked image file: " + file.Name;
        //        imgPath = file.Path;
        //        Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList.Add(file);
        //        StorageFolder localFolder = ApplicationData.Current.LocalFolder;
        //        await file.CopyAsync(localFolder, file.Name, NameCollisionOption.GenerateUniqueName);
        //    }
        //    else
        //    {
        //        ImageOutputTextBlock.Text = "Operation cancelled.";
        //    }
        //}

        /// <summary>
        /// The ContentDialog_PrimaryButtonClick.
        /// </summary>
        /// <param name="sender">The sender<see cref="ContentDialog"/>.</param>
        /// <param name="args">The args<see cref="ContentDialogButtonClickEventArgs"/>.</param>
        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (GenreComboBox.SelectedItem != null)
            {
                Musics.Add(new Music(Title.Text, Singer.Text, short.Parse(ReleaseYear.Text), (Genre)GenreComboBox.SelectedItem, filePath, CoverPictureFilePath.Text));
            }
        }

        /// <summary>
        /// The GridView_ItemClick.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="ItemClickEventArgs"/>.</param>
        private async void GridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            SoundMedia.Visibility = Visibility.Visible;
            Music music = (Music)e.ClickedItem;

            // Play from url if it exists 
            if (music.FilePath.Contains("http")) {
                SoundMedia.Source = new Uri(music.FilePath);
                return;
            }

            // Play from file, currently only works if file is from music library folder
            StorageFolder CurrentMusicFolder = await StorageFolder.GetFolderFromPathAsync(Path.GetDirectoryName(music.FilePath));
            StorageFile file = await CurrentMusicFolder.GetFileAsync(fileName);
            SoundMedia.SetSource(await file.OpenAsync(FileAccessMode.Read), file.ContentType);
        }

        //protected async override void OnNavigatedTo(NavigationEventArgs e)
        //{
        //    // Get the Pictures library
        //    StorageFolder picturesFolder =
        //       KnownFolders.PicturesLibrary;
        //    IReadOnlyList<StorageFolder> folders =
        //        await picturesFolder.GetFoldersAsync();

        //    // Process file folders
        //    foreach (StorageFolder folder in folders)
        //    {
        //        // Get and process files in folder
        //        IReadOnlyList<StorageFile> fileList = await folder.GetFilesAsync();
        //        foreach (StorageFile file in fileList)
        //        {
        //            Windows.UI.Xaml.Media.Imaging.BitmapImage bitmapImage =
        //                new Windows.UI.Xaml.Media.Imaging.BitmapImage();

        //            // Open a stream for the selected file.
        //            // The 'using' block ensures the stream is disposed
        //            // after the image is loaded.
        //            using (IRandomAccessStream fileStream =
        //                await file.OpenAsync(FileAccessMode.Read))
        //            {

        //                bitmapImage.SetSource(fileStream);

        //                // Create an Image control.  
        //                Image img = new Image
        //                {
        //                    Height = 50,
        //                    Source = bitmapImage
        //                };

        //                // Add the Image control to the UI. 'imageGrid' is a
        //                // VariableSizedWrapGrid declared in the XAML page.
        //               // SoundImage = img;
        //            }
        //        }
        //    }
        //}

        /// <summary>
        /// The GenrList_ItemClick.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="ItemClickEventArgs"/>.</param>
        private void GenrList_ItemClick(object sender, ItemClickEventArgs e)
        {
            AllMusicButton.Visibility = Visibility.Visible;
            var menuGenre = (Genre)e.ClickedItem;
            MusicManager.GetMusicsByGenre(Musics, menuGenre);
        }

        /// <summary>
        /// The AllMusicButton_Click.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="RoutedEventArgs"/>.</param>
        private void AllMusicButton_Click(object sender, RoutedEventArgs e)
        {

            MusicManager.GetAllMusics(Musics);
            AllMusicButton.Visibility = Visibility.Collapsed;
        }

        
    }
}
