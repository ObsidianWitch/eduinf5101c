using System;
using System.Collections.Generic;
using ImageSynthesis.Lights;

namespace ImageSynthesis.Models {

    class Sphere : Object3D {
        
        public float Radius { get; set; }

        public Sphere(
            V3 center, float radius, Color color, PhongMaterial material,
            Texture texture = null
        ) : base(center, color, material, texture)
        {
            Radius = radius;
        }

        override public V3 Point(V2 uv) {
            float u = uv.U * 2 * Mathf.PI; // [0;1] -> [0;2PI]
            float v = (uv.V - 0.5f) * Mathf.PI; // [0;1] -> [-PI/2;PI/2]
            
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
