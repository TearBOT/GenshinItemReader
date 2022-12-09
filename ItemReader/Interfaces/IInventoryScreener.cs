using ItemReader.InventoryScreener;
using ItemReader.Models;
using ItemReader.Utils;

namespace ItemReader.Interfaces
{
    internal interface IInventoryScreener {

        public bool IsInventoryOpen();

        public bool ProcessGameWindowInfo(IntPtr GameWindow, Rect GameWindowBounds, GenshinItemCoordinates GenshinItemsPos);

        public GenshinInventoryScreenshots TakeInventoryScreenShots();

        public IEnumerable<GenshinItem> SplitInventoryItems(GenshinInventoryScreenshots InventoryScreenShots);

    }
}
