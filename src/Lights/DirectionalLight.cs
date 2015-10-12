namespace ImageSynthesis.Lights {
    
    class DirectionalLight : Light {

        public V3 Direction { get; set; }

        public DirectionalLight(Color intensity, V3 direction) :
            base(intensity)
        {
            Direction = direction;
            Direction.Normalize();
        }
    }
}
