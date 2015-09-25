using System;

namespace ImageSynthesis.Models {

    class Sphere {

        public V3 Center { get; set; }
        public float Radius { get; set; }
        public Color Color { get; set; }
        public PhongMaterial Material { get; set; }

        public Sphere(V3 center, float radius, Color color,
            PhongMaterial material)
        {
            Center = center;
            Radius = radius;
            Color  = color;
            Material = material;
        }

        public void Draw() {
            for (float u = 0 ; u < 2 * Mathf.PI ; u += 0.01f) {
                for (float v = -Mathf.PI / 2 ; v < Mathf.PI / 2 ; v += 0.01f) {
                    V3 p = Point(u,v);
                    
                    // TODO refacto Illu
                    
                    // Ambiant reflection
                    Color ia = new Color(0.2f, 0.2f, 0.2f);
                    Color Ia = Color * ia * Material.KAmbient;
                    
                    // Diffuse reflection
                    V3 lightSrc = new V3(200, -200, 200);
                    
                    V3 n = Normal(p);
                    
                    V3 l = lightSrc - p;
                    l.Normalize();
                    
                    Color id = new Color(1.0f, 1.0f, 1.0f);
                    
                    Color Id = Color * id * Material.KDiffuse * (l * n);
                    
                    // FIXME avoid having negative Id cancelling other components
                    if (Id.R < 0.0) { Id.R = 0.0f; }
                    if (Id.G < 0.0) { Id.G = 0.0f; }
                    if (Id.B < 0.0) { Id.B = 0.0f; }
                    
                    // Specular reflection
                    Color i_s = new Color(1.0f, 1.0f, 1.0f);
                    
                    V3 r = 2 * (n * l) * n - l;
                    V3 viewpoint = p - new V3(200, -150, 1); // FIXME
                    viewpoint.Normalize();
                    
                    Color Is = i_s * Material.KSpecular * (float) Math.Pow((r * viewpoint), Material.Shininess);
                    
                    // TODO end refacto Illu
                    
                    BitmapCanvas.DrawPixel(p, Ia + Id + Is);
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
