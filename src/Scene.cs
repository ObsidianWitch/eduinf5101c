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
            // FIXME u in [0 ; pi] and v in [-pi/2 ; pi/2] are specific to
            // a sphere.
            // idea: add a UVrange() method for each Object3D subclass
            for (float u = 0 ; u < 2 * Mathf.PI ; u += 0.01f) {
                for (float v = -Mathf.PI / 2 ; v < Mathf.PI / 2 ; v += 0.01f) {
                    V3 p = obj.Point(u,v);
                    
                    Color illumination = IlluModel.Compute(Lights, obj, p);
                    
                    BitmapCanvas.DrawPixel(p, illumination);
                }
            }
        }
    }
    
}
