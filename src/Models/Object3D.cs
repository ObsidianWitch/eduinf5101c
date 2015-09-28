using System;

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
        abstract public V3 Point(V2 uv);
        abstract public V3 Normal(V3 p);
    }
    
}
