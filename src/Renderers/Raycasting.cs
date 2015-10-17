using System.Collections.Generic;
using ImageSynthesis.Models;
using ImageSynthesis.Views;
using ImageSynthesis.Lights;

namespace ImageSynthesis.Renderers {

    class Raycasting : Renderer {
        
        private V3 CameraPos;
        
        public Raycasting(Canvas canvas, Scene scene, V3 cameraPos) :
            base(canvas, scene)
        {
            CameraPos = cameraPos;
        }

        /// Renders the scene.
        override public void Render() {
            Canvas.BeginDrawing();
            
            for (int x = 0 ; x < Canvas.Width ; x++) {
                for (int y = 0 ; y < Canvas.Height ; y++) {
                    V3 p = new V3(x, 0, y);
                    
                    Ray ray = new Ray(
                        origin: CameraPos,
                        direction: p - CameraPos
                    );
                    
                    Color color = Raycast(ray);
                    
                    if (color != null) {
                        V3 pScreen = new V3(x, Canvas.Height - y, 0);
                        Canvas.DrawPixel(pScreen, color);
                    }
                }
            }
            
            Canvas.EndDrawing();
        }
        
        /// Casts a ray and checks if it intersects any object. We can then
        /// compute the color of the point corresponding to the nearest
        /// intersected object.
        private Color Raycast(Ray ray) {
            // Check if the current ray intersect with any object, and keep the
            // nearest intersected object.
            Object3D collidedObject = null;
            float distance = float.MaxValue;
            foreach (Object3D obj in Scene.Objects) {
                float newDistance;
                bool intersect = obj.Intersect(ray, out newDistance);
                
                if (intersect && newDistance < distance) {
                    collidedObject = obj;
                    distance = newDistance;
                }
            }
            
            // If the ray previously encountered an object, compute the pixel's
            // color.
            if (collidedObject != null) {
                V3 collisionPoint = ray.Origin + (ray.Direction * distance);
                V2 collisionUV = collidedObject.UV(collisionPoint);
                
                List<Light> lights = Occultation(collidedObject, collisionPoint);
                
                return Scene.IlluModel.Compute(
                    lights, collidedObject, collisionPoint, collisionUV
                );
            }
            
            return null;
        }
        
        /// Returns a list of lights from which the currentPoint is visible.
        /// If another object is placed between the currentPoint and a light,
        /// then this point is occulted and the light will not be added to the
        /// list.
        private List<Light> Occultation(Object3D currentObject, V3 currentPoint) {
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
        private bool PointLightened (
            Object3D currentObject, V3 currentPoint, Light light
        ) {
            Ray lightRay;
            if (light.GetType().Name == "PointLight") {
                PointLight pl = (PointLight) light;
                lightRay = new Ray(currentPoint, pl.Position - currentPoint);
            }
            else if (light.GetType().Name == "DirectionalLight") {
                DirectionalLight dl = (DirectionalLight) light;
                lightRay = new Ray(currentPoint, -dl.Direction);
            }
            else if (light.GetType().Name == "AmbientLight") {
                return true;
            }
            else { return false; }
            
            List<Object3D> objects = new List<Object3D>(Scene.Objects);
            objects.Remove(currentObject);
            
            foreach (Object3D o in objects) {
                if (o.Intersect(lightRay)) { return false; }
            }
            
            return true;
        }
    }
}
