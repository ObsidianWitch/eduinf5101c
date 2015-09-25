using ImageSynthesis.Models;

namespace ImageSynthesis {

    static class Pipeline {

        public static void Go() {
            // Draw spheres
            Sphere s1 = new Sphere(
                center: new V3(200, 200, 200),
                radius: 100,
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

            s1.Draw();
            s2.Draw();
        }

    }
}
