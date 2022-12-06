using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace ItemReader.Utility
{

    internal /*static*/ class ScreenShotHelper {

        /* CLASS VARIABLE(S) */

        private static int _screenShotId = 0;

        /* PUBLIC METHOD(S) */

        public static Bitmap TakeFullScreenShot(IntPtr gameWindow, Rect windowBounds)
        {
            if (gameWindow == IntPtr.Zero || windowBounds.topLeft.IsEmpty)
            {
                return null;
            }

            SetToForground(gameWindow);
            Bitmap screenShot = new Bitmap(
                windowBounds.rectSize.Width,
                windowBounds.rectSize.Height
            );

            using (Graphics captureGraphics = Graphics.FromImage(screenShot))
            {
                captureGraphics.CopyFromScreen(windowBounds.topLeft, Point.Empty, screenShot.Size);
                captureGraphics.Dispose();
            }

            // DEBUG
            screenShot.Save($@"{Resources.Resources.DEBUG_FOLDER}\test_{_screenShotId}.png", ImageFormat.Png);
            _screenShotId++;

            return screenShot;
        }

        public static  Bitmap TakePartialScreenShot(IntPtr gameWindow, Rect windowBounds, Rectangle remain)
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

        private static void SetToForground(IntPtr gameWindow)
        {
            SetForegroundWindow(gameWindow);
            Thread.Sleep(20);
        }

        [DllImport("user32.dll")]
        private static extern IntPtr SetForegroundWindow(IntPtr ptr);

        [DllImport("user32.dll")]
        private static extern bool PrintWindow(IntPtr hWnd, IntPtr hdcBlt, int nFlags);

    }
}
