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
        
        /// Returns a shadow coefficient based on the list of lights visible
        /// from the currentPoint.
        protected float Occultation(Object3D currentObject, V3 currentPoint) {
            int lightsSize = Scene.Lights.Count;
            float shadowCoeff = 0.0f;
            
            foreach (Light l in Scene.Lights) {
                if (PointLightened(currentObject, currentPoint, l)) {
                    shadowCoeff += 1.0f;
                }
            }
            
            shadowCoeff /= lightsSize;
            
            return shadowCoeff;
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
            
            float obstacleDistance;
            bool intersect = lightRay.IntersectObject(
                Scene.Objects, out obstacleDistance
            );
            
            if (light.GetType().Name == "PointLight") {
                PointLight pl = (PointLight) light;
                float lightDistance = (pl.Position - currentPoint).Norm1();
                
                // Discard intersection if the obstacle is behind the PointLight
                // compared to the currentPoint.
                bool intersectBehind = obstacleDistance > lightDistance;
                return !intersect || (intersect && intersectBehind);
            }
            
            return !intersect;
        }
        
    }
}
