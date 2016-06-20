using PictureSharing.ServiceReference1;
using System;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace PictureSharing
{
    /// <summary>
    /// Loginpage, to login a account to the app
    /// </summary>
    public sealed partial class LogInPage : Page
	{
		private Service1Client client = new Service1Client();
		public static ServiceReference1.User user = new ServiceReference1.User();
        ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;

        // Initializing the Page.
        public LogInPage()
		{
			this.InitializeComponent();
		}

		/// <summary>
		/// Method for the login Button, logs in user and sends to mainpage.
		/// </summary>
		/// <param name="sender">Button object</param>
		/// <param name="e"></param>
		private async void loginbtn_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				user = await client.InlogMethodeAsync(usrTxt.Text, pswTxt.Password);
				User currentUser = new User() { gebruikersID = user.GebruikerID, gebruikersNaam = user.GebruikerNaam, gebruikersPW = user.GebruikersPW };
				localSettings.Values["currentUser"] = currentUser.gebruikersID;
				Frame.Navigate(typeof(MainPage));
			}
			catch (Exception ex)
			{
				var dialog = new MessageDialog("Niet gelukt in te loggen! Verkeerde gebruiker en/of wachtwoord!" + ex);
				await dialog.ShowAsync();
			}
		}

		/// <summary>
		/// Method for register button, goes to registerpage.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void createAccount_Click(object sender, RoutedEventArgs e)
		{
			Frame.Navigate(typeof(RegisterPage));
		}
	}
}