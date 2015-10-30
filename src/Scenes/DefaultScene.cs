using ImageSynthesis.Models;
using ImageSynthesis.Lights;

namespace ImageSynthesis.Scenes {
    
    static class DefaultScene {
        
        /// Default Scene with only AmbientLight.
        private static void Populate(Scene scene) {
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
                center: new V3(120, 1, 380),
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
            
            // Populate
            scene.Lights.Add(aL);
            scene.Objects.Add(s1);
            scene.Objects.Add(s2);
            scene.Objects.Add(s3);
            scene.Objects.Add(s4);
            scene.Objects.Add(r1);
        }
        
        /// Default Scene with one DirectionalLight.
        public static void PopulateDL(Scene scene) {
            Populate(scene);
            
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
            scene.Lights.Add(dL);
        }
        
        /// Default Scene with two PointLights.
        public static void PopulatePL(Scene scene) {
            Populate(scene);
            
            PointLight pL1 = new PointLight(
                new Color(1.0f, 1.0f, 1.0f),
                new V3(700, 0, 200)
            );
            
            PointLight pL2 = new PointLight(
                new Color(1.0f, 1.0f, 1.0f),
                new V3(0, 0, 200)
            );
            
            // Populate
            scene.Lights.Add(pL1);
            scene.Lights.Add(pL2);
        }
        
    }
}
