using System.Collections.Generic;
using ImageSynthesis.Models;

namespace ImageSynthesis.Lights {
    
    abstract class IlluminationModel {
        
        // FIXME avoid introspection (use Visitor?)
        public Color compute(List<Light> lights, Sphere obj, V3 p) {
            Color illumination = new Color(0, 0, 0);
            
            foreach (Light l in lights) {
                if (l.GetType().Name == "AmbientLight") {
                    illumination += ComputeAmbientLight((AmbientLight) l, obj, p);
                }
                else if (l.GetType().Name == "PointLight") {
                    illumination += ComputePointLight((PointLight) l, obj, p);
                }
            }
            
            return illumination;
        }
        
        abstract public Color ComputeAmbientLight(AmbientLight aL, Sphere obj, V3 p);
        abstract public Color ComputePointLight(PointLight pL, Sphere obj, V3 p);
    }
    
}
