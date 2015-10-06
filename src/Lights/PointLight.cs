namespace ImageSynthesis.Lights {
    
    class PointLight : Light {

        public V3 Position { get; set; }

        public PointLight(Color intensity, V3 position) :
            base(intensity)
        {
            Position = position;
        }
    }
}
