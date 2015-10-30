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
        private Dictionary<string, Scene> Scenes;
        private Scene CurrentScene;
        
        public MainForm() {
            InitializeComponent();
            
            Canvas canvas = new Canvas(CANVAS_WIDTH, CANVAS_HEIGHT);
            InitializeCanvas(canvas);
            
            CameraPos = new V3(CANVAS_WIDTH/2, -1000, CANVAS_HEIGHT/2);

            InitializeScenes();
            InitializeRenderers(canvas);
        }

        private void InitializeScenes() {
            Scenes = new Dictionary<string, Scene>();

            Scene defaultSceneDL = new Scene(new PhongIllumination(CameraPos));
            DefaultScene.PopulateDL(defaultSceneDL);
            Scenes.Add("DefaultDL", defaultSceneDL);

            Scene defaultScenePL = new Scene(new PhongIllumination(CameraPos));
            DefaultScene.PopulatePL(defaultScenePL);
            Scenes.Add("DefaultPL", defaultScenePL);

            SceneComboBox.DataSource = new BindingSource(Scenes, null);
            SceneComboBox.DisplayMember = "Key";
            SceneComboBox.ValueMember = "Value";
        }

        private void InitializeRenderers(Canvas canvas) {
            Renderers = new Dictionary<string, Renderer>();

            Renderer zbuffer = new ZBuffer(
                canvas: canvas,
                scene:  CurrentScene
            );
            Renderers.Add("ZBuffer", zbuffer);

            Renderer raycasting = new Raycasting(
                canvas:    canvas,
                scene:     CurrentScene,
                cameraPos: CameraPos
            );
            Renderers.Add("Raycasting", raycasting);

            Renderer raytracing = new Raytracing(
                canvas:    canvas,
                scene:     CurrentScene,
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

        private void SceneChanged(object sender, EventArgs e) {
            var selectedItem = (KeyValuePair<string, Scene>)SceneComboBox.SelectedItem;
            CurrentScene = selectedItem.Value;

            // Avoid problems during initialization (Scenes are initialized first, then Renderers).
            if (Renderers == null) { return; }

            foreach (Renderer renderer in Renderers.Values) {
                renderer.Scene = CurrentScene;
            }
        }
    }
}
