using System;
using ImageSynthesis.Lights;

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
                    
                    AmbientLight aL = new AmbientLight(
                        new Color(0.2f, 0.2f, 0.2f)
                    );
                    
                    PointLight pL = new PointLight(
                        new Color(1.0f, 1.0f, 1.0f),
                        new V3(0, 0, 200)
                    );
                    
                    PhongIllumination illuModel = new PhongIllumination();
                    Color I = illuModel.ComputeAmbientLight(aL, this, p) +
                              illuModel.ComputePointLight(pL, this, p);
                    
                    BitmapCanvas.DrawPixel(p, I);
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
