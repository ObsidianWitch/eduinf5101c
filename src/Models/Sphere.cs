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
                    V3 p = Point(u,v);
                    
                    // TODO refacto Illu
                    
                    // Ambiant reflection
                    Color ia = new Color(0.2f, 0.2f, 0.2f);
                    float ka = 1.0f;
                    Color Ia = Color * ia * ka;
                    
                    // Diffuse reflection
                    V3 lightSrc = new V3(200, -200, 200);
                    
                    V3 n = Normal(p);
                    
                    V3 l = lightSrc - p;
                    l.Normalize();
                    
                    Color id = new Color(1.0f, 1.0f, 1.0f);
                    float kd = 1.0f;
                    
                    Color Id = Color * id * kd * (l * n);
                    
                    // FIXME avoid having negative Id cancelling other components
                    
                    if (Id.R < 0.0) { Id.R = 0.0f; }
                    if (Id.G < 0.0) { Id.G = 0.0f; }
                    if (Id.B < 0.0) { Id.B = 0.0f; }
                    // TODO end refacto Illu
                    
                    BitmapCanvas.DrawPixel(p, Ia + Id);
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

        public V3 Normal(V3 p) {
            V3 n = p - Center;
            n.Normalize();
            
            return n;
        }

    }

}
