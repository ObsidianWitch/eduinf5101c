using ImageSynthesis.Views;
using ImageSynthesis.Scenes;

namespace ImageSynthesis.Renderers {

    abstract class Renderer {
        
        public Canvas Canvas { get; private set; }
        protected Scene Scene;
        
        public Renderer(Canvas canvas, Scene scene) {
            Canvas = canvas;
            Scene = scene;
        }
        
        /// Renders the scene.
        abstract public void Render();
    }

}
