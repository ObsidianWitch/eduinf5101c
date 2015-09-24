namespace ImageSynthesis {

    static class Pipeline {

        public static void Go() {
            Texture T1 = new Texture("brick01.jpg");
           
            int larg = 600;
            int haut = 300;
            float r_x = 1.5f; // repetition de la texture en x
            float r_y = 1.0f; // repetition de la texture en y
            float pas = 0.001f;

            // echantillonage fnt paramétrique
            for (float u = 0 ; u < 1 ; u += pas) {
                for (float v = 0 ; v < 1 ; v += pas) {
                    // calcul des coordonnées planes
                    int x = (int) (u * larg + 10);
                    int y = (int) (v * haut + 15);
                    
                    Color c = T1.readColor(u * r_x, v * r_y);
                    
                    BitmapCanvas.DrawPixel(x,y,c);
                }
            }

            // dessin sur l'image pour comprendre l'orientation axe et origine
            // du Bitmap
            Color Red   = new Color(1.0f, 0.0f, 0.0f);
            Color Green = new Color(0.0f, 1.0f, 0.0f);
            
            for (int i = 0 ; i < 1000 ; i++) {
                BitmapCanvas.DrawPixel(i, i, Red);
                BitmapCanvas.DrawPixel(i, 1000-i, Green);
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
