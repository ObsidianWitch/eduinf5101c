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
        /// compute the color of the point corresponding to the closest
        /// intersected object.
        private Color Raycast(Ray ray) {
            Object3D collidedObj = ray.ClosestIntersectedObject(Scene.Objects);
            
            // If the ray previously encountered an object, compute the pixel's
            // color.
            if (collidedObj != null) {
                V3 collisionPoint = ray.CollisionPoint();
                V2 collisionUV = collidedObj.UV(collisionPoint);
                
                List<Light> lights = Occultation(collidedObj, collisionPoint);
                
                return Scene.IlluModel.Compute(
                    lights, collidedObj, collisionPoint, collisionUV
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
