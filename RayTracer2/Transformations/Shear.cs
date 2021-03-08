using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer2 {
    public class Shear : ITransformation {
        private double x_y;
        private double x_z;
        private double y_x;
        private double y_z;
        private double z_x;
        private double z_y;
        private string transformType = "Shear";

        public Shear(double shearXY, double shearXZ, double shearYX, double shearYZ, double shearZX, double shearZY) {
            x_y = shearXY;
            x_z = shearXZ;
            y_x = shearYX;
            y_z = shearYZ;
            z_x = shearZX;
            z_y = shearZY;
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
