using System;
using System.Collections.Generic;
using ImageSynthesis.Lights;
using ImageSynthesis.Renderers;

namespace ImageSynthesis.Models {

    class Sphere : Object3D {
        
        public float Radius { get; set; }
        
        public Sphere(V3 center, float radius, PhongMaterial material) :
            base(center, material)
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
        
        /// Retrieves uv values in the [0;1] range for a point p on the sphere.
        override public V2 UV(V3 p) {
            V3 d = p - Center;
            
            // cos(u) = (x - c.x) / (r * cos(v))
            // sin(u) = (y - c.y) / (r * cos(v))
            // tan(u) = sin(u) / cos(u)
            // u = atan((y - c.y) / (x - c.x))
            float u = Mathf.Atan2(d.Y, d.X);
            
            // v = asin((z - c.z) / r)
            float v = Mathf.Asin(d.Z / Radius);
            
            // u,v in [0;1]
            u = (u / (2 * Mathf.PI));
            v = 0.5f + (v / Mathf.PI);

            return new V2(u,v);
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
        
        override public bool Intersect(Ray ray, out float distance) {
            V3 centerDirection = ray.Origin - Center;
            
            float a = 1;
            float b = 2 * centerDirection * ray.Direction;
            float c = (centerDirection * centerDirection) - (Radius * Radius);
            
            float delta = (b * b) - 4 * a * c;
            
            distance = float.MaxValue;
            if (delta >= 0) {
                float d1 = (-b + Mathf.Sqrt(delta))/(2 * a);
                float d2 = (-b - Mathf.Sqrt(delta))/(2 * a);
                
                if (d1 < DIST_THRES && d2 < DIST_THRES) {
                    return false;
                }
                else if (d1 < DIST_THRES || d2 < DIST_THRES) {
                    distance = Math.Max(d1,d2);
                }
                else {
                    distance = Math.Min(d1,d2);
                }
                
                return true;
            }
            
            return false;
        }
    }

}
