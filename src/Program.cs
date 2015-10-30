using System;
using System.Windows.Forms;
using ImageSynthesis.Views;

namespace ImageSynthesis {

    static class Program {
        
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            
            MainForm Form = new MainForm();
            Application.Run(Form);
        }
        
    }
}
