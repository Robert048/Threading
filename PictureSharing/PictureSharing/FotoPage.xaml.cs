using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace PictureSharing
{
    /// <summary>
    /// Display the image
    /// </summary>
    public sealed partial class FotoPage : Page
    {
        public FotoPage()
        {
            this.InitializeComponent();

            // TEMP
            Foto temp = new Foto() {
                fotoNaam = "Deze moet vervangen worden!",
                path = "http://bartvanas.nl/_Media/_mg_1102.jpeg"
            };

            this.DataContext = temp;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is Foto)
            {
                this.DataContext = (Foto)e.Parameter;
            }
            else
            {
                // Invalid Argument, return to MainPage?
                ShowDialog();
                Frame.Navigate(typeof(MainPage));
            }
        }

        private void PreviousHistory()
        {
            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
            }
        }

        private async void ShowDialog()
        {
            var dialog = new MessageDialog("Afbeelding niet gevonden. Keer terug naar overzicht.");
            await dialog.ShowAsync();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            PreviousHistory();
        }
    }
}
