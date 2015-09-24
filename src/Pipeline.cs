namespace ImageSynthesis {

    static class Pipeline {

        public static void Go() {
            Texture T1 = new Texture("brick01.jpg");
           
            int width = 600;
            int height = 300;
            float r_x = 1.5f; // repetition de la texture en x
            float r_y = 1.0f; // repetition de la texture en y
            float step = 0.001f;

            // echantillonage fnt paramétrique
            for (float u = 0 ; u < 1 ; u += step) {
                for (float v = 0 ; v < 1 ; v += step) {
                    // calcul des coordonnées planes
                    int x = (int) (u * width + 10);
                    int y = (int) (v * height + 15);
                    
                    Color c = T1.readColor(u * r_x, v * r_y);
                    
                    BitmapCanvas.DrawPixel(x,y,c);
                }
            }

            Color red   = new Color(1.0f, 0.0f, 0.0f);
            Color green = new Color(0.0f, 1.0f, 0.0f);
            Color blue  = new Color(0.0f, 0.0f, 1.0f);
            
            // Draw some lines
            for (int i = 0 ; i < 1000 ; i++) {
                BitmapCanvas.DrawPixel(i, i, red);
                BitmapCanvas.DrawPixel(i, 1000 - i, green);
            }
            
            // Draw sphere
            Sphere sphere = new Sphere(
                new V3(100, 100, 0), // center
                100,                 // radius
                blue                 // color
            );
            sphere.Draw();

            // test des opérations sur les vecteurs
            V3 t = new V3(1, 0, 0);
            V3 r = new V3(0, 1, 0);
            V3 k = t + r;
            float p = k * t * 2;
            V3 n = t ^ r;
            V3 m = -t;
        }

    }
}
