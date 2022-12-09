using ItemReader.Models;
using System.Runtime.InteropServices;

namespace ItemReader.InventoryScreener
{
    internal static class MouseEmulator {

        /* CLASS VARIABLE(S) */

        private const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const int MOUSEEVENTF_LEFTUP = 0x0004;
        private const int MOUSEEVENTF_WHEEL = 0x0800;

        /* PUBLIC METHOD(S) */

        public static void MouseLeftClick(IntPtr gameWindow, Point Pos)
        {
            SetForegroundWindow(gameWindow);

            SetCursorPos(Pos.X, Pos.Y);

            mouse_event(MOUSEEVENTF_LEFTDOWN, Pos.X, Pos.Y, 0, 0);
            Thread.Sleep(80);
            mouse_event(MOUSEEVENTF_LEFTUP, Pos.X, Pos.Y, 0, 0);
        }

        public static void MouseScrollUp(IntPtr gameWindow, Point Pos, int loop = 1)
        {
            MouseLeftClick(gameWindow, Pos);

            for (int i = 0; i < loop; i++) {
                Thread.Sleep(10);
                mouse_event(MOUSEEVENTF_WHEEL, 0, 0, (int)ScrollDirection.UP, 0);
            }
        }

        public static void MouseScrollDown(IntPtr gameWindow, Point Pos, int loop = 1)
        {
            MouseLeftClick(gameWindow, Pos);

            for (int i = 0; i < loop; i++) {
                Thread.Sleep(10);
                mouse_event(MOUSEEVENTF_WHEEL, 0, 0, (int)ScrollDirection.DOWN, 0);
            }
        }

        /* USER32.DLL(S) */

        [DllImport("user32.dll")]
        private static extern IntPtr SetForegroundWindow(IntPtr ptr);

        [DllImport("user32.dll")]
        static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

    }
}
