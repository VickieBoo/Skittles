
using System;
using System.IO;
using SkittlesPower;
using System.Threading.Tasks;

namespace SkittlesApp {
    internal class Program {
        public static async Task Main(string[] args) {
            var files = new[] {
                "Images/pic1.jpeg", "Images/pic2.jpeg"
            };
            foreach (var file in files) {
                if (!File.Exists(file)) continue;
                var smart = await Skittles.SmartAsync(file);
                Console.WriteLine($"-= Results for '{file}' =-");
                Console.WriteLine($"Focused: {smart.Focused.ToHex()}");
                Console.WriteLine($"Secondary: {smart.Secondary.ToHex()}");
                Console.WriteLine($"Accent: {smart.Accent.ToHex()}");
            }
        }
    }
}