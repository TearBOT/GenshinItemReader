using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using ItemReader.Models;
using ItemReader.Utility;

namespace ItemReader.InventoryScreener
{

    internal static class ScreenShotter {

        /* PUBLIC METHOD(S) */

        public static Bitmap TakeScreenShot(IntPtr GameWindow, Rect GameWindowBounds)
        {
            if (GameWindow == IntPtr.Zero || GameWindowBounds.topLeft.IsEmpty) {
                return null;
            }

            SetForegroundWindow(GameWindow);

            // Sleep to ensure game is on the Forground before taking the screenshot
            Thread.Sleep(100);

            Bitmap screenShot = new Bitmap(
                GameWindowBounds.rectSize.Width,
                GameWindowBounds.rectSize.Height
            );

            using (Graphics captureGraphics = Graphics.FromImage(screenShot))
            {
                captureGraphics.CopyFromScreen(GameWindowBounds.topLeft, Point.Empty, screenShot.Size);
                captureGraphics.Dispose();
            }

            // DEBUG
            var TimeStamp = $"{DateTime.Now.Hour}-{DateTime.Now.Minute}-{DateTime.Now.Second}-{DateTime.Now.Millisecond}";
            screenShot.Save($@"C:\Users\Mini-Soo\Projects\test\test_{TimeStamp}.png", ImageFormat.Png);

            return screenShot;
        }

        public static Bitmap TakeCroppedScreenShot(IntPtr gameWindow, Rect windowBounds, Rectangle PartToCrop)
        {
            if (gameWindow == IntPtr.Zero || windowBounds.topLeft.IsEmpty || PartToCrop.IsEmpty)
                return null;

            Bitmap fullScreen = TakeScreenShot(gameWindow, windowBounds);
            Bitmap partialScreen = fullScreen.Clone(PartToCrop, PixelFormat.Format32bppArgb);

            fullScreen.Dispose();

            // DEBUG
            var TimeStamp = $"{DateTime.Now.Hour}-{DateTime.Now.Minute}-{DateTime.Now.Second}-{DateTime.Now.Millisecond}";
            partialScreen.Save($@"C:\Users\Mini-Soo\Projects\test\test_{TimeStamp}.png", ImageFormat.Png);

            return partialScreen;
        }


        /* USER32.DLL(S) */

        [DllImport("user32.dll")]
        private static extern IntPtr SetForegroundWindow(IntPtr ptr);

        [DllImport("user32.dll")]
        private static extern bool PrintWindow(IntPtr hWnd, IntPtr hdcBlt, int nFlags);

    }
}
