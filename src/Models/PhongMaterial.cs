namespace ImageSynthesis.Models {

    /// This class holds properties caracterizing a material using the Phong
    /// illumination model. The parameters for the 3 components of the
    /// illumination model are defined.
    class PhongMaterial {
        
        /// Material bump map
        public Texture BumpMap { get; private set; }
        
        /// Ambient reflection constant
        public float KAmbient { get; private set; }
        
        /// Diffuse reflection constant
        public float KDiffuse { get; private set; }
        
        /// Specular reflection constant
        public float KSpecular { get; private set; }
        
        /// Specifies how small the highlights are, the shinier the material,
        /// the smaller the highlight.
        public int Shininess { get; private set; }

        public PhongMaterial(
            float kA, float kD, float kS, int shininess, Texture bumpMap = null
        ) {
            KAmbient = kA;
            KDiffuse = kD;
            KSpecular = kS;
            Shininess = shininess;
            BumpMap = bumpMap;
        }
    }
    
}
