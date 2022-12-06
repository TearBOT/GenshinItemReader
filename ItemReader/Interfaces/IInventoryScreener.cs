using ItemReader.Utility;

namespace ItemReader.Interfaces
{
    internal interface IInventoryScreener
    {
        bool isInventoryOpen();
        void setGameWindow(IntPtr gameWindow);
        void setWindowBounds(Rect windowBounds);
        void setWindowInfo(IntPtr gameWindow, Rect windowBounds, AllCoordinates allCoordinates);
        IEnumerable<Bitmap> GetFullInventory();
        IEnumerable<Bitmap> GetEachItems(List<Bitmap> FullScreens);
    }
}
