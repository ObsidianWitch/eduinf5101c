using System;

namespace ImageSynthesis {

    class Mathf {

        static public float PI = (float) Math.PI;

        static public float Cos(float theta) {
            return (float) Math.Cos(theta);
        }

        static public float Sin(float theta) {
            return (float) Math.Sin(theta);
        }

        static public float Acos(float v) {
            return (float) Math.Acos(v);
        }

        static public float Asin(float v) {
            return (float) Math.Asin(v);
        }

        static public float Sqrt(float v) {
            return (float) Math.Sqrt(v);
        }

        static public float Pow(float v1, float v2) {
            return (float) Math.Pow(v1, v2);
        }

        static public float RandNP(float v) {
            Random r = new Random();
            
            return ((float) r.NextDouble() - 0.5f) * 2 * v;
        }

        static public float RandP(float v)  {
            Random r = new Random();
            
            return (float) r.NextDouble() * v;
        }

    }
}
