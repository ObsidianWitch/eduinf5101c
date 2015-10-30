using ImageSynthesis.Models;
using ImageSynthesis.Views;
using ImageSynthesis.Scenes;

namespace ImageSynthesis.Renderers {

    class ZBuffer : Renderer {

        private float[,] Data;

        public ZBuffer(Canvas canvas, Scene scene) : base(canvas, scene) {
            Data = new float[Canvas.Width,Canvas.Height];
            clear();
        }

        /// Renders the scene.
        override public void Render() {
            clear();
            Canvas.BeginDrawing();
            foreach (Object3D obj in Scene.Objects) {
                RenderObject(obj);
            }
            Canvas.EndDrawing();
        }

        /// Renders one object.
        private void RenderObject(Object3D obj) {
            for (float u = 0.0f ; u < 1.0f ; u += 0.001f) {
                for (float v = 0.0f ; v < 1.0f ; v += 0.001f) {
                    V2 uv = new V2(u,v);
                    V3 p = obj.Point(uv);
                    
                    V3 pScreen = new V3(
                        x: p.X,
                        y: Canvas.Height - p.Z,
                        z: p.Y
                    );
                    bool objectVisible = Set(pScreen);
                    
                    if (objectVisible) {
                        Color illumination = Scene.IlluModel.Compute(
                            Scene.Lights, obj, p, uv
                        );
                        
                        Canvas.DrawPixel(pScreen, illumination);
                    }
                }
            }
        }

        private bool Set(V3 pScreen) {
            return Set((int) pScreen.X, (int) pScreen.Y, pScreen.Z);
        }

        /// Sets the value 'z' at position (x,y) of the z-buffer.
        /// If the new value is farther from the viewer than the previous one,
        /// then do not set the new value.
        /// Returns whether the value was set or not.
        private bool Set(int x, int y, float z) {
            if (z < Data[x,y]) {
                Data[x,y] = z;
                return true;
            }
            else {
                return false;
            }
        }

        private void clear() {
            for (int x = 0 ; x < Canvas.Width ; x++) {
                for (int y = 0 ; y < Canvas.Height ; y++) {
                    Data[x,y] = float.PositiveInfinity;
                }
            }
        }
    }
}
