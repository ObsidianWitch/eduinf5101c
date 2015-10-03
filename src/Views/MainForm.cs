using System;
using System.Windows.Forms;

namespace ImageSynthesis.Views {
    
    partial class MainForm : Form {
        
        private Scene Scene;
        
        public MainForm(Scene scene) {
            InitializeComponent();
            
            Scene = scene;
            InitializeCanvas(Scene.Canvas);
        }

        private void RenderButtonClick(object sender, EventArgs e) {
            Scene.Draw();
        }

        private void SlowModeToggle(object sender, EventArgs e) {
            Scene.Canvas.Mode = SlowModeCheckbox.Checked ? DisplayMode.SLOW :
                                                     DisplayMode.FAST;
        }

    }
}
