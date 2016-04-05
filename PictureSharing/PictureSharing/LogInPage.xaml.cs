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

		public LogInPage()
		{
			this.InitializeComponent();
		}

		private async void loginbtn_Click(object sender, RoutedEventArgs e)
		{
			try {
				user = await login.InlogMethodeAsync(usrTxt.Text, pswTxt.Password);
			}
			catch
			{
				var dialog = new MessageDialog("lol faal");
				await dialog.ShowAsync();
			}
		}

		private async void forgotPassword_Click(object sender, RoutedEventArgs e)
		{
			var dialog = new MessageDialog("Je bent een mogool");
			await dialog.ShowAsync();

		}

		private void createAccount_Click(object sender, RoutedEventArgs e)
		{
			Frame.Navigate(typeof(RegisterPage));
		}


	}
}
