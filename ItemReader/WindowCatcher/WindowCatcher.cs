using ItemReader.Interfaces;
using ItemReader.Utils;
using System.Runtime.InteropServices;

namespace ItemReader.WindowCatcher
{
    internal class WindowCatcher : IWindowCatcher {

        /* CLASS VARIABLE(S) */

        public IntPtr GameWindow { get; set; }
        public Rect GameWindowBounds { get; set; }

        /* PUBLIC METHOD(S) */

        public bool IsGameWindowOpen(string GameWindowName)
        {
            GameWindow = FindWindow(IntPtr.Zero, GameWindowName);
            if (GameWindow == IntPtr.Zero) {
                return false;
            }

            var TmpRect = new Rectangle(0, 0, 0, -1);
            while (TmpRect.Height <= 0) {
                GetWindowRect(GameWindow, out TmpRect);
                Thread.Sleep(100);
            }

            GameWindowBounds = new Rect(
                TmpRect.X,
                TmpRect.Y,
                TmpRect.Width,
                TmpRect.Height
                );

            if (GameWindow != IntPtr.Zero
                && GameWindowBounds.TopLeft != Point.Empty) {
                return true;
            }

            return false;
        }

        /* USER32.DLL */

        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(IntPtr ptr, string lpWindowName);
        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr ptr, out Rectangle lpRect);
    }
}
