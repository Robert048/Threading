using System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace PictureSharing
{
    /// <summary>
    /// The Foto page. It shows a single foto.
    /// </summary>
    public sealed partial class FotoPage : Page
    {
        private const string noImage = "De afbeelding kon niet worden geladen.";
        public FotoPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets the Foto object if there is navigated to this page from the mainpage.
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
                // invalid argument and goes back to mainpage
                ShowDialog(noImage);
                Frame.Navigate(typeof(MainPage));
            }
        }

        /// <summary>
        /// Show the message in a dialogbox
        /// </summary>
        /// <param name="message">The message to display</param>
        private async void ShowDialog(String message)
        {
            MessageDialog dialog = new MessageDialog(message);
            await dialog.ShowAsync();
        }

        /// <summary>
        /// Method for back button. back to previouspage, which is mainpage.
        /// </summary>
        /// <param name="sender">button objeccct</param>
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
