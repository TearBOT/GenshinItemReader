using ItemReader.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemReader.Interfaces
{
    internal interface IInventoryScreener
    {
        bool isInventoryOpen();
        void setGameWindow(IntPtr gameWindow);
        void setWindowBounds(Rect windowBounds);
        void setWindowInfo(IntPtr gameWindow, Rect windowBounds);
        List<Bitmap> getItems();
    }
}
