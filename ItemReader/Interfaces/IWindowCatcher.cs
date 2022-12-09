using ItemReader.Utils;

namespace ItemReader.Interfaces
{
    internal interface IWindowCatcher {

        /* CLASS VARIABLE(S) */

        public IntPtr GameWindow { get; set; }
        public Rect GameWindowBounds { get; set; }

        /* PUBLIC METHOD(S) */

        public bool IsGameWindowOpen(string GameWindowName);

    }
}
