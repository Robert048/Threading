using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

namespace PictureSharing
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private List<Foto> fotolijst = new List<Foto>();

        public MainPage()
        {
            this.InitializeComponent();
            Foto test = new Foto(1, "test", 1, "/test.png");
            fotolijst.Add(test);
        }
    }
}
