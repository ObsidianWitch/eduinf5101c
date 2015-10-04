using System.Collections.Generic;
using ImageSynthesis.Models;
using ImageSynthesis.Lights;
using ImageSynthesis.Views;

namespace ImageSynthesis {
    
    /// A scene contains lights and objects and can be rendered (drawn).
    class Scene {
        
        public List<Light> Lights { get; private set; }
        public List<Object3D> Objects { get; private set; }
        public IlluminationModel IlluModel { get; private set; }
        
        public Scene(Canvas canvas) : this(canvas, new DefaultIllumination()) {}
        
        public Scene(Canvas canvas, IlluminationModel illuModel) {
            IlluModel = illuModel;
            Lights = new List<Light>();
            Objects = new List<Object3D>();
        }
        
    }
    
}
