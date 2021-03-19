using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace RayTracer2 {
    public abstract class Pattern {
        private Color a = new Color(0, 0, 0);
        private Color b = new Color(1, 1, 1);
        private Matrix transform = Matrix.GetIdentityMatrix();
        private List<ITransformation> transformList = new List<ITransformation>();
        private string patternType;

        public Color A {
            get { return a; }
            set { a = value; }
        }

        public Color B {
            get { return b; }
            set { b = value; }
        }

        [JsonIgnore]
        public Matrix Transform {
            get { return transform; }
            set { transform = value; }
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<ITransformation> TransformList {
            get { return transformList; }
            set { transformList = value; }
        }

        public string PatternType {
            get { return patternType; }
            set { patternType = value; }
        }

        public abstract Color PatternAt(Tuple point);

        public override string ToString() {
            string str = "";
            str += "Pattern: " + PatternType + Environment.NewLine;
            str += Globals.prepend + "Color 1: " + Environment.NewLine;
            str += Globals.prepend + Globals.prepend + "R: " + A.Red + " G: " + A.Green + " B: " + A.Blue + Environment.NewLine;
            str += Globals.prepend + "Color 2: " + Environment.NewLine;
            str += Globals.prepend + Globals.prepend + "R: " + B.Red + " G: " + B.Green + " B: " + B.Blue + Environment.NewLine;
            str += Globals.prepend + "Transform: " + Environment.NewLine;
            str += Globals.prepend + Globals.prepend + Transform.GetMatrix[0, 0] + Transform.GetMatrix[0, 1] + Transform.GetMatrix[0, 2] + Transform.GetMatrix[0, 3] + Environment.NewLine;
            str += Globals.prepend + Globals.prepend + Transform.GetMatrix[1, 0] + Transform.GetMatrix[1, 1] + Transform.GetMatrix[1, 2] + Transform.GetMatrix[1, 3] + Environment.NewLine;
            str += Globals.prepend + Globals.prepend + Transform.GetMatrix[2, 0] + Transform.GetMatrix[2, 1] + Transform.GetMatrix[2, 2] + Transform.GetMatrix[2, 3] + Environment.NewLine;
            str += Globals.prepend + Globals.prepend + Transform.GetMatrix[3, 0] + Transform.GetMatrix[3, 1] + Transform.GetMatrix[3, 2] + Transform.GetMatrix[3, 3] + Environment.NewLine;
            return str;
        }

        // Probably fixed for groups
        public Color PatternAtShape(Shape s, Tuple worldPoint) {
            Tuple objectPoint = s.WorldToObject(worldPoint);
            Tuple patternPoint = Transform.Inverse() * objectPoint;

            return PatternAt(patternPoint);
        }

        public void InitiateTransformation() {
            if (TransformList.Count > 0) {
                foreach (ITransformation t in TransformList) {
                    Transform = Transform * t.GetTransform();
                }
            }
        }
    }

    public class TestPattern : Pattern {
        public TestPattern() {

        }

        public override Color PatternAt(Tuple point) {
            return new Color(point.X, point.Y, point.Z);
        }
    }
}
