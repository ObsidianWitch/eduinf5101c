namespace ImageSynthesis {

    static class Pipeline {

        public static void Go() {
            // Draw sphere
            Sphere sphere = new Sphere(
                new V3(100, 100, 0), // center
                100,                 // radius
                Color.Red            // color
            );
            sphere.Draw();
        }

    }
}
