// ReSharper disable CheckNamespace

using System;
using System.Linq;
using System.Drawing;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SkittlesPower {
    public partial class Skittles {
        /// <summary>
        /// Gets the most common color of an image
        /// </summary>
        /// <param name="source">File path or URL</param>
        /// <param name="width">New width of the image</param>
        /// <param name="height">New height of the image</param>
        public static async Task<Color> CommonAsync(string source, int width = 32, int height = 32)
            => Common(await GetImageAsync(source));
        
        public static Color Common(Bitmap image, int width = 32, int height = 32) {
            image = (Bitmap)image.GetThumbnailImage(width, height, () => false, IntPtr.Zero); // resizes image
            var colors = new List<Color>();
            // first for loop goes down the image after the second for loop has finished going left to right
            for (var h = 0; h < image.Height; h++) 
                for (var w = 0; w < image.Width; w++) {
                    var pixel = image.GetPixel(w, h);
                    if (pixel.IsEmpty || pixel.A < Tolerances.Transparency) continue;
                    colors.Add(pixel);
                }
            var commons = colors.GroupBy(color => color).OrderByDescending(group => group.Count()).SelectMany(color => color).ToList();
            return commons[0];
        }
    }
}