using PictureSharing.ServiceReference1;
using System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


namespace PictureSharing
{
    /// <summary>
    /// Registerpage, to register a new account
    /// </summary>
    public sealed partial class RegisterPage : Page
	{
		/// <summary>
		/// Creates Service Client
		/// </summary>
		private Service1Client client = new Service1Client();

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
		/// <param name="sender">Button object</param>
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
		/// <param name="sender">Button object</param>
		/// <param name="e"></param>
		private void bk_Click(object sender, RoutedEventArgs e)
		{
			Frame.Navigate(typeof(LogInPage));
		}
	}
}