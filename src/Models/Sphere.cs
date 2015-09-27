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

        override public V3 Point(float u, float v) {
            float tmpU = u * 2 * Mathf.PI; // [0;1] -> [0;2PI]
            float tmpV = (v - 0.5f) * Mathf.PI; // [0;1] -> [-PI/2;PI/2]
            
            return new V3(
                Center.X + (Radius * Mathf.Cos(tmpV) * Mathf.Cos(tmpU)),
                Center.Y + (Radius * Mathf.Cos(tmpV) * Mathf.Sin(tmpU)),
                Center.Z + (Radius * Mathf.Sin(tmpV))
            );
        }

        override public V3 Normal(V3 p) {
            V3 n = p - Center;
            n.Normalize();
            
            return n;
        }
    }

}
