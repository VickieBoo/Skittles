// ReSharper disable CheckNamespace poggers

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace SkittlesPower {
    public partial class Skittles {
        public class SmartResult {
            public Color Focused;
            public Color Secondary;
            public Color Accent;
            public Bitmap Source;  // unresized source image
        }
        
        /// <summary>
        /// Gets the colors from an image in a "smart" way (compared to previous versions)
        /// </summary>
        /// <param name="source">File path or URL to image</param>
        /// <param name="width">New width of the image</param>
        /// <param name="height">New height of the image</param>
        public static async Task<SmartResult> SmartAsync(string source, int width = 32, int height = 32)
            => Smart(await GetImageAsync(source));
        
        /// <summary>
        /// Gets the colors from an image in a "smart" way (compared to previous versions)
        /// </summary>
        /// <param name="image">The image to get colors from</param>
        /// <param name="width">New width of the image</param>
        /// <param name="height">New height of the image</param>
        public static SmartResult Smart(Bitmap image, int width = 32, int height = 32) {
            var result = new SmartResult { Source = image };
            image = (Bitmap)image.GetThumbnailImage(width, height, () => false, IntPtr.Zero); // resizes image
            var colors = new List<Color>();
            // first for loop goes down the image after the second for loop has finished going left to right
            for (var h = 0; h < image.Height; h++) 
                for (var w = 0; w < image.Width; w++) {
                    var pixel = image.GetPixel(w, h);
                    if (pixel.IsEmpty || pixel.A < Tolerances.Transparency) continue;
                    colors.Add(pixel);
                }
            
            var commons = colors.GroupBy(color => color).OrderByDescending(group => group.Count())
                .SelectMany(color => color).ToList();
            
            var groups = new List<ColorGroup>();
            foreach (var common in commons) {
                // find the parent that's similar to the current color
                var group = groups.FirstOrDefault(group => group.Parent.Difference(common) <= Tolerances.Similarity);
                // if the parent doesn't exist make the current color a parent
                if (group == null) groups.Add(new ColorGroup {Parent = common});
                else group.Add();
            }

            groups = groups.OrderByDescending(group => group.Children).ToList();
            try { // in a try/catch in case there's a color missing
                result.Focused = groups[0].Parent;
                result.Secondary = groups[1].Parent;
                result.Accent = groups[2].Parent;
            } catch { /* ignore */ }
            
            return result;
        }
    }
}