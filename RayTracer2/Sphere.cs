using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer2 {
    public class Sphere : Shape {
        public Sphere() {
            ShapeType = "Sphere";
        }
        
        public override string ToString() {
            string str = "";
            str += "Shape: " + ShapeType + Environment.NewLine;
            str += "\tTransform: " + Environment.NewLine;
            str += "\t\t" + Transform.GetMatrix[0, 0] + Transform.GetMatrix[0, 1] + Transform.GetMatrix[0, 2] + Transform.GetMatrix[0, 3] + Environment.NewLine;
            str += "\t\t" + Transform.GetMatrix[1, 0] + Transform.GetMatrix[1, 1] + Transform.GetMatrix[1, 2] + Transform.GetMatrix[1, 3] + Environment.NewLine;
            str += "\t\t" + Transform.GetMatrix[2, 0] + Transform.GetMatrix[2, 1] + Transform.GetMatrix[2, 2] + Transform.GetMatrix[2, 3] + Environment.NewLine;
            str += "\t\t" + Transform.GetMatrix[3, 0] + Transform.GetMatrix[3, 1] + Transform.GetMatrix[3, 2] + Transform.GetMatrix[3, 3] + Environment.NewLine;
            str += "\tMaterial: " + Environment.NewLine;
            str += "\t\tColor: " + Environment.NewLine;
            str += "\t\t\tR: " + Material.Color.Red + " G: " + Material.Color.Green + " B: " + Material.Color.Blue + Environment.NewLine;
            str += "\t\tAmbient: " + Material.Ambient + Environment.NewLine;
            str += "\t\tDiffuse: " + Material.Diffuse + Environment.NewLine;
            str += "\t\tSpecular: " + Material.Specular + Environment.NewLine;
            str += "\t\tShininess: " + Material.Shininess + Environment.NewLine;
            if (Material.Pattern == null) {
                str += "\t\tPattern: None" + Environment.NewLine;
            } else {
                str += "\t\tPattern: " + Material.Pattern.PatternType + Environment.NewLine;
                str += "\t\t\tColor 1: " + Environment.NewLine;
                str += "\t\t\t\tR: " + Material.Pattern.A.Red + " G: " + Material.Pattern.A.Green + " B: " + Material.Pattern.A.Blue + Environment.NewLine;
                str += "\t\t\tColor 2: " + Environment.NewLine;
                str += "\t\t\t\tR: " + Material.Pattern.B.Red + " G: " + Material.Pattern.B.Green + " B: " + Material.Pattern.B.Blue + Environment.NewLine;
                str += "\t\t\tTransform (Pattern): " + Environment.NewLine;
                str += "\t\t\t\t" + Material.Pattern.Transform.GetMatrix[0, 0] + Material.Pattern.Transform.GetMatrix[0, 1] + Material.Pattern.Transform.GetMatrix[0, 2] + Material.Pattern.Transform.GetMatrix[0, 3] + Environment.NewLine;
                str += "\t\t\t\t" + Material.Pattern.Transform.GetMatrix[1, 0] + Material.Pattern.Transform.GetMatrix[1, 1] + Material.Pattern.Transform.GetMatrix[1, 2] + Material.Pattern.Transform.GetMatrix[1, 3] + Environment.NewLine;
                str += "\t\t\t\t" + Material.Pattern.Transform.GetMatrix[2, 0] + Material.Pattern.Transform.GetMatrix[2, 1] + Material.Pattern.Transform.GetMatrix[2, 2] + Material.Pattern.Transform.GetMatrix[2, 3] + Environment.NewLine;
                str += "\t\t\t\t" + Material.Pattern.Transform.GetMatrix[3, 0] + Material.Pattern.Transform.GetMatrix[3, 1] + Material.Pattern.Transform.GetMatrix[3, 2] + Material.Pattern.Transform.GetMatrix[3, 3] + Environment.NewLine;
            }
            str += "\t\tReflectivity: " + Material.Reflectivity + Environment.NewLine;
            str += "\t\tTransparency: " + Material.Transparency + Environment.NewLine;
            str += "\t\tRefractive Index: " + Material.RefractiveIndex + Environment.NewLine;
            str += Environment.NewLine;
            return str;
        }
        
        public static Sphere GlassSphere() {
            Sphere s = new Sphere();
            s.Material.Transparency = 1.0;
            s.Material.RefractiveIndex = 1.5;
            return s;
        }

        public override List<Intersection> LocalIntersect(Ray localRay) {
            List<Intersection> list = new List<Intersection>();
            Tuple sphereToRay = localRay.Origin - Tuple.Point(0, 0, 0);

            double a = Tuple.Dot(localRay.Direction, localRay.Direction);
            double b = 2 * Tuple.Dot(localRay.Direction, sphereToRay);
            double c = Tuple.Dot(sphereToRay, sphereToRay) - 1;

            double discriminant = Math.Pow(b, 2) - 4 * a * c;

            if (discriminant >= 0) {
                list.Add(new Intersection((-b - Math.Sqrt(discriminant)) / (2 * a), this));
                list.Add(new Intersection((-b + Math.Sqrt(discriminant)) / (2 * a), this));
            }
            return list;
        }

        public override Tuple LocalNormalAt(Tuple localPoint, Intersection hit) {
            return localPoint - Tuple.Point(0, 0, 0);
        }

        // Equality Stuff
        public static bool operator ==(Sphere t1, Sphere t2) {
            return t1.Transform == t2.Transform && t1.Material == t2.Material;
        }

        public static bool operator !=(Sphere t1, Sphere t2) {
            return !(t1.Transform == t2.Transform && t1.Material == t2.Material);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Sphere)obj);
        }

        public override int GetHashCode() {
            unchecked {
                return (int)Math.Pow((Transform.Determinant() * 397), Material.Ambient) + (int)Material.Shininess;
            }
        }

        public bool Equals(Sphere other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Transform == other.Transform && Material == other.Material;
        }
    }
}
