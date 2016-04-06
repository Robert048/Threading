using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
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
    public sealed partial class UploadPage : Page
    {
        public ObservableCollection<uploadIMG> uploadImages { get; set; }
        private ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
        ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
        private long userId;

        public UploadPage()
        {
            this.InitializeComponent();
            
            uploadImages = new ObservableCollection<uploadIMG>();
            uploadStatusListbox.ItemsSource = uploadImages;

            try
            {
                User currentuser = (User)localSettings.Values["currentuser"];
                userId = currentuser.gebruikersID;
            }
            catch(Exception)
            {
                Frame.Navigate(typeof(LogInPage));
            }

        }

        private async void openBtn_Click(object sender, RoutedEventArgs e)
        {
            //Create a filepicker
            FileOpenPicker openPicker = new FileOpenPicker();

            //Add settings to the filepicker
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".png");
            
            //We have to wait until the user has decided which images he wants to upload, if any
            IReadOnlyList<StorageFile> filelist = await openPicker.PickMultipleFilesAsync();


            foreach (var file in filelist)
            {
                var randomAccessStream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read);

                Stream managedStream = randomAccessStream.AsStream();
                var newImage = new uploadIMG();
                
                //Check if the image size is less than 1 MB
                if(managedStream.Length > 1048576)
                {
                    newImage.uploadstatus = "Image exceeds sizelimit";
                }
                else
                {
                    newImage.imageStream = ReadFully(managedStream);
                    newImage.uploadstatus = "Added to queue";
                }
                
                newImage.filename = file.Name;

                uploadImages.Add(newImage);
            }
        }

        //Clear the screen and cancels all uploads that havent started yet
        private void clearBtn_Click(object sender, RoutedEventArgs e)
        {
            uploadImages.Clear();
        }

        //Return to the overview
        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SettingsPage));
        }


        private async void uploadBtn_Click(object sender, RoutedEventArgs e)
        {
            List<uploadIMG> requestList = new List<uploadIMG>();
            requestList = uploadImages.ToList();

            //While there are images in the uploadqueue
            while(requestList.Where(i => i.uploadstatus == "Added to queue").Any())
            {
                //Get the first item in the image list
                var image = uploadImages.FirstOrDefault(i => i.uploadstatus == "Added to queue");
                if (image != null)
                {
                    //Change the status of the image
                    image.uploadstatus = "Uploading...";

                    //and start the upload request
                    image.uploadstatus = await MakePostRequest(image);
                }
            }
        }
        
        private async Task<string> MakePostRequest(uploadIMG image)
        {
            
            //Try to upload the image
            try
            {
                image.uploadstatus = await client.UploadFotoAsync(image.filename, image.imageStream, userId);
            }
            catch(Exception e)
            {
                //Upload has failed, we want to know why
                image.uploadstatus = e.Message;
            }


            return (image.uploadstatus);
        }



        //converts a Windows RandomAccessStream to a byte array 
        public static byte[] ReadFully(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }      
    }

    //The item used to display all the chosen images and their statuses
    public class uploadIMG : INotifyPropertyChanged
    {
        private string _uploadstatus;
        public string uploadstatus
        {
            get { return _uploadstatus; }
            set {
                    _uploadstatus = value;
                    OnPropertyChanged("uploadstatus");
                }
        }

        public byte[] imageStream { get; set; }
        
        public string filename { get; set; }

        //An interface with a propertyeventhandler had to be added to be able to see the changes immediatly
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}
