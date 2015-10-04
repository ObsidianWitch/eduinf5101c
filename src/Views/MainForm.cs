using System;
using System.Windows.Forms;
using ImageSynthesis.Renderers;

namespace ImageSynthesis.Views {
    
    partial class MainForm : Form {
        
        private Renderer Renderer;
        
        public MainForm(Renderer renderer) {
            InitializeComponent();
            
            Renderer = renderer;
            InitializeCanvas(Renderer.Canvas);
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
