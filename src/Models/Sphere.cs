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
        
        /// Retrieves uv values in the ranges specific to a sphere.
        /// u: [0;1] -> [0;2PI]
        /// v: [0;1] -> [-PI/2;PI/2]
        override public V2 UV(V2 uv) {
            return new V2 (
                u: uv.U * 2 * Mathf.PI,
                v: (uv.V - 0.5f) * Mathf.PI
            );
        }
        
        override public V3 Point(V2 uv) {
            V2 uvS = UV(uv); // uv sphere
            
            return new V3(
                Center.X + (Radius * Mathf.Cos(uvS.V) * Mathf.Cos(uvS.U)),
                Center.Y + (Radius * Mathf.Cos(uvS.V) * Mathf.Sin(uvS.U)),
                Center.Z + (Radius * Mathf.Sin(uvS.V))
            );
        }
        
        override public V3 Normal(V3 p) {
            V3 n = p - Center;
            n.Normalize();
            
            return n;
        }
    }

}
