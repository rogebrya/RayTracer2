using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer2 {
    public static class Globals {
        public const double EPSILON = 0.0001;
        public const string prepend = "   ";
        
        public static bool EqualityOfDouble(double a, double b) {
            return Math.Abs(a - b) < Globals.EPSILON;
        }

        public static string IndentString(string str) {
            string prepend = "    ";
            str = str.Replace("\n", "\n" + prepend);
            str = prepend + str;
            return str;
        }
    }
}
