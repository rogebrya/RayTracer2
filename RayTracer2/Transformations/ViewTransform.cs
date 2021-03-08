using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracer2 {
    public class ViewTransform : ITransformation {
        private Tuple from;
        private Tuple to;
        private Tuple up;
        private string transformType = "View Transform";

        public ViewTransform(Tuple fromVector, Tuple toVector, Tuple upVector) {
            from = fromVector;
            to = toVector;
            up = upVector;
        }

        public override string ToString() {
            return transformType + " From: " + from.X + " " + from.Y + " " + from.Z + " To: " + to.X + " " + to.Y + " " + to.Z + " Up: " + up.X + " " + up.Y + " " + up.Z + Environment.NewLine; ;
        }

        public Matrix GetTransform() {
            Tuple forward = Tuple.Normalize(to - from);
            Tuple upn = Tuple.Normalize(up);
            Tuple left = Tuple.Cross(forward, upn);
            Tuple trueUp = Tuple.Cross(left, forward);

            Matrix orientation = new Matrix(
                left.X, left.Y, left.Z, 0,
                trueUp.X, trueUp.Y, trueUp.Z, 0,
                -forward.X, -forward.Y, -forward.Z, 0,
                0, 0, 0, 1);

            Translate translate = new Translate(-from.X, -from.Y, -from.Z);
            return orientation * translate.GetTransform();
        }
    }
}
