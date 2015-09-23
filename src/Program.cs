using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ImageSynthesis {

    static class Program {

        static public Views.MainForm Form;

        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            Form = new Views.MainForm();
            Application.Run(Form);
        }
    }
}
