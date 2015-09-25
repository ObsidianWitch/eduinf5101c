namespace ImageSynthesis.Lights {
    
    class PointLight : Light {

        public V3 Position { get; set; }

        public PointLight(Color intensity, V3 position) :
            base(intensity)
        {
            Position = position;
        }

        /// Gets the normalized direction vector from the given point p toward
        /// the current light source.
        public V3 Direction(V3 p) {
            V3 dir = Position - p;
            dir.Normalize();
            return dir;
        }
    }
}
