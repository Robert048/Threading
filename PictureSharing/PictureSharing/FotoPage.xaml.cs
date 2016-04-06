using System;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace PictureSharing
{
    /// <summary>
    /// Foto object weergave
    /// </summary>
    public sealed partial class FotoPage : Page
    {
        private const string noImage = "De afbeelding kon niet worden geladen.";
        public FotoPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Ontvang een Foto object en plaats deze in de DataContext
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is Foto)
            {
                this.DataContext = (Foto)e.Parameter;
            }
            else
            {
                // Ongeldig argument // terug naar overzicht 
                ShowDialog(noImage);
                Frame.Navigate(typeof(MainPage));
            }
        }

        /// <summary>
        /// Toon bericht als er geen afbeeling object is ontvangen
        /// </summary>
        private async void ShowDialog(String message)
        {
            MessageDialog dialog = new MessageDialog(message);
            await dialog.ShowAsync();
        }

        /// <summary>
        /// Terug naar pagina 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
            }
        }
    }
}
