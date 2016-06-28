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
        private ObservableCollection<uploadIMG> uploadImages { get; set; }
        private ThreadingWebServiceClient client = new ThreadingWebServiceClient();
        ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
        private long userId;

        public UploadPage()
        {
            this.InitializeComponent();
            uploadImages = new ObservableCollection<uploadIMG>();
            uploadStatusListbox.ItemsSource = uploadImages;
            
            try
            {
                long currentUserID = (long)localSettings.Values["currentUser"];
                userId = currentUserID;
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

        

        /// <summary>
        /// Method for the clear button, Clears the screen and cancels all uploads that havent started yet
        /// </summary>
        /// <param name="sender">Button object</param>
        /// <param name="e"></param>
        private void clearBtn_Click(object sender, RoutedEventArgs e)
        {
            uploadImages.Clear();
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
            List<uploadIMG> requestList = new List<uploadIMG>();
            requestList = uploadImages.ToList();

            //While there are images in the uploadqueue
            while(requestList.Where(i => i.uploadstatus == "Added to queue").Any())
            {
                
                //Get the first item in the image list
                var image = uploadImages.FirstOrDefault(i => i.uploadstatus == "Added to queue");
                if (image != null)
                {
                    image.uploadstatus = "Uploading...";

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
        private async void MakePostRequest(uploadIMG image)
        {
            
#if DEBUG
            Debug.WriteLine(image.filename + " has started");
#endif
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
#if DEBUG
            Debug.WriteLine(image.filename + " is done");
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
    internal class uploadIMG : INotifyPropertyChanged
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
