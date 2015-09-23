using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ImageSynthesis {

    static class Program {

        static public Form1 MyForm;

        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            MyForm = new Form1();
            Application.Run(MyForm);
        }
    }
}
