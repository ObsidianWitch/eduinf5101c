using System;
using System.Windows.Forms;
using ImageSynthesis.Models;
using ImageSynthesis.Lights;
using ImageSynthesis.Views;

namespace ImageSynthesis {

    static class Program {

        public static MainForm Form;
        private static Scene Scene;

        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            
            PopulateScene();
            
            Form = new MainForm();
            Application.Run(Form);
        }
        
        private static void PopulateScene() {
            // Objects
            Sphere s1 = new Sphere(
                center: new V3(200, 200, 200),
                radius: 150,
                color: Color.Red,
                material: new PhongMaterial(
                    kA: 1.0f,
                    kD: 1.0f,
                    kS: 0.5f,
                    shininess: 20,
                    bumpMap: new Texture("gold_bump.jpg")
                ),
                texture: new Texture(
                    textureFile: "gold.jpg",
                    tileUV: new V2(1.0f, 1.0f)
                )
            );
            
            Sphere s2 = new Sphere(
                center: new V3(550, 200, 200),
                radius: 150,
                color: Color.Green,
                material: new PhongMaterial(
                    kA: 1.0f,
                    kD: 1.0f,
                    kS: 0.5f,
                    shininess: 40,
                    bumpMap: new Texture("lead_bump.jpg")
                ),
                texture: new Texture("lead.jpg")
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
            Scene = new Scene(
                new PhongIllumination(
                    cameraPos: new V3(0, 0, 0)
                )
            );
            Scene.Lights.Add(aL);
            Scene.Lights.Add(pL);
            Scene.Objects.Add(s1);
            Scene.Objects.Add(s2);
        }
        
        public static void Run() {

            Scene.Draw();
        }
    }
}
