using System;
using System.Windows.Forms;

namespace ImageSynthesis.Views
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            pictureBox1.Image = BitmapCanvas.Init(pictureBox1.Width, pictureBox1.Height);
        }

        public bool Checked()              { return checkBox1.Checked;   }
        public void PictureBoxInvalidate() { pictureBox1.Invalidate(); }
        public void PictureBoxRefresh()    { pictureBox1.Refresh();    }

        private void button1_Click(object sender, EventArgs e)
        {
            BitmapCanvas.Refresh(new Color(0,0,0));
            Program.Run();
            BitmapCanvas.Show();
        }
    }
}
