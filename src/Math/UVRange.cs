namespace ImageSynthesis {
    
    class UVRange {
        
        public float UMin { get; private set; }
        public float UMax { get; private set; }
        public float VMin { get; private set; }
        public float VMax { get; private set; }
        
        public UVRange(float uMin, float uMax, float vMin, float vMax) {
            UMin = uMin;
            UMax = uMax;
            VMin = vMin;
            VMax = vMax;
        }
    }
    
}
