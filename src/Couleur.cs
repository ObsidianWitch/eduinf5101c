using System.Drawing;

namespace ImageSynthesis {

    public struct Couleur {

        /// R,G,B in [0,1]
        public float R, G, B;

        public Couleur(float R, float G, float B) {
            this.R = R;
            this.G = G;
            this.B = B;
        }

        public Couleur(Couleur c) {
            this.R = c.R;
            this.G = c.G;
            this.B = c.B;
        }

        public void From255(byte r255, byte g255, byte b255) {
            R = (float) (r255 / 255.0);
            G = (float) (g255 / 255.0);
            B = (float) (b255 / 255.0);
        }

        public Color To255() {
            check();
            
            byte r255 = (byte) (R * 255);
            byte g255 = (byte) (G * 255);
            byte b255 = (byte) (B * 255);
            
            return Color.FromArgb(r255, g255, b255);
        }

        /// Useful for bump mapping
        public float GreyLevel() {
            return (R + G + B) / 3.0f;
        }

        private void check() {
            if (R > 1.0) R = 1.0f;
            if (G > 1.0) G = 1.0f;
            if (B > 1.0) B = 1.0f;
        }

        // Operators

        public static Couleur operator +(Couleur a, Couleur b) {
            return new Couleur(a.R + b.R, a.G + b.G, a.B + b.B);
        }

        public static Couleur operator -(Couleur a, Couleur b) {
            return new Couleur(a.R - b.R, a.G - b.G, a.B - b.B);
        }

        public static Couleur operator -(Couleur a) {
            return new Couleur(-a.R, -a.G, -a.B);
        }

        public static Couleur operator *(Couleur a, Couleur b) {
            return new Couleur(a.R * b.R, a.G * b.G, a.B * b.B);
        }

        public static Couleur operator *(float a, Couleur b) {
            return new Couleur(a * b.R, a * b.G, a * b.B);
        }

        public static Couleur operator *(Couleur b, float a) {
            return new Couleur(a * b.R, a * b.G, a * b.B);
        }

        public static Couleur operator /(Couleur b, float a) {
            return new Couleur(b.R / a, b.G / a, b.B / a);
        }
    }
}
