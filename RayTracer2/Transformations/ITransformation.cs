using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer2 {
    public interface ITransformation {
        public Matrix GetTransform();
    }
}
