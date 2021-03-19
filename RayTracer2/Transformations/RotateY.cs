using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RayTracer2 {
    public class RotateY : ITransformation {
        private double theta;
        [JsonInclude]
        private string transformType = "RotateY";

        public RotateY() {
            theta = 0;
        }

        public RotateY(double thetaY) {
            theta = thetaY;
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
            m.GetMatrix[0, 2] = Math.Sin(theta);
            m.GetMatrix[2, 0] = -Math.Sin(theta);
            m.GetMatrix[2, 2] = Math.Cos(theta);
            return m;
        }
    }
}
