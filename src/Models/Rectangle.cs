using System;
using System.Collections.Generic;
using ImageSynthesis.Lights;
using ImageSynthesis.Renderers;

namespace ImageSynthesis.Models {

    class Rectangle : Object3D {
        
        public V3 VA { get; set; }
        public V3 VB { get; set; }
        
        public Rectangle(
            V3 center, V3 va, V3 vb, Color color,
            PhongMaterial material, Texture texture = null
        ) : base(center, color, material, texture)
        {
            if (va * vb != 0) {
                throw new Exception(
                    "Not a rectangle (VA: " + va + ", VB: " + vb + ")."
                );
            }
            
            VA = va;
            VB = vb;
        }
        
        /// Retrieves uv values in the ranges specific to a rectangle.
        /// u: [0;1] -> [-0.5 ; 0.5]
        /// v: [0;1] -> [-0.5 ; 0.5]
        override public V2 UV(V2 uv) {
            return new V2 (
                u: uv.U - 0.5f,
                v: uv.V - 0.5f
            );
        }
        
        override public V2 UV(V3 p) {
            V3 d = p - Center;
            
            V3 van = new V3(VA);
            van.Normalize();
            
            V3 vbn = new V3(VB);
            vbn.Normalize();
            
            // u = ||d -> VA|| / ||VA||
            float u = ((d * van) / VA.Norm1());
            
            // v = ||d -> VB|| / ||VB||
            float v = ((d * vbn) / VB.Norm1());
            
            // u & v: [-0.5 ; 0.5] -> [0;1]
            u += 0.5f;
            v += 0.5f;
            
            return new V2(u,v);
        }
        
        override public V3 Point(V2 uv) {
            V2 uvR = UV(uv); // uv rectangle
            
            return Center + (uvR.U * VA) + (uvR.V * VB);
        }
        
        override public Tuple<V3,V3> DerivativePoint(V2 uv) {
            V3 dPdu = new V3(VA);
            dPdu.Normalize();
            
            V3 dPdv = new V3(VB);
            dPdv.Normalize();
            
            return new Tuple<V3,V3>(dPdu, dPdv);
        }
        
        public V3 Normal() {
            V3 normal = VA ^ VB;
            normal.Normalize();
            return normal;
        }
        
        override public V3 Normal(V3 p) {
            return Normal();
        }
        
        override public bool Intersect(Ray ray, out float distance) {
            V3 centerDirection = Center - ray.Origin;
            
            V3 n = Normal();
            float t = (centerDirection * n) / (ray.Direction * n);
            
            if (t > DIST_THRES) {
                distance = t;
                
                V3 collisionPoint = ray.Origin + (ray.Direction * distance);
                V3 cornerPoint = Center - VB/2 - VA/2;
                
                V3 v = collisionPoint - cornerPoint;
                
                bool inRectangle = (VA * v >= 0) &&
                                   (VA * v <= VA.Norm2()) &&
                                   (VB * v >= 0) &&
                                   (VB * v <= VB.Norm2());
                
                return inRectangle;
            }
            
            distance = float.MaxValue;
            return false;
        }
    }

}
