// ReSharper disable CheckNamespace

using System.Linq;
using System.Drawing;
using System.Globalization;

namespace SkittlesPower {
    public static partial class Skittles {
        public static string ToHex(this Color c, bool hash = true)
            => $"{(hash ? "#" : "")}{c.R:X2}{c.G:X2}{c.B:X2}";
        public static double Value(this Color c)
            => int.Parse(c.ToHex(false), NumberStyles.HexNumber);
        
        /// <summary>
        /// Finds the difference between 2 colors (needs improvements but has worked so far)
        /// </summary>
        /// <param name="c1">First color</param>
        /// <param name="c2">Second color</param>
        /// <returns></returns>
        public static double Difference(this Color c1, Color c2) {
            var values = new[] {c1.Value(), c2.Value()}.OrderByDescending(_=>_).ToArray();
            var difference = (values[0] - values[1]) / values.Sum();
            return difference switch {
                double.NaN => -1, 
                double.PositiveInfinity => -1, 
                _ => difference
            };
        }
    }
}