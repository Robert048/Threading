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
	public sealed partial class RegisterPage : Page
	{
		/// <summary>
		/// Creates Service Client
		/// </summary>
		private ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();

		/// <summary>
		/// Initializes Page
		/// </summary>
		public RegisterPage()
		{
			this.InitializeComponent();
		}

		/// <summary>
		/// Method for button to Register new User.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private async void registerbtn_Click(object sender, RoutedEventArgs e)
		{
			if (!String.IsNullOrWhiteSpace(passwordtxt.Password) && !String.IsNullOrWhiteSpace(usernametxt.Text))
			{
				bool check = false;

				var gebruikers = await client.GetAllGebruikersAsync();
				foreach (var user in gebruikers)
				{
					if (usernametxt.Text == user.GebruikerNaam)
					{
						check = true;
					}
				}

				if (!check)
				{
					ServiceReference1.User newUser = new ServiceReference1.User();
					newUser.GebruikerNaam = usernametxt.Text;
					newUser.GebruikersPW = passwordtxt.Password;

					await client.AddGebruikerAsync(newUser);

					var dialogCreated = new MessageDialog("User: " + usernametxt.Text + " successfully created");
					await dialogCreated.ShowAsync();
				}
				else
				{
					var dialog = new MessageDialog("Username bestaat al");
					await dialog.ShowAsync();
				}
			}
			else
			{
				var dialog = new MessageDialog("Velden mogen niet leeg zijn");
				await dialog.ShowAsync();
			}
		}

		/// <summary>
		/// Method for button click to return to login page.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bk_Click(object sender, RoutedEventArgs e)
		{
			Frame.Navigate(typeof(LogInPage));
		}
	}
}