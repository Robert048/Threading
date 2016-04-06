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
using Windows.Storage;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace PictureSharing
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {

        // gets the list of foto's and set the client
        private List<Foto> fotolijst { get; set; }
        private ServiceReference1.Service1Client client = new Service1Client();
        ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

        public SettingsPage()
        {
            this.InitializeComponent();

            // load page with fotos
            getFotos();
        }

        //method to get fotos
        private async void getFotos()
        {

            try {
                User user = (User)localSettings.Values["currentuser"];

                // get all foto's by user ID           
                var tempList = await client.GetAllFotosByIdAsync(user.gebruikersID);

                List<Foto> fotolijst = new List<Foto>();
                //adds foto's found to the list
                foreach (var item in tempList)
                {
                    fotolijst.Add(new Foto() { fotoID = item.FotoID, fotoNaam = item.FotoNaam, gebruikersID = item.GebruikerID, path = item.Path });
                }
                // bind control with fotolijst
                control.ItemsSource = fotolijst;
            }
            catch(Exception)
            {
                Frame.Navigate(typeof(LogInPage));
            }
            
        }

        //go to the uploadpage
        private void btnSettings_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Frame.Navigate(typeof(UploadPage));
        }

        // go to the mainpage
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
