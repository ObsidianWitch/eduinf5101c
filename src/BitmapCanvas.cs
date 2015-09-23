using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace ImageSynthesis {

    enum DisplayMode { SLOW_MODE, FULL_SPEED};

    class BitmapCanvas {

        const int REFRESH = 1000;

        static int pxCounter = 0;

        static private Bitmap Bmp;
        static private DisplayMode Mode;
        static private int Width;
        static private int Height;
        static private int Stride;
        static private BitmapData data;

        static public Bitmap Init(int w, int h) {
            Width = w;
            Height = h;
            
            Bmp = new Bitmap(Width, Height);
            
            return Bmp;
        }
 
        static void DrawFastPixel(int x, int y, Couleur c) {
            unsafe {
                byte RR, VV, BB;
                c.check();
                c.To255(out RR, out  VV, out  BB);
                
                byte* ptr = (byte*) data.Scan0;
                ptr[(x * 3) + y * Stride    ] = BB;
                ptr[(x * 3) + y * Stride + 1] = VV;
                ptr[(x * 3) + y * Stride + 2] = RR;
            }
        }

        static void DrawSlowPixel(int x, int y, Couleur c) {
            Color cc = c.Convertion();
            Bmp.SetPixel(x, y, cc);
            
            Program.MyForm.PictureBoxInvalidate();
            pxCounter++;
            
            // Force redraw every REFRESH px
            if (pxCounter > REFRESH) {
               Program.MyForm.PictureBoxRefresh();
               pxCounter = 0;
            }
         }

        static public void RefreshScreen(Couleur c) {
            if (Program.MyForm.Checked()) {
                Mode = DisplayMode.SLOW_MODE;
                Graphics g = Graphics.FromImage(Bmp);
                Color cc = c.Convertion();
                g.Clear(cc);
            }
            else {
                Mode = DisplayMode.FULL_SPEED;
                data = Bmp.LockBits(
                    new Rectangle(0, 0, Bmp.Width, Bmp.Height),
                    ImageLockMode.ReadWrite,
                    PixelFormat.Format24bppRgb
                );
                
                Stride = data.Stride;
                
                for (int x = 0; x < Width; x++) {
                    for (int y = 0; y < Height; y++) {
                        DrawFastPixel(x, y, c);
                    }
                }
            }
        }

        public static void DrawPixel(int x, int y, Couleur c) {
            int x_Screen = x;
            int y_Screen = Height - y;
            
            bool inScreen = (x_Screen >= 0) && (x_Screen < Width) &&
                            (y_Screen >= 0) && (y_Screen < Height);
            
            if (inScreen) {
                if (Mode == DisplayMode.SLOW_MODE) {
                    DrawSlowPixel(x_Screen, y_Screen, c);
                }
                else {
                    DrawFastPixel(x_Screen, y_Screen, c);
                }
            }
        }

        static public void Show() {
            if (Mode == DisplayMode.FULL_SPEED) {
                Bmp.UnlockBits(data);
            }
            
            Program.MyForm.PictureBoxInvalidate();
        }

        static public int GetWidth() { return Width; }
        static public int GetHeight() { return Height; }
    }
}
