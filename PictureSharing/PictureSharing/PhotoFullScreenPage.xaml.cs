using System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace PictureSharing
{
    /// <summary>
    /// The PhotoFullScreenPage. It shows the single foto.
    /// </summary>
    public sealed partial class PhotoFullScreenPage : Page
    {
        private const string NoImage = "De afbeelding kon niet worden geladen.";
        public PhotoFullScreenPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Load the Photo in the frame
        /// </summary>
        /// <param name="e">Needs to be a LocalPhoto obj</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var photo = e.Parameter as LocalPhoto;
            if (photo != null)
            {
                DataContext = photo;
            }
            else
            {
                // invalid argument and goes back to mainpage
                ShowDialog(NoImage);
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
        /// Back to previouspage, which is mainpage.
        /// </summary>
        /// <param name="sender">button object</param>
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
