using System;
using System.Windows.Forms;
using ImageSynthesis.Models;
using ImageSynthesis.Lights;

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
        
        public static void Run() {
            // Objects
            Sphere s1 = new Sphere(
                center: new V3(200, 200, 200),
                radius: 50,
                color: Color.Red,
                material: new PhongMaterial(
                    kA: 1.0f,
                    kD: 1.0f,
                    kS: 0.5f,
                    shininess: 20
                )
            );

            Sphere s2 = new Sphere(
                center: new V3(450, 200, 200),
                radius: 100,
                color: Color.Green,
                material: new PhongMaterial(
                    kA: 1.0f,
                    kD: 1.0f,
                    kS: 0.0f,
                    shininess: 40
                )
            );
            
            // Lights
            AmbientLight aL = new AmbientLight(
                new Color(0.2f, 0.2f, 0.2f)
            );
            
            PointLight pL = new PointLight(
                new Color(1.0f, 1.0f, 1.0f),
                new V3(0, 0, 200)
            );
            
            
            // Scene
            Scene scene = new Scene(
                new PhongIllumination(
                    cameraPos: new V3(0, 0, 0)
                )
            );
            scene.Lights.Add(aL);
            scene.Lights.Add(pL);
            scene.Objects.Add(s1);
            scene.Objects.Add(s2);
            
            scene.Draw();
        }
    }
}
