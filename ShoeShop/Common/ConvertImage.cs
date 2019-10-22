using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace ShoeShop.Common
{
    public class ConvertImage
    {
        public static string ImageToString(HttpPostedFileBase image)
        {
            int width = 150;
            int height = 150;

            var source = Image.FromStream(image.InputStream, true, true);
            var result = (Bitmap)ResizeImageKeepAspectRatio(source, width, height);
            result.Save("../../Resources/TempFiles/avatar.jpg");

            byte[] imageArray = System.IO.File.ReadAllBytes("../../Resources/TempFiles/avatar.jpg");
            return Convert.ToBase64String(imageArray);
        }

        private static Bitmap ResizeImageKeepAspectRatio(Image source, int width, int height)
        {
            throw new NotImplementedException();
        }
    }
}