using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using PictureSharing.WebServiceReference;

namespace PictureSharing
{
    /// <summary>
    /// PictureSharing main page with picture view
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private List<LocalPhoto> PhotoList { get; set; }
        private ThreadingWebServiceClient _client = new ThreadingWebServiceClient();

        public MainPage()
        {
            this.InitializeComponent();
            GetPhotosFromService();
        }

        /// <summary>
        /// Method to get the foto's from service for the mainpage
        /// connects with service to get fotos, loops thru and adds fotos to the fotolist. after loop it adds the list to the itemsource of the itemcontrol
        /// </summary>
        private async void GetPhotosFromService()
        {
            var photoServicesList = await _client.GetAllPhotosAsync();
            PhotoList = new List<LocalPhoto>();
            foreach (var servicePhoto in photoServicesList)
            {
                PhotoList.Add(new LocalPhoto()
                {
                    Id = servicePhoto.PhotoId,
                    Name = servicePhoto.PhotoName,
                    UserId = servicePhoto.UserId,
                    Path = servicePhoto.Path,
                    ImageData = servicePhoto.ImageData
                });
            }
            control.ItemsSource = PhotoList;
        }

        /// <summary>
        /// Method for the settings button. Goes to the SettingsPage
        /// </summary>
        /// <param name="sender">button object</param>
        /// <param name="e"></param>
        private void btnSettings_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SettingsPage));
        }

        /// <summary>
        /// Method for the refresh button. Calls getFotos() to refresh the fotolist.
        /// </summary>
        /// <param name="sender">button object</param>
        /// <param name="e"></param>
        private void btnRefresh_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            GetPhotosFromService();
        }

        /// <summary>
        /// Click method for the individual foto's. Gets selected foto and send it to FotoPage and goes to Fotopage.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TemplateClick(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            LocalPhoto selectedLocalPhoto = (LocalPhoto)control.SelectedItem;
            Frame.Navigate(typeof(PhotoFullScreenPage), selectedLocalPhoto);
        }
    }
}
