using PictureSharing.ServiceReference1;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using System;

namespace PictureSharing
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
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

        private async void getFotos()
        {
            var tempList = await client.GetAllFotosAsync();
            foreach (var item in tempList)
            {
                fotolijst.Add(new Foto() { fotoID = item.FotoID, fotoNaam = item.FotoNaam, gebruikersID = item.GebruikerID, path = item.Path });
            }
            fotolijst.Add(new Foto() { fotoID = 1, fotoNaam = "naam", gebruikersID = 1, path = "f" });
            control.ItemsSource = fotolijst;
        }

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
            Frame.Navigate(typeof(SettingsPage));
        }
    }
}
