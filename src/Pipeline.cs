namespace ImageSynthesis {

    static class Pipeline {

        public static void Go() {
            Color red   = new Color(1.0f, 0.0f, 0.0f);
            Color green = new Color(0.0f, 1.0f, 0.0f);
            Color blue  = new Color(0.0f, 0.0f, 1.0f);
            
            // Draw sphere
            Sphere sphere = new Sphere(
                new V3(100, 100, 0), // center
                100,                 // radius
                blue                 // color
            );
            sphere.Draw();
        }

    }
}
