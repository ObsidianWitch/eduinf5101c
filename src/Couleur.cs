using System.Drawing;

namespace ImageSynthesis {

    public struct Couleur {

        /// R,G,B in [0,1]
        public float R, G, B;

        public void From255(byte RR, byte GG, byte BB) {
            R = (float)(RR / 255.0);
            G = (float)(GG / 255.0);
            B = (float)(BB / 255.0);
        }

        static public  void Transpose(ref Couleur cc, System.Drawing.Color c) {
            cc.R = (float) (c.R / 255.0);
            cc.G = (float) (c.G / 255.0);
            cc.B = (float) (c.B / 255.0);
        }

        public void check() {
            if (R > 1.0) R = 1.0f;
            if (G > 1.0) G = 1.0f;
            if (B > 1.0) B = 1.0f;
        }

        public void To255(out byte RR, out byte GG, out byte BB) {
            RR = (byte)(R * 255);
            GG = (byte)(G * 255);
            BB = (byte)(B * 255);
        }

        public Color Convertion() {
            check();
            byte RR, GG, BB;
            To255(out RR, out GG, out BB);
            return Color.FromArgb(RR, GG, BB);
        }

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

        /// Useful for bump mapping
        public float GreyLevel() {
            return (R + B + G) / 3.0f;
        }

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
