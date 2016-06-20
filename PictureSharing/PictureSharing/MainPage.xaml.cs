using PictureSharing.ServiceReference1;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

namespace PictureSharing
{
    /// <summary>
    /// PictureSharing main page with picture view
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private List<Foto> fotolijst { get; set; }
        private Service1Client client = new Service1Client();

        public MainPage()
        {
            this.InitializeComponent();
            getFotos();
        }

        /// <summary>
        /// Method to get the foto's from service for the mainpage
        /// </summary>
        private async void getFotos()
        {
            var fotoServicesList = await client.GetAllFotosAsync();
            fotolijst = new List<Foto>();
            foreach (var foto in fotoServicesList)
            {
                fotolijst.Add(new Foto() { fotoID = foto.FotoID, fotoNaam = foto.FotoNaam, gebruikersID = foto.GebruikerID, path = foto.Path });
            }
            fotolijst.Add(new Foto() {fotoNaam = "naam"});
            control.ItemsSource = fotolijst;
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
            getFotos();
        }

        /// <summary>
        /// Click method for the individual foto's. Gets selected foto and send it to FotoPage and goes to Fotopage.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void templateClick(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            Foto selectedFoto = (Foto)control.SelectedItem;
            Frame.Navigate(typeof(FotoPage), selectedFoto);
        }
    }
}
