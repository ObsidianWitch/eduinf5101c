using ImageSynthesis.Models;

namespace ImageSynthesis {

    static class Pipeline {

        public static void Go() {
            // Draw spheres
            Sphere s1 = new Sphere(
                new V3(200, 0, 200), // center
                100,                 // radius
                Color.Red            // color
            );

            Sphere s2 = new Sphere(
                new V3(450, 0, 200), // center
                100,                 // radius
                Color.Green          // color
            );

            s1.Draw();
            s2.Draw();
        }

    }
}
