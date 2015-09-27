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

        override public UVRange UVRange() {
            return new UVRange(
                uMin: 0.0f,          uMax: 2 * Mathf.PI,
                vMin: -Mathf.PI / 2, vMax: Mathf.PI / 2
            );
        }
    }

}
