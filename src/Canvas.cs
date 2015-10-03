using System.Drawing;
using System.Drawing.Imaging;

namespace ImageSynthesis {

    enum DisplayMode { SLOW, FAST };

    class Canvas : System.Windows.Forms.PictureBox {
        
        private const int REFRESH = 1000;
        
        private ZBuffer ZBuffer;
        private Bitmap Bmp;
        private BitmapData data;
        private int PxCounter;
        
        public DisplayMode Mode { get; set; }
        
        public Canvas(int width, int height) {
            Size = new Size(width, height);
            Bmp = new Bitmap(width, height);
            Image = Bmp;
            ZBuffer = new ZBuffer(Width, Height);
            
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
            ZBuffer.clear();
            
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
        
        /// Draws a pixel at the position specified by the p vector.
        /// p's coordinates are in the orthonormal basis specified in the
        /// assignement with Y being the viewing direction.
        public void DrawPixel(V3 p, Color c) {
            DrawPixel((int) p.X, (int) p.Z, p.Y, c);
        }
        
        /// Draws a pixel on the screen at the following position:
        /// xScreen = x
        /// yScreen = screenHeight - y
        /// A pixel is drawn only if it is inside the screen (canvas), and
        /// if it is not hidden by another already drawn pixel (check the
        /// ZBuffer).
        public void DrawPixel(int x, int y, float z, Color c) {
            int xScreen = x;
            int yScreen = Height - y;
            
            bool inScreen = (xScreen >= 0) && (xScreen < Width) &&
                            (yScreen >= 0) && (yScreen < Height);
            
            if (inScreen) {
                bool canDraw = ZBuffer.Set(xScreen, yScreen, z);
                
                if (canDraw) {
                    if (Mode == DisplayMode.SLOW) {
                        DrawSlowPixel(xScreen, yScreen, c);
                    }
                    else {
                        DrawFastPixel(xScreen, yScreen, c);
                    }
                }
            }
        }
        
        public void EndDrawing() {
            if (Mode == DisplayMode.FAST) {
                Bmp.UnlockBits(data);
            }
            
            Refresh();
        }
    }
}
