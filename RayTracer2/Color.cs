using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace RayTracer2 {
    public class Color : IEquatable<Color> {
        private double red;
        private double green;
        private double blue;

        public Color(double r, double g, double b) {
            red = r;
            green = g;
            blue = b;
        }

        public double Red {
            get { return red; }
            set { red = value; }
        }

        public double Green {
            get { return green; }
            set { green = value; }
        }

        public double Blue {
            get { return blue; }
            set { blue = value; }
        }

        public static Color operator +(Color t1, Color t2) {
            return new Color((t1.Red + t2.Red), (t1.Green + t2.Green), (t1.Blue + t2.Blue));
        }

        public static Color operator -(Color t1, Color t2) {
            return new Color((t1.Red - t2.Red), (t1.Green - t2.Green), (t1.Blue - t2.Blue));
        }

        public static Color operator *(Color t1, Color t2) {
            return new Color(t1.Red * t2.Red, t1.Green * t2.Green, t1.Blue * t2.Blue);
        }

        public static Color operator *(Color t1, double d1) {
            return new Color(d1 * t1.Red, d1 * t1.Green, d1 * t1.Blue);
        }

        // Equality Stuff
        public static bool operator ==(Color t1, Color t2) {
            return Globals.EqualityOfDouble(t1.red, t2.red) && Globals.EqualityOfDouble(t1.green, t2.green) && Globals.EqualityOfDouble(t1.blue, t2.blue);
        }

        public static bool operator !=(Color t1, Color t2) {
            return !Globals.EqualityOfDouble(t1.red, t2.red) || !Globals.EqualityOfDouble(t1.green, t2.green) || !Globals.EqualityOfDouble(t1.blue, t2.blue);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Color)obj);
        }

        public override int GetHashCode() {
            unchecked {
                return (int)(Math.Pow((red * 397), green) + blue);
            }
        }

        public bool Equals(Color other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Globals.EqualityOfDouble(red, other.red) && Globals.EqualityOfDouble(green, other.green) && Globals.EqualityOfDouble(blue, other.blue);
        }
    }
}
