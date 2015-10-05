using ImageSynthesis.Models;
using ImageSynthesis.Views;

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
                    Color color = raycast(p);
                    
                    if (color != null) {
                        V3 pScreen = new V3(x, Canvas.Height - y, 0);
                        Canvas.DrawPixel(pScreen, color);
                    }
                }
            }
            
            Canvas.EndDrawing();
        }
        
        private Color raycast(V3 p) {
            V3 rayDirection = p - CameraPos;
            rayDirection.Normalize();
            
            // Check if the current ray intersect with any object, and keep the
            // first intersected object.
            Object3D collidedObject = null;
            foreach (Object3D obj in Scene.Objects) {
                if (obj.intersect(CameraPos, p)) {
                    collidedObject = obj;
                }
            }
            
            // If the ray previously encountered an object, compute the pixel's
            // color.
            if (collidedObject != null) {
                V3 collisionPoint = CameraPos + (rayDirection * distance);
                
                return Scene.IlluModel.Compute(
                    Scene.Lights, collidedObject, collisionPoint
                );
            }
            
            return null;
        }
    }
}
