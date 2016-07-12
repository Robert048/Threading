using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace PictureSharing.Converter
{
    /// <summary>
    /// Binding converter to convert the image byte to an actual image
    /// </summary>
    public class ByteArrayToImageConverter : IValueConverter
    {
        /// <summary>
        /// Convert the binding value (byte array) to an BitmapImage
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (!(value is byte[]))
            {
                return null;
            }

            // use a in-memory stream and write the bytes to the stream with a datawriter
            using (InMemoryRandomAccessStream memoryStream = new InMemoryRandomAccessStream())
            {
                using (DataWriter writer = new DataWriter(memoryStream.GetOutputStreamAt(0)))
                {
                    writer.WriteBytes((byte[])value);
                    // Data value converter have to be sync, so wait for the result.
                    writer.StoreAsync().GetResults();
                }

                // set the datastream to the image
                var image = new BitmapImage();
                image.SetSource(memoryStream);
                return image;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            // not needed, only neccessary to Convert
            throw new NotImplementedException();
        }
    }
}
