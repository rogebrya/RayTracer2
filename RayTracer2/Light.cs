using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer2 {
    public class Light {
        private Tuple position;
        private Color intensity;
        private Tuple target;
        private double fieldOfView;
        private string lightType;

        public Light() {

        }

        public Light(Tuple position, Color intensity, string lightType) {
            this.position = position;
            this.intensity = intensity;
            this.lightType = lightType;
        }

        public Light(Tuple position, Color intensity, Tuple target, double fieldOfView, string lightType) {
            this.position = position;
            this.intensity = intensity;
            this.target = target;
            this.fieldOfView = fieldOfView;
            this.lightType = lightType;
        }

        public Tuple Position {
            get { return position; }
            set { position = value; }
        }

        public Color Intensity {
            get { return intensity; }
            set { intensity = value; }
        }

        public Tuple Target {
            get { return target; }
            set { target = value; }
        }

        public double FieldOfView {
            get { return fieldOfView; }
            set { fieldOfView = value; }
        }

        public string LightType {
            get { return lightType; }
            set { lightType = value; }
        }

        public static Light PointLight(Tuple position, Color intensity) {
            return new Light(position, intensity, "Point Light");
        }

        public static Light SpotLight(Tuple position, Color intensity, Tuple target, double fieldOfView) {
            return new Light(position, intensity, target, DegreesToRadians(fieldOfView), "Spotlight");
        }

        private static double DegreesToRadians(double degrees) {
            return Math.Round(degrees * Math.PI / 180);
        }

        public string ToString() {
            string str = "";
            str += "Light: " + LightType + Environment.NewLine;
            str += "  Position: " + Environment.NewLine;
            str += "    X: " + position.X + " Y: " + position.Y + " Z: " + position.Z + Environment.NewLine;
            str += "  Intensity: " + Environment.NewLine;
            str += "    R: " + intensity.Red + " G: " + intensity.Green + " B: " + intensity.Blue + Environment.NewLine;
            if (lightType == "Spotlight") {
                str += "  Target: " + Environment.NewLine;
                str += "    X: " + target.X + " Y: " + target.Y + " Z: " + target.Z + Environment.NewLine;
                str += "  Field of View: " + fieldOfView + Environment.NewLine;
            }
            str += Environment.NewLine;
            return str;
        }

        // Equality Stuff
        public static bool operator ==(Light t1, Light t2) {
            return t1.Position == t2.Position && t1.Intensity == t2.Intensity;
        }

        public static bool operator !=(Light t1, Light t2) {
            return !(t1.Position == t2.Position && t1.Intensity == t2.Intensity);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Light)obj);
        }

        public override int GetHashCode() {
            unchecked {
                return (int)Math.Pow(((position.X * position.Z) * 397), position.Y) + (int)intensity.Red;
            }
        }

        public bool Equals(Light other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Position == other.Position && Intensity == other.Intensity;
        }
    }
}
