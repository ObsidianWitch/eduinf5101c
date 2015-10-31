using System;
using System.Windows.Forms;
using ImageSynthesis.Scenes;
using ImageSynthesis.Renderers;
using ImageSynthesis.Lights;
using System.Collections.Generic;

namespace ImageSynthesis.Views {
    
    partial class MainForm : Form {
        
        private V3 CameraPos;
        private Dictionary<string, Renderer> Renderers;
        private Renderer CurrentRenderer;
        private Dictionary<string, Scene> Scenes;
        private Scene CurrentScene;
        
        public MainForm() {
            InitializeComponent();
            InitializeCanvas();
            
            CameraPos = new V3(Canvas.Width/2, -1000, Canvas.Height/2);

            InitializeScenes();
            InitializeRenderers();
        }

        private void InitializeScenes() {
            Scenes = new Dictionary<string, Scene>();
            
            PhongIllumination illu = new PhongIllumination(CameraPos);
            
            Scene boxScene = new Scene(illu);
            BoxScene.Populate(boxScene);
            Scenes.Add("Box", boxScene);
            
            Scene defaultSceneDL = new Scene(illu);
            DefaultScene.PopulateDL(defaultSceneDL);
            Scenes.Add("DefaultDL", defaultSceneDL);

            Scene defaultScenePL = new Scene(illu);
            DefaultScene.PopulatePL(defaultScenePL);
            Scenes.Add("DefaultPL", defaultScenePL);
            
            SceneComboBox.DataSource = new BindingSource(Scenes, null);
            SceneComboBox.DisplayMember = "Key";
            SceneComboBox.ValueMember = "Value";
        }

        private void InitializeRenderers() {
            Renderers = new Dictionary<string, Renderer>();

            Renderer zbuffer = new ZBuffer(
                canvas: Canvas,
                scene:  CurrentScene
            );
            Renderers.Add("ZBuffer", zbuffer);

            Renderer raycasting = new Raycasting(
                canvas:    Canvas,
                scene:     CurrentScene,
                cameraPos: CameraPos
            );
            Renderers.Add("Raycasting", raycasting);

            Renderer raytracing = new Raytracing(
                canvas:    Canvas,
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
