// ReSharper disable CheckNamespace poggers

using System;
using System.Linq;
using System.Drawing;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SkittlesPower {
    public static partial class Skittles {
        /// <summary>
        /// Gets the average color of an image
        /// </summary>
        /// <param name="source">File path or URL</param>
        /// <param name="width">New width of the image</param>
        /// <param name="height">New height of the image</param>
        public static async Task<Color> AverageAsync(string source, int width = 32, int height = 32)
            => Average(await GetImageAsync(source));
        
        public static Color Average(Bitmap image, int width = 32, int height = 32) {
            image = (Bitmap)image.GetThumbnailImage(width, height, () => false, IntPtr.Zero); // resizes image
            var colors = new List<Color>();
            // first for loop goes down the image after the second for loop has finished going left to right
            for (var h = 0; h < image.Height; h++) 
                for (var w = 0; w < image.Width; w++) {
                    var pixel = image.GetPixel(w, h);
                    if (pixel.IsEmpty || pixel.A < Tolerances.Transparency) continue;
                    colors.Add(pixel);
                }
            
            var R = colors.Select(color => (int)color.R).Sum() / colors.Count;
            var G = colors.Select(color => (int)color.G).Sum() / colors.Count;
            var B = colors.Select(color => (int)color.B).Sum() / colors.Count;
            return Color.FromArgb(R, G, B);
        }
    }
}