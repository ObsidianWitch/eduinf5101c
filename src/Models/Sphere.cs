using System;
using System.Collections.Generic;
using ImageSynthesis.Lights;

namespace ImageSynthesis.Models {

    class Sphere : Object3D {
        
        public float Radius { get; set; }

        public Sphere(
            V3 center, float radius, Color color, PhongMaterial material
        ) : base(center, color, material)
        {
            Radius = radius;
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
                    
                    IlluminationModel illuModel = new PhongIllumination(
                        cameraPos: new V3(0, 0, 0)
                    );
                    List<Light> lights = new List<Light>();
                    lights.Add(aL);
                    lights.Add(pL);
                    
                    Color illumination = illuModel.compute(lights, this, p);
                    
                    BitmapCanvas.DrawPixel(p, illumination);
                }
            }
        }

        override public V3 Point(float u, float v) {
            return new V3(
                Center.X + (Radius * Mathf.Cos(v) * Mathf.Cos(u)),
                Center.Y + (Radius * Mathf.Cos(v) * Mathf.Sin(u)),
                Center.Z + (Radius * Mathf.Sin(v))
            );
        }

        override public V3 Normal(V3 p) {
            V3 n = p - Center;
            n.Normalize();
            
            return n;
        }
    }

}
