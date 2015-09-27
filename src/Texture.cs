using System.IO;﻿
using System.Drawing;
using System.Drawing.Imaging;

namespace ImageSynthesis {

    class Texture {

        private int Height;
        private int Width;
        private Color[,] C;
        
        /// Amount of tiling in the u direction.
        private float TileU;
        
        /// Amount of tiling in the v direction.
        private float TileV;
        
        public Texture(
            string textureFile, float tileU = 1.0f, float tileV = 1.0f
        ) {
            Bitmap bmp = Open(textureFile);
            
            Height = bmp.Height;
            Width = bmp.Width;
            
            FillColors(bmp);
            
            bmp.Dispose();
            
            TileU = tileU;
            TileV = tileV;
        }

        /// u,v in [0,1]
        public Color Color(float u, float v) {
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

        /// Opens an image texture file.
        private Bitmap Open(string textureFile) {
            // FIXME VisualStudio: Path.GetFullPath("..\\..")
            string path = Path.Combine(
                Path.GetFullPath("resources"),
                "textures",
                textureFile
            );
            
            return new Bitmap(path);
        }

        /// Fills the Colors array using the given Bitmap.
        private void FillColors(Bitmap bmp) {
            BitmapData data = bmp.LockBits(
                new Rectangle(0, 0, bmp.Width, bmp.Height),
                ImageLockMode.ReadWrite,
                PixelFormat.Format24bppRgb
            );
            
            //int stride = data.Stride;
            C = new Color[Width, Height];
            
            unsafe {
                byte* ptr = (byte*) data.Scan0;
                for (int x = 0 ; x < Width ; x++) {
                    for (int y = 0 ; y < Height ; y++) {
                        C[x,y] = new Color(
                            b255: ptr[(x * 3) + y * data.Stride],
                            g255: ptr[(x * 3) + y * data.Stride + 1],
                            r255: ptr[(x * 3) + y * data.Stride + 2]
                        );
                    }
                }
            }
            
            bmp.UnlockBits(data);
        }

        private Color Interpolate(float Lu, float Hv) {
            int x = (int) (TileU * Lu);  // plus grand entier <=
            int y = (int) (TileV * Hv);
            
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
