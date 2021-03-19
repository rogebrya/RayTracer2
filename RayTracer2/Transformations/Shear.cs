using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RayTracer2 {
    public class Shear : ITransformation {
        private double x_y;
        private double x_z;
        private double y_x;
        private double y_z;
        private double z_x;
        private double z_y;
        [JsonInclude]
        private string transformType = "Shear";

        public Shear() {
            x_y = 0;
            x_z = 0;
            y_x = 0;
            y_z = 0;
            z_x = 0;
            z_y = 0;
        }

        public Shear(double shearXY, double shearXZ, double shearYX, double shearYZ, double shearZX, double shearZY) {
            x_y = shearXY;
            x_z = shearXZ;
            y_x = shearYX;
            y_z = shearYZ;
            z_x = shearZX;
            z_y = shearZY;
        }

        public double XY {
            get { return x_y; }
            set { x_y = value; }
        }

        public double XZ {
            get { return x_z; }
            set { x_z = value; }
        }

        public double YX {
            get { return y_x; }
            set { y_x = value; }
        }

        public double YZ {
            get { return y_z; }
            set { y_z = value; }
        }

        public double ZX {
            get { return z_x; }
            set { z_x = value; }
        }

        public double ZY {
            get { return z_y; }
            set { z_y = value; }
        }

        public override string ToString() {
            return transformType + " X_Y: " + x_y + " X_Z: " + x_z + " Y_X: " + y_x + " Y_Z: " + y_z + " Z_X: " + z_x + " Z_Y: " + z_y + Environment.NewLine;
        }

        public Matrix GetTransform() {
            Matrix m = Matrix.GetIdentityMatrix();
            m.GetMatrix[0, 1] = x_y;
            m.GetMatrix[0, 2] = x_z;
            m.GetMatrix[1, 0] = y_x;
            m.GetMatrix[1, 2] = y_z;
            m.GetMatrix[2, 0] = z_x;
            m.GetMatrix[2, 1] = z_y;
            return m;
        }
    }
}
