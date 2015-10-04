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
                x: Center.X + (Radius * Mathf.Cos(uvS.V) * Mathf.Cos(uvS.U)),
                y: Center.Y + (Radius * Mathf.Cos(uvS.V) * Mathf.Sin(uvS.U)),
                z: Center.Z + (Radius * Mathf.Sin(uvS.V))
            );
        }
        
        override public Tuple<V3,V3> DerivativePoint(V2 uv) {
            V2 uvS = UV(uv); // uv sphere
            
            V3 dPdu = new V3(
                x: -Radius * Mathf.Cos(uvS.V) * Mathf.Sin(uvS.U),
                y: Radius * Mathf.Cos(uvS.V) * Mathf.Cos(uvS.U),
                z: 0
            );
            dPdu.Normalize();
            
            V3 dPdv = new V3(
                x: -Radius * Mathf.Sin(uvS.V) * Mathf.Cos(uvS.U),
                y: -Radius * Mathf.Sin(uvS.V) * Mathf.Sin(uvS.U),
                z: Radius * Mathf.Cos(uvS.V)
            );
            dPdv.Normalize();
            
            return new Tuple<V3,V3>(dPdu, dPdv);
        }
        
        override public V3 Normal(V3 p) {
            V3 n = p - Center;
            n.Normalize();
            
            return n;
        }
    }

}
