using System.Collections.Generic;
using ImageSynthesis.Models;

namespace ImageSynthesis.Lights {
    
    abstract class IlluminationModel {
        
        // FIXME avoid introspection (use Visitor?)
        virtual public Color Compute(
            List<Light> lights, Object3D obj, V3 p, V2 uv
        ) {
            Color illumination = new Color(0, 0, 0);
            
            foreach (Light l in lights) {
                if (l.GetType().Name == "AmbientLight") {
                    illumination += ComputeAmbientLight(
                        (AmbientLight) l, obj, p, uv
                    );
                }
                else if (l.GetType().Name == "PointLight") {
                    illumination += ComputePointLight(
                        (PointLight) l, obj, p, uv
                    );
                }
            }
            
            return illumination;
        }
        
        abstract public Color ComputeAmbientLight(
            AmbientLight aL, Object3D obj, V3 p, V2 uv
        );
        abstract public Color ComputePointLight(
            PointLight pL, Object3D obj, V3 p, V2 uv
        );
    }
    
}
