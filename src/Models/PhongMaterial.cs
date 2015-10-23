namespace ImageSynthesis.Models {
    
    /// This class holds properties caracterizing a material using the Phong
    /// illumination model. The parameters for the 3 components of the
    /// illumination model are defined.
    class PhongMaterial {
        
        /// Material color (unused if material texture defined)
        public Color Color { get; set; }
        
        /// Material texture
        public Texture Texture { get; set; }
        
        /// Material bump map
        public Texture BumpMap { get; private set; }
        
        /// Bump map coefficient
        public float KBump { get; private set; }
        
        /// Ambient reflection constant
        public float KAmbient { get; private set; }
        
        /// Diffuse reflection constant
        public float KDiffuse { get; private set; }
        
        /// Specular reflection constant
        public float KSpecular { get; private set; }
        
        /// Specifies how small the highlights are, the shinier the material,
        /// the smaller the highlight.
        public int Shininess { get; private set; }
        
        public float Reflection { get; private set; }
        public bool IsReflective() { return Reflection > 0; }
        
        public float Transparency { get; private set; }
        public bool IsTransparent() { return Transparency > 0; }
        public float RefractiveIndex { get; private set; }
        
        public PhongMaterial(
            Color color, float kA, float kD, float kS, int shininess,
            float reflection, float transparency, float refractiveIndex,
            Texture texture = null, Texture bumpMap = null, float kBump = 1.0f
        ) {
            Color = color;
            Texture = texture;
            KAmbient = kA;
            KDiffuse = kD;
            KSpecular = kS;
            Shininess = shininess;
            Reflection = reflection;
            Transparency = transparency;
            RefractiveIndex = refractiveIndex;
            BumpMap = bumpMap;
            KBump = kBump;
        }
    }
    
}
