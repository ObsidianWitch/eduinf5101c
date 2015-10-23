using System;
using System.Windows.Forms;
using ImageSynthesis.Models;
using ImageSynthesis.Lights;
using ImageSynthesis.Views;
using ImageSynthesis.Renderers;

namespace ImageSynthesis {

    static class Program {

        private const int CANVAS_WIDTH = 800;
        private const int CANVAS_HEIGHT = 500;

        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            
            Canvas canvas = new Canvas(CANVAS_WIDTH, CANVAS_HEIGHT);
            
            V3 cameraPos = new V3(CANVAS_WIDTH/2, -1000, CANVAS_HEIGHT/2);
            
            Scene scene = new Scene(new PhongIllumination(cameraPos));
            PopulateScene(scene);
            
            Renderer renderer = new Raytracing(
                canvas:    canvas,
                scene:     scene,
                cameraPos: cameraPos,
                maxDepth:  10
            );
            
            MainForm Form = new MainForm(renderer);
            Application.Run(Form);
        }
        
        private static void PopulateScene(Scene scene) {
            // Objects
            Sphere s1 = new Sphere(
                center: new V3(200, 300, 200),
                radius: 150,
                material: new PhongMaterial(
                    color: Color.Red,
                    texture: new Texture(
                        textureFile: "gold.jpg",
                        tileUV: new V2(1.0f, 1.0f)
                    ),
                    bumpMap: new Texture("gold_bump.jpg"),
                    kBump: 0.5f,
                    kA: 1.0f,
                    kD: 0.7f,
                    kS: 0.5f,
                    shininess: 20,
                    reflection: 1.0f,
                    transparency: 0.0f,
                    refractiveIndex: 1.0f
                )
            );
            
            Sphere s2 = new Sphere(
                center: new V3(500, 200, 200),
                radius: 150,
                material: new PhongMaterial(
                    color: Color.Green,
                    texture: new Texture("lead.jpg"),
                    bumpMap: new Texture("lead_bump.jpg"),
                    kBump: 0.5f,
                    kA: 1.0f,
                    kD: 0.7f,
                    kS: 0.5f,
                    shininess: 40,
                    reflection: 0.5f,
                    transparency: 0.7f,
                    refractiveIndex: 1.2f
                )
            );
            
            Sphere s3 = new Sphere(
                center: new V3(350, 400, 200),
                radius: 150,
                material: new PhongMaterial(
                    color: Color.Green,
                    texture: new Texture("wood.jpg"),
                    bumpMap: new Texture("bump38.jpg"),
                    kBump: 1.0f,
                    kA: 1.0f,
                    kD: 0.7f,
                    kS: 0.5f,
                    shininess: 40,
                    reflection: 0.0f,
                    transparency: 0.0f,
                    refractiveIndex: 1.0f
                )
            );
            
            Sphere s4 = new Sphere(
                center: new V3(600, 300, 200),
                radius: 50,
                material: new PhongMaterial(
                    color: Color.Green,
                    kA: 1.0f,
                    kD: 0.7f,
                    kS: 0.5f,
                    shininess: 40,
                    reflection: 0.0f,
                    transparency: 0.0f,
                    refractiveIndex: 1.2f
                )
            );
            
            Rectangle r1 = new Rectangle(
                center: new V3(120, 1, CANVAS_HEIGHT - 120),
                va: new V3(200, 200, 0),
                vb: new V3(0, 0, 200),
                material: new PhongMaterial(
                    color: Color.Blue,
                    texture: new Texture("fibre.jpg"),
                    bumpMap: new Texture("bump38.jpg"),
                    kA: 1.0f,
                    kD: 0.7f,
                    kS: 0.0f,
                    shininess: 0,
                    reflection: 0.0f,
                    transparency: 0.0f,
                    refractiveIndex: 1.0f
                )
            );
            
            // Lights
            AmbientLight aL = new AmbientLight(
                new Color(0.3f, 0.3f, 0.3f)
            );
            
            PointLight pL1 = new PointLight(
                new Color(1.0f, 1.0f, 1.0f),
                new V3(700, 0, 200)
            );
            
            PointLight pL2 = new PointLight(
                new Color(1.0f, 1.0f, 1.0f),
                new V3(0, 0, 200)
            );
            
            DirectionalLight dL = new DirectionalLight(
                new Color(1.0f, 1.0f, 1.0f),
                new V3(-1, 1, -1)
            );
            
            // Populate
            scene.Lights.Add(aL);
            //scene.Lights.Add(pL1);
            //scene.Lights.Add(pL2);
            scene.Lights.Add(dL);
            scene.Objects.Add(s1);
            scene.Objects.Add(s2);
            scene.Objects.Add(s3);
            scene.Objects.Add(s4);
            scene.Objects.Add(r1);
        }
        
    }
}
