namespace ImageSynthesis.Lights {
    
    abstract class Light {

        public Color Intensity { get; set; }

        public Light(Color intensity) {
            Intensity = intensity;
        }
    }
}
