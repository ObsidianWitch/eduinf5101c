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
            return System.Drawing.Color.FromArgb(
                R255(), G255(), B255()
            );
        }

        public byte R255() {
            if (R > 1.0) { R = 1.0f; }
            
            return (byte) (R * 255);
        }

        public byte G255() {
            if (G > 1.0) { G = 1.0f; }
            
            return (byte) (G * 255);
        }

        public byte B255() {
            if (B > 1.0) { B = 1.0f; }
            
            return (byte) (B * 255);
        }

        /// Useful for bump mapping
        public float GreyLevel() {
            return (R + G + B) / 3.0f;
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
