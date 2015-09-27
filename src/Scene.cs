using System.Collections.Generic;
using ImageSynthesis.Models;
using ImageSynthesis.Lights;

namespace ImageSynthesis {
    
    /// A scene contains lights and objects and can be rendered (drawn).
    class Scene {
        
        public List<Light> Lights { get; private set; }
        public List<Object3D> Objects { get; private set; }
        private IlluminationModel IlluModel;
        
        public Scene() : this(new DefaultIllumination()) {}
        
        public Scene(IlluminationModel illuModel) {
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
            UVRange range = obj.UVRange();
            
            for (float u = range.UMin ; u < range.UMax ; u += 0.01f) {
                for (float v = range.VMin ; v < range.VMax ; v += 0.01f) {
                    V3 p = obj.Point(u,v);
                    
                    Color illumination = IlluModel.Compute(Lights, obj, p, u, v);
                    
                    Canvas.DrawPixel(p, illumination);
                }
            }
        }
    }
    
}
