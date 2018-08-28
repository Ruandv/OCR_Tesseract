using System.IO;

namespace BusinessLayer
{
    public static class ExtensionMethods
    {
        public static Stream ToStream(this byte[] data)
        {
            return new MemoryStream(data);
        }
    }
}
