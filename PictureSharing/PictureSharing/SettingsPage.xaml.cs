using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using PictureSharing.ServiceReference1;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace PictureSharing
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        private List<Foto> fotolijst { get; set; }
        private ServiceReference1.Service1Client client = new Service1Client();


        public SettingsPage()
        {
            this.InitializeComponent();
            getFotos();
        }

        private async void getFotos()
        {
            var tempList = await client.GetAllFotosByIdAsync(1);
           
            List<Foto> fotolijst = new List<Foto>();
            foreach (var item in tempList)
            {
                fotolijst.Add(new Foto() { fotoID = item.FotoID, fotoNaam = item.FotoNaam, gebruikersID = item.GebruikerID, path = item.Path });
            }         
            control.ItemsSource = fotolijst;
        }

        private void btnSettings_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Frame.Navigate(typeof(UploadPage));
        }

        private void btnRefresh_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
        private async void button_Click(object sender, RoutedEventArgs e)
        {     
            var myValue = (long)((Button)sender).Tag;                  
            await client.DeleteFotoAsync(myValue);
            getFotos();         
        }
    }
}
