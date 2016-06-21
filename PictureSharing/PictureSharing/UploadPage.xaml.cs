using PictureSharing.WebServiceReference;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace PictureSharing
{
    /// <summary>
    /// Upload page to upload new images
    /// </summary>
    public sealed partial class UploadPage : Page
    {
        private ObservableCollection<UploadImg> UploadImages { get; set; }
        private ThreadingWebServiceClient _client = new ThreadingWebServiceClient();
        private ApplicationDataContainer _localSettings = ApplicationData.Current.LocalSettings;
        private long _userId;

        public UploadPage()
        {
            this.InitializeComponent();
            UploadImages = new ObservableCollection<UploadImg>();
            uploadStatusListbox.ItemsSource = UploadImages;
            
            try
            {
                long currentUserId = (long)_localSettings.Values["currentUser"];
                _userId = currentUserId;
            }
            catch(Exception)
            {
                Frame.Navigate(typeof(LogInPage));
            } 
        }

        /// <summary>
        /// Method for the open button, to open filepicker
        /// </summary>
        /// <param name="sender">Button object</param>
        /// <param name="e"></param>
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
                var newImage = new UploadImg();
                
                //Check if the image size is less than 1 MB
                if(managedStream.Length > 1048576)
                {
                    newImage.Uploadstatus = "Image exceeds sizelimit";
                }
                else
                {
                    newImage.ImageStream = ReadFully(managedStream);
                    newImage.Uploadstatus = "Added to queue";
                }
                
                newImage.Filename = file.Name;
                UploadImages.Add(newImage);
            }
        }

        

        /// <summary>
        /// Method for the clear button, Clears the screen and cancels all uploads that havent started yet
        /// </summary>
        /// <param name="sender">Button object</param>
        /// <param name="e"></param>
        private void clearBtn_Click(object sender, RoutedEventArgs e)
        {
            UploadImages.Clear();
        }

        /// <summary>
        /// Method for the back button, Returns to the settingspage
        /// </summary>
        /// <param name="sender">Button object</param>
        /// <param name="e"></param>
        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SettingsPage));
        }

        /// <summary>
        /// Method for the upload button. uploads the image
        /// </summary>
        /// <param name="sender">Button object</param>
        /// <param name="e"></param>
        private void uploadBtn_Click(object sender, RoutedEventArgs e)
        {
            List<UploadImg> requestList = UploadImages.ToList();

            //While there are images in the uploadqueue
            while(requestList.Any(i => i.Uploadstatus == "Added to queue"))
            {
                
                //Get the first item in the image list
                var image = UploadImages.FirstOrDefault(i => i.Uploadstatus == "Added to queue");
                if (image != null)
                {
                    image.Uploadstatus = "Uploading...";

                    //Start the task
                    MakePostRequest(image);
                }
            }
        }
        
        /// <summary>
        /// Method to upload the image with the service
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        private async void MakePostRequest(UploadImg image)
        {
            
#if DEBUG
            Debug.WriteLine(image.Filename + " has started");
#endif
            //Try to upload the image
            try
            {
                image.Uploadstatus = await _client.UploadFotoAsync(image.Filename, image.ImageStream, _userId);
            }
            catch(Exception e)
            {
                //Upload has failed, we want to know why
                image.Uploadstatus = e.Message;
            }
#if DEBUG
            Debug.WriteLine(image.Filename + " is done");
#endif
        }


        /// <summary>
        /// converts a Windows RandomAccessStream to a byte array 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static byte[] ReadFully(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }      
    }

    /// <summary>
    /// The item used to display all the chosen images and their statuses
    /// </summary>
    internal class UploadImg : INotifyPropertyChanged
    {
        private string _uploadstatus;
        public string Uploadstatus
        {
            get { return _uploadstatus; }
            set {
                    _uploadstatus = value;
                    OnPropertyChanged("uploadstatus");
                }
        }
        
        public byte[] ImageStream { get; set; }
        
        public string Filename { get; set; }

        //An interface with a propertyeventhandler had to be added to be able to see the changes immediatly
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
