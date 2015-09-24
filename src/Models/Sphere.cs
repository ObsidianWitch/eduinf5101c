namespace ImageSynthesis.Models {

    class Sphere {

        private V3 Center { get; set; }
        private float Radius { get; set; }
        private Color Color { get; set; }

        public Sphere(V3 center, float radius, Color color) {
            Center = center;
            Radius = radius;
            Color  = color;
        }

        public void Draw() {
            for (float u = 0 ; u < 2 * Mathf.PI ; u += 0.01f) {
                for (float v = -Mathf.PI / 2 ; v < Mathf.PI / 2 ; v += 0.01f) {
                    V3 p = Point(u, v);
                    BitmapCanvas.DrawPixel((int) p.X, (int) p.Z, p.Y, Color);
                }
            }
        }

        public V3 Point(float u, float v) {
            return new V3(
                Center.X + (Radius * Mathf.Cos(v) * Mathf.Cos(u)),
                Center.Y + (Radius * Mathf.Cos(v) * Mathf.Sin(u)),
                Center.Z + (Radius * Mathf.Sin(v))
            );
        }

    }

}
