using System.Runtime.InteropServices;

namespace ItemReader.Utility
{

    internal class MouseEmulator {

        // https://stackoverflow.com/questions/37262822/c-sharp-simulate-mouse-wheel-down

        /* CLASS VARIABLE(S) */

        private const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const int MOUSEEVENTF_LEFTUP = 0x0004;
        private const int MOUSEEVENTF_WHEEL = 0x0800;

        /* PUBLIC METHOD(S) */

        public MouseEmulator()
        {}

        public static void MouseLeftClick(Point coord)
        {
            SetCursorPos(coord.X, coord.Y);
            mouse_event(MOUSEEVENTF_LEFTDOWN, coord.X, coord.Y, 0, 0);
            Thread.Sleep(40);
            mouse_event(MOUSEEVENTF_LEFTUP, coord.X, coord.Y, 0, 0);
        }

        public static void MouseScrollDown(Point coord, int loop)
        {
            for (int i = 0; i < loop; i++) {
                SetCursorPos(coord.X, coord.Y);
                mouse_event(MOUSEEVENTF_WHEEL, 0, 0, -300, 0);
                Thread.Sleep(10);
            }
        }

        /* PRIVATE METHOD(S) */

        [DllImport("user32.dll")]
        private static extern IntPtr SetForegroundWindow(IntPtr ptr);

        [DllImport("user32.dll")]
        static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

    }
}
