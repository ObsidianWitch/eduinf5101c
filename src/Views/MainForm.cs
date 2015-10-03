using System;
using System.Windows.Forms;

namespace ImageSynthesis.Views {
    
    partial class MainForm : Form {
        
        public MainForm() {
            InitializeComponent();
            pictureBox1.Image = Canvas.Init(
                pictureBox1.Width,
                pictureBox1.Height,
                DisplayMode.SLOW
            );
        }

        public void PictureBoxRefresh() {
            pictureBox1.Refresh();
        }

        private void RenderButtonClick(object sender, EventArgs e) {
            Canvas.Refresh(new Color(0, 0, 0));
            Program.Run();
            Canvas.Show();
        }

        private void SlowModeToggle(object sender, EventArgs e) {
            Canvas.Mode = SlowModeCheckbox.Checked ? DisplayMode.SLOW :
                                                     DisplayMode.FAST;
        }

    }
}
