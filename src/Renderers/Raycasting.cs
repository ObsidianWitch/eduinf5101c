using System.Collections.Generic;
using ImageSynthesis.Models;
using ImageSynthesis.Views;
using ImageSynthesis.Lights;
using ImageSynthesis.Scenes;

namespace ImageSynthesis.Renderers {

    class Raycasting : RayRenderer {
        
        public Raycasting(Canvas canvas, Scene scene, V3 cameraPos) :
            base(canvas, scene, cameraPos)
        {}

        /// Renders the scene.
        override public void Render() {
            Canvas.BeginDrawing();
            
            for (int x = 0 ; x < Canvas.Width ; x++) {
                for (int y = 0 ; y < Canvas.Height ; y++) {
                    V3 p = new V3(x, 0, y);
                    
                    Color color = Raycast(new Ray(
                        origin: CameraPos,
                        direction: p - CameraPos
                    ));
                    
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
                
                float shadowCoeff = Occultation(collidedObj, collisionPoint);
                
                return shadowCoeff * Scene.IlluModel.Compute(
                    Scene.Lights, collidedObj, collisionPoint, collisionUV
                );
            }
            
            return null;
        }
        
    }
}
