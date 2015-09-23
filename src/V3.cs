namespace ImageSynthesis {

    struct V3 {

        public float X;
        public float Y;
        public float Z;

        public V3(V3 t) {
            X = t.X;
            Y = t.Y;
            Z = t.Z;
        }

        public V3(float x, float y, float z) {
            X = x;
            Y = y;
            Z = z;
        }

        public V3(int x, int y, int z) {
            X = (float) x;
            Y = (float) y;
            Z = (float) z;
        }

        public float Norm1() {
            return Mathf.Sqrt(X * X + Y * Y + Z * Z);
        }

        public float Norm2() {
            return X * X + Y * Y + Z * Z;
        }

        public void Normalize() {
            float n = Norm1();
            
            if (n == 0) { return; }
            
            X /= n;
            Y /= n;
            Z /= n;
        }

        public static V3 operator +(V3 a, V3 b) {
            V3 t;
            t.X = a.X + b.X;
            t.Y = a.Y + b.Y;
            t.Z = a.Z + b.Z;
            return t;
        }

        public static V3 operator -(V3 a, V3 b) {
            V3 t;
            t.X = a.X - b.X;
            t.Y = a.Y - b.Y;
            t.Z = a.Z - b.Z;
            return t;
        }

        public static V3 operator -(V3 a) {
            V3 t;
            t.X = -a.X;
            t.Y = -a.Y;
            t.Z = -a.Z;
            return t;
        }

        /// Cross product
        public static V3 operator ^ (V3 a, V3 b) {
            V3 t;
            t.X = a.Y * b.Z - a.Z * b.Y;
            t.Y = a.Z * b.X - a.X * b.Z;
            t.Z = a.X * b.Y - a.Y * b.X;
            return t;
        }

        /// Dot product
        public static float operator * (V3 a, V3 b) {
            return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
        }

        public static V3 operator *(float a, V3 b) {
            V3 t;
            t.X = b.X * a;
            t.Y = b.Y * a;
            t.Z = b.Z * a;
            return t;
        }

        public static V3 operator *(V3 b, float a) {
            V3 t;
            t.X = b.X * a;
            t.Y = b.Y * a;
            t.Z = b.Z * a;
            return t;
        }

        public static V3 operator /(V3 b, float a) {
            V3 t;
            t.X = b.X / a;
            t.Y = b.Y / a;
            t.Z = b.Z / a;
            return t;
        }
    }
}
