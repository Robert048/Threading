using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Storage;
using PictureSharing.WebServiceReference;


namespace PictureSharing
{
    /// <summary>
    /// Settingspage to adjust the settings of the app
    /// </summary>
    public sealed partial class SettingsPage : Page
    {

        // gets the list of foto's and set the client
        private List<Foto> Fotolijst { get; set; }
        private ThreadingWebServiceClient _client = new ThreadingWebServiceClient();
        ApplicationDataContainer _localSettings = ApplicationData.Current.LocalSettings;

        public SettingsPage()
        {
            this.InitializeComponent();

            // load page with fotos
            GetFotos();
        }

        /// <summary>
        /// method to get fotos
        /// </summary>
        private async void GetFotos()
        {
            try {
                long userId = (long)_localSettings.Values["currentUser"];

                // get all foto's by user ID           
                var fotoServicesList = await _client.GetAllFotosByIdAsync(userId);

                Fotolijst = new List<Foto>();
                //adds foto's found to the list
                foreach (var foto in fotoServicesList)
                {
                    Fotolijst.Add(new Foto() { fotoID = foto.FotoID, fotoNaam = foto.FotoNaam, gebruikersID = foto.GebruikerID, path = foto.Path });
                }
                // bind control with fotolijst
                control.ItemsSource = Fotolijst;
            }
            catch(Exception)
            {
                Frame.Navigate(typeof(LogInPage));
            }
        }

        /// <summary>
        /// Method for the delete button, deletes the foto and reloads the list.
        /// </summary>
        /// <param name="sender">Button object</param>
        /// <param name="e"></param>
        private async void button_Click(object sender, RoutedEventArgs e)
        {
            var myValue = (long)((Button)sender).Tag;
            await _client.DeleteFotoAsync(myValue);
            GetFotos();
        }

        /// <summary>
        /// Method for the upload button, goes to the upload page.
        /// </summary>
        /// <param name="sender">Button object</param>
        /// <param name="e"></param>
        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(UploadPage));
        }

        /// <summary>
        /// Method for the back button, goes to the mainpage
        /// </summary>
        /// <param name="sender">Button object</param>
        /// <param name="e"></param>
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
    }
}
