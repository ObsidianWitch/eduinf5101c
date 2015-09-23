using System.IO;﻿
using System.Drawing;
using System.Drawing.Imaging;

namespace ImageSynthesis {

    class Texture {

        int Height;
        int Width;
        Couleur [,] C;

        public Texture(string textureFile) {
            // VisualStudio: Path.GetFullPath("..\\..")
            string path = Path.Combine(
                Path.GetFullPath("resources"),
                "textures",
                textureFile
            );
            Bitmap bmp = new Bitmap(path);
            
            Height = bmp.Height;
            Width = bmp.Width;
            
            BitmapData data = bmp.LockBits(
                new Rectangle(0, 0, bmp.Width, bmp.Height),
                ImageLockMode.ReadWrite,
                PixelFormat.Format24bppRgb
            );
            
            int stride = data.Stride;
             
            C = new Couleur[Width, Height];
            
            unsafe {
                byte* ptr = (byte*) data.Scan0;
                for (int x = 0 ; x < Width ; x++) {
                    for (int y = 0 ; y < Height ; y++) {
                        byte RR, VV, BB;
                        BB = ptr[(x * 3) + y * stride];
                        VV = ptr[(x * 3) + y * stride + 1];
                        RR = ptr[(x * 3) + y * stride + 2];
                        C[x, y].From255(RR, VV, BB);
                    }
                }
            }
            
            bmp.UnlockBits(data);
            bmp.Dispose();
        }

        /// u,v in [0,1]
        public Couleur readColor(float u, float v) {
            return Interpolate(Width * u, Height * v);
        }

        public void Bump(float u, float v, out float dhdu, out float dhdv) {
            float x = u * Height;
            float y = v * Width;

            float vv = Interpolate(x, y).GreyLevel();
            float vx = Interpolate(x + 1, y).GreyLevel();
            float vy = Interpolate(x, y + 1).GreyLevel();

            dhdu = vx - vv;
            dhdv = vy - vv;
        }

        private Couleur Interpolate(float Lu, float Hv) {
            int x = (int) Lu;  // plus grand entier <=
            int y = (int) Hv;
            
            // float cx = Lu - x; // reste
            // float cy = Hv - y;
          
            x %= Width;
            y %= Height;
            if (x < 0) { x += Width; }
            if (y < 0) { y += Height; }
            
            return C[x, y];
            
            /*
            int xpu = (x + 1) % Width;
            int ypu = (y + 1) % Height;
            
            float ccx = cx * cx;
            float ccy = cy * cy;
            
            return C[x, y] * (1 - ccx) * (1 - ccy) +
                   C[xpu, y] * ccx * (1 - ccy) +
                   C[x, ypu] * (1 - ccx) * ccy +
                   C[xpu, ypu] * ccx * ccy;
            */
        }
    }

}
