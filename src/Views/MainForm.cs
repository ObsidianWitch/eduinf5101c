using System;
using System.Windows.Forms;
using ImageSynthesis.Renderers;
using ImageSynthesis.Lights;

namespace ImageSynthesis.Views {
    
    partial class MainForm : Form {
        
        public const int CANVAS_WIDTH = 800;
        public const int CANVAS_HEIGHT = 500;
        
        private Renderer Renderer;
        
        public MainForm() {
            InitializeComponent();
            
            Canvas canvas = new Canvas(CANVAS_WIDTH, CANVAS_HEIGHT);
            InitializeCanvas(canvas);
            
            V3 cameraPos = new V3(CANVAS_WIDTH/2, -1000, CANVAS_HEIGHT/2);
            
            Scene scene = new Scene(new PhongIllumination(cameraPos));
            Program.PopulateScene(scene);
            
            Renderer = new Raytracing(
                canvas:    canvas,
                scene:     scene,
                cameraPos: cameraPos,
                maxDepth:  10
            );
        }

        private void RenderButtonClick(object sender, EventArgs e) {
            Renderer.Render();
        }

        private void SlowModeToggle(object sender, EventArgs e) {
            Renderer.Canvas.Mode = SlowModeCheckbox.Checked ?
                DisplayMode.SLOW : DisplayMode.FAST;
        }

    }
}
