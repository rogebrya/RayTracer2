using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace RayTracer2 {
    public class Material {
        private Color color = new Color(1, 1, 1);
        private double ambient = 0.1;
        private double diffuse = 0.9;
        private double specular = 0.9;
        private double shininess = 200.0;
        private Pattern pattern;
        private double reflectivity = 0.0;
        private double transparency = 0.0;
        private double refractiveIndex = 1.0;

        public Material() {

        }

        public Material(Color color, double ambient, double diffuse, double specular, double shininess) {
            this.color = color;
            this.ambient = ambient;
            this.diffuse = diffuse;
            this.specular = specular;
            this.shininess = shininess;
        }

        public Color Color {
            get { return color; }
            set { color = value; }
        }

        public double Ambient {
            get { return ambient; }
            set { ambient = value; }
        }

        public double Diffuse {
            get { return diffuse; }
            set { diffuse = value; }
        }

        public double Specular {
            get { return specular; }
            set { specular = value; }
        }

        public double Shininess {
            get { return shininess; }
            set { shininess = value; }
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Pattern Pattern {
            get { return pattern; }
            set { pattern = value; }
        }

        public double Reflectivity {
            get { return reflectivity; }
            set { reflectivity = value; }
        }

        public double Transparency {
            get { return transparency; }
            set { transparency = value; }
        }

        public double RefractiveIndex {
            get { return refractiveIndex; }
            set { refractiveIndex = value; }
        }

        public Color Lighting(Shape shape, Light light, Tuple point, Tuple eyev, Tuple normalv, bool isShadow) {
            Color c = Color;
            if (Pattern is not null) {
                c = Pattern.PatternAtShape(shape, point);
            }
            Color effectiveColor = c * light.Intensity;
            Tuple lightv = Tuple.Normalize(light.Position - point);
            Color ambientPart = effectiveColor * Ambient;
            if (isShadow) {
                return ambientPart;
            } else {
                Color diffusePart;
                Color specularPart;
                double lightDotNormal = Tuple.Dot(lightv, normalv);
                if (lightDotNormal < 0) {
                    diffusePart = new Color(0, 0, 0);
                    specularPart = new Color(0, 0, 0);
                } else {
                    diffusePart = effectiveColor * Diffuse * lightDotNormal;
                    Tuple reflectv = Tuple.Reflect(-lightv, normalv);
                    double reflectDotEye = Tuple.Dot(reflectv, eyev);
                    if (reflectDotEye <= 0) {
                        specularPart = new Color(0, 0, 0);
                    } else {
                        double factor = Math.Pow(reflectDotEye, Shininess);
                        specularPart = light.Intensity * Specular * factor;
                    }
                }
                return ambientPart + diffusePart + specularPart;
            }
        }

        public override string ToString() {
            string str = "";
            str += "Material: " + Environment.NewLine;
            str += Globals.prepend + "Color: " + Environment.NewLine;
            str += Globals.prepend + Globals.prepend + "R: " + Color.Red + " G: " + Color.Green + " B: " + Color.Blue + Environment.NewLine;
            str += Globals.prepend + "Ambient: " + Ambient + Environment.NewLine;
            str += Globals.prepend + "Diffuse: " + Diffuse + Environment.NewLine;
            str += Globals.prepend + "Specular: " + Specular + Environment.NewLine;
            str += Globals.prepend + "Shininess: " + Shininess + Environment.NewLine;
            if (Pattern is null) {
                str += Globals.prepend + Globals.prepend + "Pattern: None" + Environment.NewLine;
            } else {
                str += Globals.IndentString(Pattern.ToString());
            }
            str += Globals.prepend + "Reflectivity: " + Reflectivity + Environment.NewLine;
            str += Globals.prepend + "Transparency: " + Transparency + Environment.NewLine;
            str += Globals.prepend + "Refractive Index: " + RefractiveIndex + Environment.NewLine;
            return str;
        }

        // Equality Stuff
        public static bool operator ==(Material m1, Material m2) {
            return ((m1.Color == m2.Color) && Globals.EqualityOfDouble(m1.Ambient, m2.Ambient) &&
                Globals.EqualityOfDouble(m1.Diffuse, m2.Diffuse) && Globals.EqualityOfDouble(m1.Specular, m2.Specular)
                && Globals.EqualityOfDouble(m1.Shininess, m2.Shininess));
        }

        public static bool operator !=(Material m1, Material m2) {
            return !((m1.Color == m2.Color) && Globals.EqualityOfDouble(m1.Ambient, m2.Ambient) &&
                Globals.EqualityOfDouble(m1.Diffuse, m2.Diffuse) && Globals.EqualityOfDouble(m1.Specular, m2.Specular)
                && Globals.EqualityOfDouble(m1.Shininess, m2.Shininess));
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Material)obj);
        }

        public override int GetHashCode() {
            unchecked {
                return (int)Math.Pow(((Color.Red * Color.Green * Color.Blue * Diffuse * Specular * Shininess) * 397), 10 * Ambient);
            }
        }

        public bool Equals(Material other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return ((Color == other.Color) && Globals.EqualityOfDouble(Ambient, other.Ambient) &&
                Globals.EqualityOfDouble(Diffuse, other.Diffuse) && Globals.EqualityOfDouble(Specular, other.Specular)
                && Globals.EqualityOfDouble(Shininess, other.Shininess));
        }
    }
}
