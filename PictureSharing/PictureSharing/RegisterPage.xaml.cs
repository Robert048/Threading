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
		private ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
		public RegisterPage()
		{
			this.InitializeComponent();
		}

		private async void registerbtn_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				ServiceReference1.User newUser = new ServiceReference1.User();
				newUser.GebruikerNaam = usernametxt.Text;
				newUser.GebruikersPW = passwordtxt.Password;

				await client.AddGebruikerAsync(newUser);

				var dialog = new MessageDialog("User: " + usernametxt.Text + " successfully created");
				await dialog.ShowAsync();
			}
			catch
			{
				var dialog = new MessageDialog("Fail");
				await dialog.ShowAsync();
			}
		}

		private void bk_Click(object sender, RoutedEventArgs e)
		{
			Frame.Navigate(typeof(LogInPage));
		}
	}
}
