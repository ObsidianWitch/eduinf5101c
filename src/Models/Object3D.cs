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
        public Color TextureColor(float u, float v) {
            if (Texture != null) {
                return Texture.Color(u,v);
            }
            
            return Color;
        }
        
        abstract public V3 Point(float u, float v);
        abstract public V3 Normal(V3 p);
        abstract public UVRange UVRange();
    }
    
}
