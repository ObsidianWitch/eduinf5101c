using System.Collections.Generic;
using ImageSynthesis.Models;
using ImageSynthesis.Lights;

namespace ImageSynthesis.Scenes {
    
    /// A scene contains lights and objects and can be rendered (drawn).
    class Scene {
        
        public List<Light> Lights { get; private set; }
        public List<Object3D> Objects { get; private set; }
        public IlluminationModel IlluModel { get; private set; }
        
        public float RefractiveIndex { get; private set; }
        
        public Scene(
            IlluminationModel illuModel, float refractiveIndex = 1.0f
        ) {
            IlluModel = illuModel;
            RefractiveIndex = refractiveIndex;
            
            Lights = new List<Light>();
            Objects = new List<Object3D>();
        }
        
    }
    
}
