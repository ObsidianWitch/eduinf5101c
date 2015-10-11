using ImageSynthesis;

namespace ImageSynthesis.Renderers {
    
    class Ray {
        
        public V3 Origin { get; private set; }
        public V3 Direction { get; private set; }
        
        public Ray(V3 origin, V3 direction) {
            Origin = origin;
            
            Direction = direction;
            Direction.Normalize();
        }
        
    }
}
