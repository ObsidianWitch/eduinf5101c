using System;
using System.Collections.Generic;
using ImageSynthesis.Lights;

namespace ImageSynthesis.Models {

    class Sphere : Object3D {
        
        public float Radius { get; set; }

        public Sphere(
            V3 center, float radius, Color color, PhongMaterial material
        ) : base(center, color, material)
        {
            Radius = radius;
        }

        override public V3 Point(float u, float v) {
            return new V3(
                Center.X + (Radius * Mathf.Cos(v) * Mathf.Cos(u)),
                Center.Y + (Radius * Mathf.Cos(v) * Mathf.Sin(u)),
                Center.Z + (Radius * Mathf.Sin(v))
            );
        }

        override public V3 Normal(V3 p) {
            V3 n = p - Center;
            n.Normalize();
            
            return n;
        }
    }

}
