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
        static private ZBuffer ZBuffer;

        static public Bitmap Init(int w, int h) {
            Width = w;
            Height = h;
            
            Bmp = new Bitmap(Width, Height);
            ZBuffer = new ZBuffer(Width, Height);
            
            return Bmp;
        }
 
        static void DrawFastPixel(int x, int y, Color c) {
            unsafe {
                byte* ptr = (byte*) data.Scan0;
                ptr[(x * 3) + y * Stride    ] = c.B255();
                ptr[(x * 3) + y * Stride + 1] = c.G255();
                ptr[(x * 3) + y * Stride + 2] = c.R255();
            }
        }

        static void DrawSlowPixel(int x, int y, Color c) {
            Bmp.SetPixel(x, y, c.To255());
            
            Program.Form.PictureBoxInvalidate();
            pxCounter++;
            
            // Force redraw every REFRESH px
            if (pxCounter > REFRESH) {
               Program.Form.PictureBoxRefresh();
               pxCounter = 0;
            }
         }

        static public void Refresh(Color c) {
            ZBuffer.clear();
            
            if (Program.Form.Checked()) {
                Mode = DisplayMode.SLOW_MODE;
                Graphics g = Graphics.FromImage(Bmp);
                g.Clear(c.To255());
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

        /// Draw a pixel at the position specified by the p vector.
        /// p's coordinates are in the orthonormal basis specified in the
        /// assignement: (X,Z,Y) with Y being the viewing direction.
        public static void DrawPixel(V3 p, Color c) {
            DrawPixel((int) p.X, (int) p.Z, p.Y, c);
        }

        /// Draw a pixel on the screen at the following position:
        /// xScreen = x
        /// yScreen = screenHeight - y
        /// A pixel is drawn only if it is inside the screen (canvas), and
        /// if it is not hidden by another already drawn pixel (check the
        /// ZBuffer).
        public static void DrawPixel(int x, int y, float z, Color c) {
            int xScreen = x;
            int yScreen = Height - y;
            
            bool inScreen = (xScreen >= 0) && (xScreen < Width) &&
                            (yScreen >= 0) && (yScreen < Height);
            
            bool canDraw = ZBuffer.Set(xScreen, yScreen, z);
            
            if (inScreen && canDraw) {
                if (Mode == DisplayMode.SLOW_MODE) {
                    DrawSlowPixel(xScreen, yScreen, c);
                }
                else {
                    DrawFastPixel(xScreen, yScreen, c);
                }
            }
        }

        static public void Show() {
            if (Mode == DisplayMode.FULL_SPEED) {
                Bmp.UnlockBits(data);
            }
            
            Program.Form.PictureBoxInvalidate();
        }

        static public int GetWidth() { return Width; }
        static public int GetHeight() { return Height; }
    }
}
