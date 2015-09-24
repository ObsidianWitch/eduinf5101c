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

            // dessin sur l'image pour comprendre l'orientation axe et origine
            // du Bitmap
            Color Red   = new Color(1.0f, 0.0f, 0.0f);
            Color Green = new Color(0.0f, 1.0f, 0.0f);
            Color Blue  = new Color(0.0f, 0.0f, 1.0f);
            
            // Draw some lines
            for (int i = 0 ; i < 1000 ; i++) {
                BitmapCanvas.DrawPixel(i, i, Red);
                BitmapCanvas.DrawPixel(i, 1000 - i, Green);
            }
            
            // Draw sphere
            // FIXME Z computed but not used yet, needed for Z-buffer
            float radius = 100;
            V3 center = new V3(100, 100, 0);
            for (float u = 0 ; u < 2 * Mathf.PI ; u += 0.01f) {
                for (float v = -Mathf.PI / 2 ; v < Mathf.PI ; v += 0.01f) {
                    V3 tmp = new V3(
                        (radius * Mathf.Sin(v) * Mathf.Cos(u)) + center.X,
                        (radius * Mathf.Sin(v) * Mathf.Sin(u)) + center.Y,
                        (radius * Mathf.Cos(v)) + center.Z
                    );
                    
                    BitmapCanvas.DrawPixel((int) tmp.X, (int) tmp.Y, Blue);
                }
            }

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
