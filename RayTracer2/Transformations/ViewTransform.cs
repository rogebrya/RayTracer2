using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RayTracer2 {
    public class ViewTransform : ITransformation {
        private Tuple from;
        private Tuple to;
        private Tuple up;
        [JsonInclude]
        private string transformType = "View Transform";

        public ViewTransform() {
            from = Tuple.Point(-2, 4, -7);
            to = Tuple.Point(0, 1, 0);
            up = Tuple.Vector(0, 1, 0);
        }

        public ViewTransform(Tuple fromPoint, Tuple toPoint, Tuple upVector) {
            from = fromPoint;
            to = toPoint;
            up = upVector;
        }

        public ViewTransform(double fromX, double fromY, double fromZ, double toX, double toY, double toZ, double upX, double upY, double upZ) {
            from = Tuple.Point(fromX, fromY, fromZ);
            to = Tuple.Point(toX, toY, toZ);
            up = Tuple.Vector(upX, upY, upZ);
        }

        public Tuple From {
            get { return from; }
            set { from = value; }
        }

        public Tuple To {
            get { return to; }
            set { to = value; }
        }

        public Tuple Up {
            get { return up; }
            set { up = value; }
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
