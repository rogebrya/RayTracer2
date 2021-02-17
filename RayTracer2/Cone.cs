﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer2 {
    public class Cone : Shape {
        private double minimum = double.MinValue;
        private double maximum = double.MaxValue;
        private bool isClosed = false;

        public Cone() {
            ShapeType = "Cone";
        }

        public double Minimum {
            get { return minimum; }
            set { minimum = value; }
        }

        public double Maximum {
            get { return maximum; }
            set { maximum = value; }
        }

        public bool IsClosed {
            get { return isClosed; }
            set { isClosed = value; }
        }

        public static Cone GlassCone() {
            Cone c = new Cone();
            c.Material.Transparency = 1.0;
            c.Material.RefractiveIndex = 1.5;
            c.Minimum = -1;
            c.Maximum = 0;
            c.IsClosed = true;
            return c;
        }

        public override List<Intersection> LocalIntersect(Ray localRay) {
            List<Intersection> list = new List<Intersection>();
            double a = Math.Pow(localRay.Direction.X, 2) - Math.Pow(localRay.Direction.Y, 2) + Math.Pow(localRay.Direction.Z, 2);
            double b =
                2 * localRay.Origin.X * localRay.Direction.X -
                2 * localRay.Origin.Y * localRay.Direction.Y +
                2 * localRay.Origin.Z * localRay.Direction.Z;
            double c = Math.Pow(localRay.Origin.X, 2) - Math.Pow(localRay.Origin.Y, 2) + Math.Pow(localRay.Origin.Z, 2);

            if (Math.Abs(a) <= EPSILON && Math.Abs(b) <= EPSILON) {
                IntersectCaps(localRay, list);
                return list;
            } else if (Math.Abs(a) <= EPSILON) {
                double t = -c / (2 * b);
                double y = localRay.Origin.Y + t * localRay.Direction.Y;
                if (Minimum < y && y < Maximum) {
                    list.Add(new Intersection(t, this));
                }
                IntersectCaps(localRay, list);
                return list;
            }

            double discriminant = Math.Pow(b, 2) - 4 * a * c;

            if (discriminant < 0) {
                return list;
            }
            double t0 = (-b - Math.Sqrt(discriminant)) / (2 * a);
            double t1 = (-b + Math.Sqrt(discriminant)) / (2 * a);
            if (t0 > t1) {
                double temp = t0;
                t0 = t1;
                t1 = temp;
            }

            double y0 = localRay.Origin.Y + t0 * localRay.Direction.Y;
            if (Minimum < y0 && y0 < Maximum) {
                list.Add(new Intersection(t0, this));
            }
            double y1 = localRay.Origin.Y + t1 * localRay.Direction.Y;
            if (Minimum < y1 && y1 < Maximum) {
                list.Add(new Intersection(t1, this));
            }
            IntersectCaps(localRay, list);
            return list;
        }

        public bool CheckCap(Ray ray, double t, double radius) {
            double x = ray.Origin.X + t * ray.Direction.X;
            double z = ray.Origin.Z + t * ray.Direction.Z;
            return (Math.Pow(x, 2) + Math.Pow(z, 2)) <= radius;
        }

        public void IntersectCaps(Ray ray, List<Intersection> xs) {
            if (!IsClosed || Math.Abs(ray.Direction.Y) <= EPSILON) {
                return;
            }

            double t = (Minimum - ray.Origin.Y) / ray.Direction.Y;
            if (CheckCap(ray, t, Math.Abs(Minimum))) {
                xs.Add(new Intersection(t, this));
            }

            t = (Maximum - ray.Origin.Y) / ray.Direction.Y;
            if (CheckCap(ray, t, Math.Abs(Maximum))) {
                xs.Add(new Intersection(t, this));
            }
        }

        public override Tuple LocalNormalAt(Tuple localPoint, Intersection hit) {
            double dist = Math.Pow(localPoint.X, 2) + Math.Pow(localPoint.Z, 2);
            if (dist < 1 && localPoint.Y >= Maximum - EPSILON) {
                return Tuple.Vector(0, 1, 0);
            } else if (dist < 1 && localPoint.Y <= Minimum + EPSILON) {
                return Tuple.Vector(0, -1, 0);
            } else {
                double y = Math.Sqrt(Math.Pow(localPoint.X, 2) + Math.Pow(localPoint.Z, 2));
                if (localPoint.Y > 0) {
                    y = -y;
                }
                return Tuple.Vector(localPoint.X, y, localPoint.Z);
            }
        }

        // Equality Stuff
        public static bool operator ==(Cone t1, Cone t2) {
            return
                t1.Transform == t2.Transform &&
                t1.Material == t2.Material &&
                EqualityOfDouble(t1.Minimum, t2.Minimum) &&
                EqualityOfDouble(t1.Maximum, t2.Maximum) &&
                t1.IsClosed == t2.IsClosed;
        }

        public static bool operator !=(Cone t1, Cone t2) {
            return
                !(t1.Transform == t2.Transform &&
                t1.Material == t2.Material &&
                EqualityOfDouble(t1.Minimum, t2.Minimum) &&
                EqualityOfDouble(t1.Maximum, t2.Maximum) &&
                t1.IsClosed == t2.IsClosed);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Cone)obj);
        }

        public override int GetHashCode() {
            unchecked {
                return (int)Math.Pow((Transform.Determinant() * 389), Material.Ambient) + (int)Material.Shininess;
            }
        }

        public bool Equals(Cone other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return
                Transform == other.Transform &&
                Material == other.Material &&
                EqualityOfDouble(Minimum, other.Minimum) &&
                EqualityOfDouble(Maximum, other.Maximum) &&
                IsClosed == other.IsClosed;
        }
    }
}