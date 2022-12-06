using ItemReader.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ItemReader.Interfaces
{
    internal interface IWindowCatcher
    {
        string testMessage();
        bool catchGameWindow();
        IntPtr getGameWindow();
        Rect getWindowBouds();
    }
}
