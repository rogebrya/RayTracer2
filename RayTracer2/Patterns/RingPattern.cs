using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer2 {
    public class RingPattern : Pattern {
        public RingPattern() {
            PatternType = "Ring Pattern";
        }

        public RingPattern(Color a, Color b) {
            A = a;
            B = b;
            PatternType = "Ring Pattern";
        }

        public override Color PatternAt(Tuple point) {
            if (Math.Floor(Math.Sqrt(Math.Pow(point.X, 2) + Math.Pow(point.Z, 2))) % 2 == 0) {
                return A;
            } else {
                return B;
            }
        }
    }
}
