namespace ImageSynthesis.Models {
    
    abstract class Object3D {
        
        public V3 Center { get; set; }
        public Color Color { get; set; }
        public PhongMaterial Material { get; set; }
        
        public Object3D(V3 center, Color color, PhongMaterial material) {
            Center = center;
            Color = color;
            Material = material;
        }
        
        abstract public V3 Point(float u, float v);
        abstract public V3 Normal(V3 p);
        abstract public UVRange UVRange();
    }
    
}
