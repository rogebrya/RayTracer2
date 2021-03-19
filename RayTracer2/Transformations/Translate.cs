using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RayTracer2 {
    public class Translate : ITransformation {
        private double x;
        private double y;
        private double z;
        [JsonInclude]
        private string transformType = "Translate";

        public Translate() {
            x = 0;
            y = 0;
            z = 0;
        }

        public Translate(double translateX, double translateY, double translateZ) {
            x = translateX;
            y = translateY;
            z = translateZ;
        }

        public double X {
            get { return x; }
            set { x = value; }
        }

        public double Y {
            get { return y; }
            set { y = value; }
        }

        public double Z {
            get { return z; }
            set { z = value; }
        }

        public override string ToString() {
            return transformType + " X: " + x + " Y: " + y + " Z: " + z + Environment.NewLine;
        }

        public Matrix GetTransform() {
            Matrix m = Matrix.GetIdentityMatrix();
            m.GetMatrix[0, 3] = x;
            m.GetMatrix[1, 3] = y;
            m.GetMatrix[2, 3] = z;
            return m;
        }
    }
}
