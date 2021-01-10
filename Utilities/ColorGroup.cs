// ReSharper disable CheckNamespace

using System.Drawing;

namespace SkittlesPower {
    public class ColorGroup {
        public Color Parent;
        public int Children;
        public void Add() => Children += 1;
    }
}