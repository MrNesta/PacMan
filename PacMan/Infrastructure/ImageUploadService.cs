using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace PacMan.Infrastructure
{
    public class ImageUploadService
    {
        public BitmapImage GetImage(string filePath)
        {           
            if (filePath == null)
            {
                return null;
            }

            var image = new BitmapImage();

            try
            {                
                image.BeginInit();
                var uri = new Uri(filePath, UriKind.Relative);
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.UriSource = uri;
                image.EndInit();
            }
            catch (InvalidOperationException exc)
            {
                LogService.SaveToLog(exc.Message);
                MessageBox.Show("Error accessing images resources");
                throw exc;
            }              

            return image;
        }
    }
}
