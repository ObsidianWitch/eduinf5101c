using System;

namespace ImageSynthesis {

    class Mathf {

        public static float PI = (float) Math.PI;
        
        public static float MinMax(float min, float v, float max) {
            if (v < min) { return min; }
            if (v > max) { return max; }
            return v;
        }

        public static float Cos(float theta) {
            return (float) Math.Cos(theta);
        }

        public static float Sin(float theta) {
            return (float) Math.Sin(theta);
        }

        public static float Acos(float v) {
            return (float) Math.Acos(v);
        }

        public static float Asin(float v) {
            return (float) Math.Asin(v);
        }
        
        public static float Atan(float v) {
            return (float) Math.Atan(v);
        }

        public static float Atan2(float y, float x) {
            return (float) Math.Atan2(y,x);
        }

        public static float Sqrt(float v) {
            return (float) Math.Sqrt(v);
        }

        public static float Pow(float v1, float v2) {
            return (float) Math.Pow(v1, v2);
        }

        public static float RandNP(float v) {
            Random r = new Random();
            
            return ((float) r.NextDouble() - 0.5f) * 2 * v;
        }

        public static float RandP(float v)  {
            Random r = new Random();
            
            return (float) r.NextDouble() * v;
        }

    }
}
