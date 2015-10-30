using System.Collections.Generic;
using ImageSynthesis.Models;
using ImageSynthesis.Views;
using ImageSynthesis.Lights;
using ImageSynthesis.Scenes;

namespace ImageSynthesis.Renderers {
    
    /// Base class for ray based renderers.
    abstract class RayRenderer : Renderer {
        
        protected V3 CameraPos;
        
        public RayRenderer(
            Canvas canvas, Scene scene, V3 cameraPos
        ) : base(canvas, scene)
        {
            CameraPos = cameraPos;
        }
        
        /// Returns a list of lights from which the currentPoint is visible.
        /// If another object is placed between the currentPoint and a light,
        /// then this point is occulted and the light will not be added to the
        /// list.
        protected List<Light> Occultation(Object3D currentObject, V3 currentPoint) {
            List<Light> lights = new List<Light>();
            
            foreach (Light l in Scene.Lights) {
                if (PointLightened(currentObject, currentPoint, l)) {
                    lights.Add(l);
                }
            }
            
            return lights;
        }
        
        /// Checks whether the specified light is able to lightens the
        /// currentPoint (i.e. there is no obstacle between them).
        protected bool PointLightened (
            Object3D currentObject, V3 currentPoint, Light light
        ) {
            Ray lightRay;
            
            if (light.GetType().Name == "PointLight") {
                PointLight pl = (PointLight) light;
                lightRay = new Ray(
                    origin:       currentPoint,
                    direction:    pl.Position - currentPoint,
                    originObject: currentObject
                );
            }
            else if (light.GetType().Name == "DirectionalLight") {
                DirectionalLight dl = (DirectionalLight) light;
                lightRay = new Ray(
                    origin:       currentPoint,
                    direction:    -dl.Direction,
                    originObject: currentObject
                );
            }
            else if (light.GetType().Name == "AmbientLight") {
                return true;
            }
            else { return false; }
            
            return !lightRay.IntersectObject(Scene.Objects);
        }
        
    }
}
