using ImageSynthesis.Models;
using ImageSynthesis.Lights;

namespace ImageSynthesis.Scenes {
    
    static class BoxScene {
        
        public static void Populate(Scene scene) {
            // Objects
            Rectangle leftWall = new Rectangle(
                center: new V3(0, 250, 250),
                va: new V3(0, 500, 0),
                vb: new V3(0, 0, 500),
                material: new PhongMaterial(
                    color: new Color(0.8f, 0.0f, 0.0f),
                    kA: 1.0f,
                    kD: 0.7f,
                    kS: 0.0f,
                    shininess: 0,
                    reflection: 0.0f,
                    transparency: 0.0f,
                    refractiveIndex: 1.0f
                )
            );
            
            Rectangle rightWall = new Rectangle(
                center: new V3(800, 250, 250),
                va: new V3(0, -500, 0),
                vb: new V3(0, 0, 500),
                material: new PhongMaterial(
                    color: new Color(0.1f, 0.1f, 0.8f),
                    kA: 1.0f,
                    kD: 0.7f,
                    kS: 0.0f,
                    shininess: 0,
                    reflection: 0.0f,
                    transparency: 0.0f,
                    refractiveIndex: 1.0f
                )
            );
            
            Rectangle backWall = new Rectangle(
                center: new V3(400, 500, 250),
                va: new V3(800, 0, 0),
                vb: new V3(0, 0, 500),
                material: new PhongMaterial(
                    color: Color.White,
                    bumpMap: new Texture(
                        textureFile: "bump38.jpg",
                        tileUV: new V2(1.5f, 1.5f)
                    ),
                    kBump: 0.5f,
                    kA: 1.0f,
                    kD: 0.7f,
                    kS: 0.0f,
                    shininess: 0,
                    reflection: 0.0f,
                    transparency: 0.0f,
                    refractiveIndex: 1.0f
                )
            );
            
            Rectangle floor = new Rectangle(
                center: new V3(400, 250, 0),
                va: new V3(800, 0, 0),
                vb: new V3(0, 500, 0),
                material: new PhongMaterial(
                    color: Color.White,
                    kA: 1.0f,
                    kD: 0.7f,
                    kS: 0.0f,
                    shininess: 0,
                    reflection: 0.0f,
                    transparency: 0.0f,
                    refractiveIndex: 1.0f
                )
            );
            
            Rectangle ceiling = new Rectangle(
                center: new V3(400, 250, 500),
                va: new V3(-800, 0, 0),
                vb: new V3(0, 500, 0),
                material: new PhongMaterial(
                    color: Color.White,
                    kA: 1.0f,
                    kD: 0.7f,
                    kS: 0.0f,
                    shininess: 0,
                    reflection: 0.0f,
                    transparency: 0.0f,
                    refractiveIndex: 1.0f
                )
            );
            
            Sphere s1 = new Sphere(
                center: new V3(250, 400, 75),
                radius: 75,
                material: new PhongMaterial(
                    color: new Color(0.5f, 0.5f, 0.5f),
                    kA: 1.0f,
                    kD: 0.0f,
                    kS: 0.7f,
                    shininess: 100,
                    reflection: 1.0f,
                    transparency: 0.0f,
                    refractiveIndex: 1.0f
                )
            );
            
            Sphere s2 = new Sphere(
                center: new V3(650, 400, 50),
                radius: 50,
                material: new PhongMaterial(
                    color: new Color(0.1f, 0.8f, 0.0f),
                    kA: 1.0f,
                    kD: 0.5f,
                    kS: 0.7f,
                    shininess: 100,
                    reflection: 0.0f,
                    transparency: 0.0f,
                    refractiveIndex: 1.0f
                )
            );
            
            Sphere s3 = new Sphere(
                center: new V3(700, 200, 75),
                radius: 75,
                material: new PhongMaterial(
                    color: new Color(0.5f, 0.5f, 0.5f),
                    kA: 1.0f,
                    kD: 0.5f,
                    kS: 0.7f,
                    shininess: 100,
                    reflection: 0.0f,
                    transparency: 0.9f,
                    refractiveIndex: 1.1f
                )
            );
            
            Sphere s4 = new Sphere(
                center: new V3(110, 100, 100),
                radius: 100,
                material: new PhongMaterial(
                    color: Color.Green,
                    texture: new Texture("lead.jpg"),
                    bumpMap: new Texture("lead_bump.jpg"),
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
            
            // Lights
            AmbientLight aL = new AmbientLight(
                new Color(0.3f, 0.3f, 0.3f)
            );
            
            PointLight pL = new PointLight(
                new Color(1.0f, 1.0f, 1.0f),
                new V3(400, 250, 450)
            );
            
            // Populate
            scene.Objects.Add(leftWall);
            scene.Objects.Add(rightWall);
            scene.Objects.Add(backWall);
            scene.Objects.Add(floor);
            scene.Objects.Add(ceiling);
            scene.Objects.Add(s1);
            scene.Objects.Add(s2);
            scene.Objects.Add(s3);
            scene.Objects.Add(s4);
            scene.Lights.Add(pL);
            scene.Lights.Add(aL);
        }
        
    }
}
