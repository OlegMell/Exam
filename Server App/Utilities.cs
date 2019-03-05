using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_App
{
    public static class Utilities
    {
        private static int id = 0;
        private const string ROOT = @"..\Debug\Profile Images\";
        public static string ConvertToImage(string name, byte[] data)
        {
            string path = ROOT + $"{++id}" + name;
            //await Task.Factory.StartNew(async() =>
            //{
                using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
                {
                   fs.Write(data, 0, data.Length);
                }
            //});
            return path;
        }
    }
}
