using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer2 {
    public class Scale : ITransformation {
        private double x;
        private double y;
        private double z;
        private string transformType = "Scale";

        public Scale(double scaleX, double scaleY, double scaleZ) {
            x = scaleX;
            y = scaleY;
            z = scaleZ;
        }

        public override string ToString() {
            return transformType + " X: " + x + " Y: " + y + " Z: " + z + Environment.NewLine;
        }

        public Matrix GetTransform() {
            Matrix m = Matrix.GetIdentityMatrix();
            m.GetMatrix[0, 0] = x;
            m.GetMatrix[1, 1] = y;
            m.GetMatrix[2, 2] = z;
            return m;
        }
    }
}
