using System;
using System.Windows.Forms;
using ImageSynthesis.Models;
using ImageSynthesis.Lights;
using ImageSynthesis.Views;

namespace ImageSynthesis {

    static class Program {

        private const int CANVAS_WIDTH = 800;
        private const int CANVAS_HEIGHT = 500;

        public static MainForm Form;
        private static Scene Scene;

        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            
            Canvas canvas = new Canvas(CANVAS_WIDTH, CANVAS_HEIGHT);
            
            Scene = new Scene(
                canvas,
                new PhongIllumination(
                    cameraPos: new V3(0, 0, 0)
                )
            );
            PopulateScene(Scene);
            
            Form = new MainForm(canvas);
            Application.Run(Form);
        }
        
        private static void PopulateScene(Scene scene) {
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
            
            // Populate
            scene.Lights.Add(aL);
            scene.Lights.Add(pL);
            scene.Objects.Add(s1);
            scene.Objects.Add(s2);
        }
        
        public static void Run() {
            Scene.Draw();
        }
    }
}
