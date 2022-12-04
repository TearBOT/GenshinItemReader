using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace ItemReader.Utility
{

    internal /*static*/ class ScreenShotHelper {

        /* CLASS VARIABLE(S) */

        private int _screenShotId = 0;

        /* PUBLIC METHOD(S) */

        public Bitmap TakeFullScreenShot(IntPtr gameWindow, Rect windowBounds)
        {
            if (gameWindow == IntPtr.Zero || windowBounds.topLeft.IsEmpty) {
                return null;
            }

            SetForegroundWindow(gameWindow);
            try {
                Bitmap screenShot = new Bitmap(
                    windowBounds.rectSize.Width,
                    windowBounds.rectSize.Height,
                    PixelFormat.Format32bppArgb
                );

                Graphics captureGraphics = Graphics.FromImage(screenShot);
                IntPtr hdcBMP = captureGraphics.GetHdc();
                PrintWindow(gameWindow, hdcBMP, 0);
                captureGraphics.ReleaseHdc(hdcBMP);
                captureGraphics.Dispose();

                // DEBUG
                screenShot.Save($@"{Resources.Resources.DEBUG_FOLDER}\test_{_screenShotId}.png", ImageFormat.Png);
                _screenShotId++;

                return screenShot;
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
                throw new Exception();
            }
        }

        public Bitmap TakePartialScreenShot(IntPtr gameWindow, Rect windowBounds, Rectangle remain)
        {
            if (gameWindow == IntPtr.Zero || windowBounds.topLeft.IsEmpty || remain.IsEmpty)
                return null;

            Bitmap fullScreen = TakeFullScreenShot(gameWindow, windowBounds);
            Bitmap partialScreen = fullScreen.Clone(remain, PixelFormat.Format32bppArgb);

            fullScreen.Dispose();

            // DEBUG
            partialScreen.Save($@"{Resources.Resources.DEBUG_FOLDER}\test_{_screenShotId}.png", ImageFormat.Png);
            _screenShotId++;

            return partialScreen;
        }

        /* PRIVATE METHOD(S) */

        [DllImport("user32.dll")]
        private static extern IntPtr SetForegroundWindow(IntPtr ptr);

        [DllImport("user32.dll")]
        private static extern bool PrintWindow(IntPtr hWnd, IntPtr hdcBlt, int nFlags);

    }
}
