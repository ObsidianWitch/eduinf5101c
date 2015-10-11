using System;
using ImageSynthesis.Renderers;

namespace ImageSynthesis.Models {
    
    abstract class Object3D {
        
        public V3 Center { get; set; }
        public Color Color { get; set; }
        public PhongMaterial Material { get; set; }
        public Texture Texture { get; set; }
        
        public Object3D(
            V3 center, Color color, PhongMaterial material, Texture texture
        ) {
            Center = center;
            Color = color;
            Material = material;
            Texture = texture;
        }
        
        /// Returns the texture color at coordinates (u,v). If no Texture has
        /// been given for this Object3D, return the object's Color property.
        public Color TextureColor(V2 uv) {
            if (Texture != null) {
                return Texture.Color(uv);
            }
            
            return Color;
        }
        
        /// Retrieves uv values in the ranges specific to this 3D object.
        abstract public V2 UV(V2 uv);
        abstract public V2 UV(V3 p);
        abstract public V3 Point(V2 uv);
        abstract public Tuple<V3,V3> DerivativePoint(V2 uv);
        
        /// Returns this object's normal at the current p point. The normal
        /// has not been altered by a bump map if one is present.
        abstract public V3 Normal(V3 p);
        
        /// Returns the altered normal if this object possesses a bump map, or
        /// the unaltered one if it does not.
        public V3 Normal(V3 p, V2 uv) {
            if (Material.BumpMap != null) {
                return AlteredNormal(p, uv);
            }
            
            return Normal(p);
        }
        
        /// Returns this object's normal altered by the Bump Map attached to
        /// this object's material.
        protected V3 AlteredNormal(V3 p, V2 uv) {
            V2 dh = Material.BumpMap.Bump(uv);
            
            Tuple<V3,V3> dP = DerivativePoint(uv);
            V3 dPdu = dP.Item1;
            V3 dPdv = dP.Item2;
            
            V3 n = Normal(p);
            V3 alteredN = n + Material.KBump * (
                (dPdu ^ n * dh.V) +
                (n * dh.U ^ dPdv)
            );
            alteredN.Normalize();
            
            return alteredN;
        }
        
        abstract public bool Intersect(Ray ray, out float distance);
    }
    
}
