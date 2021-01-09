using System;
using System.IO;
using System.Drawing;
using System.Threading.Tasks;
// ReSharper disable CheckNamespace

namespace SkittlesPower {
    public static partial class Skittles {
        // todo: add status code, file extensions, and content-type checks
        public static async Task<Bitmap> GetImageAsync(string source) {
            Bitmap image;
            if (File.Exists(source)) image = new Bitmap(source);
            else {
                using var response = await HTTP.Client.GetAsync(source);
                using var content = await response.Content.ReadAsStreamAsync();
                image = new Bitmap(content);
            }
            if (image == null) throw new Exception("Unable to get image");
            return image;
        }
    }
}