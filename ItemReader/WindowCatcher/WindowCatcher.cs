using System.Runtime.InteropServices;

using ItemReader.Interfaces;
using ItemReader.Utility;

namespace ItemReader.WindowCatcher
{
    internal class WindowCatcher : IWindowCatcher
    {
        private IntPtr _gameWindow;
        private Rect _windowBounds;

        public WindowCatcher()
        { }

        public string testMessage()
        {
            return ("Hello World");
        }

        public IntPtr getGameWindow()
        {
            return _gameWindow;
        }
        public Rect getWindowBouds()
        {
            return _windowBounds;
        }

        private void setGameWindow(IntPtr gameWindow)
        {
            _gameWindow = gameWindow;
        }

        public bool catchGameWindow()
        {
            do
            {
                _gameWindow = FindWindow(IntPtr.Zero, Resources.Resources.GAME_WINDOW);
                Thread.Sleep(1000);
            } while (_gameWindow.ToInt32() == 0);
            Rectangle tmpBound;
            do
            {
                GetWindowRect(_gameWindow, out tmpBound);
                Thread.Sleep(1000);
            } while (tmpBound.X < 0);
            _windowBounds = new Rect(tmpBound.X, tmpBound.Y, tmpBound.Width, tmpBound.Height);
            if (_gameWindow != IntPtr.Zero && _windowBounds.topLeft != Point.Empty) {
                return true;
            }
            return false;
        }

        public void Dispose() { }
        public void Show() { }
        public void Hide() { }

        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(IntPtr ptr, string lpWindowName);
        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr ptr, out Rectangle lpRect);
    }
}
