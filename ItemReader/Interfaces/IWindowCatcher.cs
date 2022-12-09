using ItemReader.Utility;

namespace ItemReader.Interfaces
{
    internal interface IWindowCatcher {

        public IntPtr GameWindow { get; set; }

        public Rect GameWindowBounds { get; set; }

        public bool IsGameWindowOpen(string GameWindowName);

    }
}
