using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RayTracer2 {
    public class RotateZ : ITransformation {
        private double theta;
        [JsonInclude]
        private string transformType = "RotateZ";

        public RotateZ() {
            theta = 0;
        }

        public RotateZ(double thetaZ) {
            theta = thetaZ;
        }

        public double Theta {
            get { return theta; }
            set { theta = value; }
        }

        public override string ToString() {
            return transformType + " Theta: " + theta + Environment.NewLine;
        }

        public Matrix GetTransform() {
            Matrix m = Matrix.GetIdentityMatrix();
            m.GetMatrix[0, 0] = Math.Cos(theta);
            m.GetMatrix[0, 1] = -Math.Sin(theta);
            m.GetMatrix[1, 0] = Math.Sin(theta);
            m.GetMatrix[1, 1] = Math.Cos(theta);
            return m;
        }
    }
}
