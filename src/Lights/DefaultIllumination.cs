using System.Collections.Generic;
using ImageSynthesis.Models;

namespace ImageSynthesis.Lights {
    
    /// This illumination model simply displays the object's color, indendently
    /// from any light in the scene.
    class DefaultIllumination : IlluminationModel {
        
        public DefaultIllumination() {}
        
        override public Color Compute(List<Light> lights, Object3D obj, V3 p) {
            return obj.Color;
        }
        
        override public Color ComputeAmbientLight(
            AmbientLight aL, Object3D obj, V3 p
        ) {
            return new Color(0, 0, 0);
        }
        
        override public Color ComputePointLight(
            PointLight pL, Object3D obj, V3 p
        ) {
            return new Color(0, 0, 0);
        }
    }

}
