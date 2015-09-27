namespace ImageSynthesis {

    class V2 {

        public float U { get; set; }
        public float V { get; set; }
        
        public float X {
            get { return U; }
            set { U = value; }
        }
        public float Y {
            get { return V; }
            set { V = value; }
        }

        public V2(V2 t) {
            U = t.U;
            V = t.V;
        }

        public V2(float u, float v) {
            U = u;
            V = v;
        }

        public V2(int u, int v) {
            U = (float) u;
            V = (float) v;
        }

        public float Norm1() {
            return Mathf.Sqrt(U * U + V * V);
        }

        public float Norm2() {
            return U * U + V * V;
        }

        public void Normalize() {
            float n = Norm1();
            
            if (n == 0) { return; }
            
            U /= n;
            V /= n;
        }

        public static V2 operator +(V2 a, V2 b) {
             return new V2(
                u: a.U + b.U,
                v: a.V + b.V
            );
        }

        public static V2 operator -(V2 a, V2 b) {
            return new V2(
                u: a.U - b.U,
                v: a.V - b.V
            );
        }

        public static V2 operator -(V2 a) {
            return new V2(
                u: -a.U,
                v: -a.V
            );
        }

        /// Dot product
        public static float operator * (V2 a, V2 b) {
            return a.U * b.U + a.V * b.V;
        }

        public static V2 operator *(float a, V2 b) {
            return new V2(
                u: b.U * a,
                v: b.V * a
            );
        }

        public static V2 operator *(V2 b, float a) {
            return a * b;
        }

        public static V2 operator /(V2 b, float a) {
            return new V2(
                u: b.U / a,
                v: b.V / a
            );
        }
    }
}
