using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ImageViewer
{
    class ImageConvertor
    {
        public static byte[] ConvertImageIntoByteArray(BitmapImage bitmapImage)
        {
            byte[] imageIntoByteArray;
            BitmapEncoder jpegEncoder = new JpegBitmapEncoder();
            jpegEncoder.Frames.Add(BitmapFrame.Create(bitmapImage));
            using (MemoryStream memoryStream = new MemoryStream())
            {
                jpegEncoder.Save(memoryStream);
                imageIntoByteArray = memoryStream.ToArray();
            }
            return imageIntoByteArray;
        }
        public static BitmapImage ConvertByteArrayIntoBitmapImage(byte[] byteArray)
        {
            if (byteArray == null || byteArray.Length == 0) return null;
            BitmapImage bitmapImage = new BitmapImage();
            using (var memoryStream = new MemoryStream(byteArray))
            {
                memoryStream.Position = 0;
                bitmapImage.BeginInit();
                bitmapImage.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.UriSource = null;
                bitmapImage.StreamSource = memoryStream;
                bitmapImage.EndInit();
            }
            bitmapImage.Freeze();
            return bitmapImage;
        }
    }
}
