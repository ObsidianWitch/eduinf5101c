using System;
using System.Windows.Forms;
using ImageSynthesis.Scenes;
using ImageSynthesis.Renderers;
using ImageSynthesis.Lights;
using System.Collections.Generic;

namespace ImageSynthesis.Views {
    
    partial class MainForm : Form {
        
        public const int CANVAS_WIDTH = 800;
        public const int CANVAS_HEIGHT = 500;

        private V3 CameraPos;
        private Dictionary<string, Renderer> Renderers;
        private Renderer CurrentRenderer;
        
        public MainForm() {
            InitializeComponent();
            
            Canvas canvas = new Canvas(CANVAS_WIDTH, CANVAS_HEIGHT);
            InitializeCanvas(canvas);
            
            CameraPos = new V3(CANVAS_WIDTH/2, -1000, CANVAS_HEIGHT/2);
            
            Scene scene = new Scene(new PhongIllumination(CameraPos));
            DefaultScene.PopulateDL(scene);

            InitializeRenderers(canvas, scene);
        }

        private void InitializeRenderers(Canvas canvas, Scene scene) {
            Renderers = new Dictionary<string, Renderer>();

            Renderer zbuffer = new ZBuffer(
                canvas: canvas,
                scene: scene
            );
            Renderers.Add("ZBuffer", zbuffer);

            Renderer raycasting = new Raycasting(
                canvas:    canvas,
                scene:     scene,
                cameraPos: CameraPos
            );
            Renderers.Add("Raycasting", raycasting);

            Renderer raytracing = new Raytracing(
                canvas:    canvas,
                scene:     scene,
                cameraPos: CameraPos,
                maxDepth:  10
            );
            Renderers.Add("Raytracing", raytracing);

            RendererComboBox.DataSource = new BindingSource(Renderers, null);
            RendererComboBox.DisplayMember = "Key";
            RendererComboBox.ValueMember = "Value";
        }

        private void RenderButtonClick(object sender, EventArgs e) {
            CurrentRenderer.Render();
        }

        private void SlowModeToggle(object sender, EventArgs e) {
            CurrentRenderer.Canvas.SwitchDisplayMode(SlowModeCheckbox.Checked);
        }

        private void RendererChanged(object sender, EventArgs e) {
            var selectedItem = (KeyValuePair <string, Renderer>) RendererComboBox.SelectedItem;
            CurrentRenderer = selectedItem.Value;

        }

    }
}
