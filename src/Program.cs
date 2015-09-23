using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ImageSynthesis {

    static class Program {

        static public Views.Form1 Form;

        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            Form = new Views.Form1();
            Application.Run(Form);
        }
    }
}
