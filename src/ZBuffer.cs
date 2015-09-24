namespace ImageSynthesis {

    class ZBuffer {

        private float[,] Data;
        private int Width, Height;

        public ZBuffer(int width, int height) {
            Width = width;
            Height = height;
            
            Data = new float[Width,Height];
            clear();
        }

        public float Get(int x, int y) {
            return Data[x,y];
        }

        /// Sets the value 'z' at position (x,y) of the z-buffer.
        /// If the new value is farther from the viewer than the previous one,
        /// then do not set the new value.
        /// Returns whether the value was set or not.
        public bool Set(int x, int y, float z) {
            if (z < Data[x,y]) {
                Data[x,y] = z;
                return true;
            }
            else {
                return false;
            }
        }

        public void clear() {
            for (int x = 0 ; x < Width ; x++) {
                for (int y = 0 ; y < Height ; y++) {
                    Data[x,y] = float.PositiveInfinity;
                }
            }
        }
    }
}
