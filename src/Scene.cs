using System.Collections.Generic;
using ImageSynthesis.Models;
using ImageSynthesis.Lights;

namespace ImageSynthesis {
    
    /// A scene contains lights and objects and can be rendered (drawn).
    class Scene {
        
        public List<Light> Lights { get; private set; }
        public List<Object3D> Objects { get; private set; }
        
        private IlluminationModel IlluModel;
        private Canvas Canvas;
        
        public Scene(Canvas canvas) : this(canvas, new DefaultIllumination()) {}
        
        public Scene(Canvas canvas, IlluminationModel illuModel) {
            Canvas = canvas;
            IlluModel = illuModel;
            Lights = new List<Light>();
            Objects = new List<Object3D>();
        }
        
        // Draw the whole scene.
        public void Draw() {
            foreach (Object3D obj in Objects) {
                DrawObject(obj);
            }
            
        }
        
        // Draw one object.
        private void DrawObject(Object3D obj) {
            for (float u = 0.0f ; u < 1.0f ; u += 0.001f) {
                for (float v = 0.0f ; v < 1.0f ; v += 0.001f) {
                    V2 uv = new V2(u,v);
                    V3 p = obj.Point(uv);
                    
                    Color illumination = IlluModel.Compute(Lights, obj, p, uv);
                    
                    Canvas.DrawPixel(p, illumination);
                }
            }
        }
    }
    
}
