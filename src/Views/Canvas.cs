using System.Drawing;
using System.Drawing.Imaging;

namespace ImageSynthesis.Views {

    enum DisplayMode { SLOW, FAST };

    class Canvas : System.Windows.Forms.PictureBox {
        
        private const int REFRESH = 1000;
        
        private Bitmap Bmp;
        private BitmapData data;
        private int PxCounter;
        
        public DisplayMode Mode { get; set; }
        
        public Canvas(int width, int height) {
            Size = new Size(width, height);
            Bmp = new Bitmap(width, height);
            Image = Bmp;
            
            PxCounter = 0;
            
            Mode = DisplayMode.SLOW;
        }

        private void DrawFastPixel(int x, int y, Color c) {
            unsafe {
                byte* ptr = (byte*) data.Scan0;
                ptr[(x * 3) + y * data.Stride    ] = c.B255();
                ptr[(x * 3) + y * data.Stride + 1] = c.G255();
                ptr[(x * 3) + y * data.Stride + 2] = c.R255();
            }
        }
        
        private void DrawSlowPixel(int x, int y, Color c) {
            Bmp.SetPixel(x, y, c.To255());
            PxCounter++;
            
            // Force redraw every REFRESH px
            if (PxCounter > REFRESH) {
               Refresh();
               PxCounter = 0;
            }
         }
         
        public void BeginDrawing() {
            Graphics g = Graphics.FromImage(Bmp);
            g.Clear(Color.Black.To255());
            
            if (Mode == DisplayMode.FAST) {
                data = Bmp.LockBits(
                    new Rectangle(0, 0, Bmp.Width, Bmp.Height),
                    ImageLockMode.ReadWrite,
                    PixelFormat.Format24bppRgb
                );
            }
        }
        
        public void DrawPixel(V3 pScreen, Color c) {
            DrawPixel((int) pScreen.X, (int) pScreen.Y, c);
        }
        
        /// Draws a pixel on the canvas. A pixel is only drawn if it is inside
        /// the canvas.
        public void DrawPixel(int x, int y, Color c) {
            bool inScreen = (x >= 0) && (x < Width) &&
                            (y >= 0) && (y < Height);
            
            if (inScreen) {
                if (Mode == DisplayMode.SLOW) {
                    DrawSlowPixel(x, y, c);
                }
                else {
                    DrawFastPixel(x, y, c);
                }
            }
        }
        
        public void EndDrawing() {
            if (Mode == DisplayMode.FAST) {
                Bmp.UnlockBits(data);
            }
            
            Refresh();
        }
        
        public void SwitchDisplayMode(bool slow) {
            Mode = slow ? DisplayMode.SLOW : DisplayMode.FAST;
        }
    }
}
