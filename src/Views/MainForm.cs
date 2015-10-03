using System;
using System.Windows.Forms;

namespace ImageSynthesis.Views {
    
    partial class MainForm : Form {
        
        public MainForm(Canvas canvas) {
            InitializeComponent();
            InitializeCanvas(canvas);
        }

        private void RenderButtonClick(object sender, EventArgs e) {
            Canvas.BeginDrawing();
            Program.Run();
            Canvas.EndDrawing();
        }

        private void SlowModeToggle(object sender, EventArgs e) {
            Canvas.Mode = SlowModeCheckbox.Checked ? DisplayMode.SLOW :
                                                     DisplayMode.FAST;
        }

    }
}
