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
        private ServiceReference1.Service1Client client = new Service1Client();

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
            var tempList = await client.GetAllFotosAsync();
            fotolijst = new List<Foto>();
            foreach (var item in tempList)
            {
                fotolijst.Add(new Foto() { fotoID = item.FotoID, fotoNaam = item.FotoNaam, gebruikersID = item.GebruikerID, path = item.Path });
            }
            fotolijst.Add(new Foto() {fotoNaam = "naam"});
            control.ItemsSource = fotolijst;
        }

        //Click methods for the buttons and fotoviews

        private void btnSettings_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SettingsPage));
        }

        private void btnRefresh_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            getFotos();
        }

        private void templateClick(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            Foto selectedFoto = (Foto)control.SelectedItem;
            Frame.Navigate(typeof(FotoPage), selectedFoto);
        }
    }
}
