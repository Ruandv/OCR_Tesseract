using Newtonsoft.Json;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace BusinessLayer
{
    public static class ExtensionMethods
    {
        public static Rectangle[] ToRecs(this string data)
        {
            return JsonConvert.DeserializeObject<Rectangle[]>(data);
        }

        public static Stream ToStream(this byte[] data)
        {
            return new MemoryStream(data);
        }

        public static byte[] ToByteArray(this Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, ImageFormat.Bmp);
            return ms.ToArray();
        }

        public static Image ToImage(this byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }
    }
}
