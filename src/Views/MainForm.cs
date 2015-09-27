using System;
using System.Windows.Forms;

namespace ImageSynthesis.Views {
    
    public partial class MainForm : Form {
        
        public MainForm() {
            InitializeComponent();
            pictureBox1.Image = Canvas.Init(
                pictureBox1.Width,
                pictureBox1.Height,
                DisplayMode.SLOW
            );
        }

        public void PictureBoxInvalidate() { pictureBox1.Invalidate(); }
        public void PictureBoxRefresh()    { pictureBox1.Refresh();    }

        private void button1_Click(object sender, EventArgs e) {
            Canvas.Refresh(new Color(0,0,0));
            Program.Run();
            Canvas.Show();
        }
        
        private void checkbox1_clicked(object sender, EventArgs e) {
            Canvas.Mode = checkBox1.Checked ? DisplayMode.SLOW :
                                              DisplayMode.FAST;
        }
    }
}
