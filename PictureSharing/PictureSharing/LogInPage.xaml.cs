using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class LogInPage : Page
	{
		private ServiceReference1.Service1Client login = new ServiceReference1.Service1Client();
		public static ServiceReference1.User user = new ServiceReference1.User();
		private ServiceReference1.User currentUser;

		// Initializing the Page.
		public LogInPage()
		{
			this.InitializeComponent();
		}

		/// <summary>
		///  Methode voor button inloggen.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private async void loginbtn_Click(object sender, RoutedEventArgs e)
		{
			try {
				user = await login.InlogMethodeAsync(usrTxt.Text, pswTxt.Password);
				Frame.Navigate(typeof(MainPage),user);
				currentUser = user;
			}
			catch(Exception ex)
			{
				var dialog = new MessageDialog("Niet gelukt in te loggen! Verkeerde gebruiker en/of wachtwoord!" + ex);
				await dialog.ShowAsync();
			}
		}

		/// <summary>
		/// Methode voor klikken op register, refereert naar de Register Page.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void createAccount_Click(object sender, RoutedEventArgs e)
		{
			Frame.Navigate(typeof(RegisterPage));
		}

		/// <summary>
		/// Gets current logged in User
		/// </summary>
		/// <returns></returns>
		public ServiceReference1.User getUser()
		{
			return currentUser;
		}
	}
}