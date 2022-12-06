using ItemReader.Interfaces;
using ItemReader.Utility;

namespace ItemReader.ScreenShotter
{

    internal class InventoryScreener : IInventoryScreener
    {

        /* CLASS VARIABLE(S) */

        private Point _MaterialTabClick;
        private Point _TopLeftItem;
        private Point _BottomLeftItem;
        private Point _SpecialitiesTabClick;
        private AllCoordinates _AllCoordinates;
        private IntPtr _GameWindow { get; set; }
        private Rect _WindowBounds { get; set; }


        /* PUBLIC METHOD(S) */
        public InventoryScreener() { }

        public InventoryScreener(IntPtr gameWindow, Rect windowBounds, AllCoordinates allCoordinates)
        {
            setWindowInfo(gameWindow, windowBounds, allCoordinates);
        }

        public void setGameWindow(IntPtr gameWindow)
        {
            _GameWindow = gameWindow;
        }

        public void setWindowBounds(Rect windowBounds)
        {
            _WindowBounds = windowBounds;
            _TopLeftItem = new Point(_WindowBounds.topLeft.X + 180, _WindowBounds.topLeft.Y + 180);
            _BottomLeftItem = new Point(_WindowBounds.topLeft.X + 180, _WindowBounds.topLeft.Y + 900);
            _MaterialTabClick = new Point(_WindowBounds.topLeft.X + 770, _WindowBounds.topLeft.Y + 65);
            _SpecialitiesTabClick = new Point(_WindowBounds.topLeft.X + 960, _WindowBounds.topLeft.Y + 65);
        }

        public void setWindowInfo(IntPtr gameWindow, Rect windowBounds, AllCoordinates allCoordinates)
        {
            setGameWindow(gameWindow);
            setWindowBounds(windowBounds);
            _AllCoordinates = allCoordinates;
        }

        public bool isInventoryOpen()
        {
            Bitmap inventoryScreen = ScreenShotHelper.TakePartialScreenShot(_GameWindow, _WindowBounds, new Rectangle(_AllCoordinates.BagIcon.topLeft, _AllCoordinates.BagIcon.rectSize));
            Bitmap bagIcon = new Bitmap(Resources.Resources.BagIcon);

            bool result = ImageComparator.ComapreImages(inventoryScreen, bagIcon);
            if (inventoryScreen != null) {
                inventoryScreen.Dispose();
            }
            bagIcon.Dispose();

            return result;
        }

        public IEnumerable<Bitmap> GetFullInventory()
        {
            List<Bitmap> FullScreens = new List<Bitmap>();
            List<Bitmap> Items = new List<Bitmap>();

            FullScreens.AddRange(GetItemsFromTab(_MaterialTabClick, 8));
            FullScreens.AddRange(GetItemsFromTab(_SpecialitiesTabClick, 4));

            return Items;
        }

        public IEnumerable<Bitmap> GetEachItems(List<Bitmap> FullScreens)
        {
            List<Bitmap> Items = new List<Bitmap>();
            int PixelShift = 0;
            foreach (Bitmap bitmap in FullScreens) {
                foreach (var ItemLine in _AllCoordinates.Items) {
                    foreach (var Item in ItemLine) {
                        Items.Add(
                            ScreenShotHelper.TakePartialScreenShot(
                                _GameWindow,
                                _WindowBounds,
                                new Rectangle(Item.topLeft, Item.rectSize)
                            )
                        );
                    }
                }
                PixelShift++;
                if ( PixelShift % 4 == 0 ) {
                    PixelShift++;
                }
            }
            return Items;
        }

        /* PRIVATE METHOD(S) */

        private IEnumerable<Bitmap> GetItemsFromTab(Point tab, int loops)
        {
            List<Bitmap> FullScreens = new List<Bitmap>();

            MouseEmulator.MouseLeftClick(_GameWindow, tab);
            Thread.Sleep(400);
            MouseEmulator.MouseScrollUp(_GameWindow, _TopLeftItem, 400);
            MouseEmulator.MouseLeftClick(_GameWindow, _BottomLeftItem);

            // DEBUG
            for (var i = 0; i < loops; i++) {
                Thread.Sleep(50);
                MouseEmulator.MouseScrollDown(_GameWindow, _BottomLeftItem, 39);
                MouseEmulator.MouseLeftClick(_GameWindow, _BottomLeftItem);
                ScreenShotHelper.TakeFullScreenShot(_GameWindow, _WindowBounds);
            }
            MouseEmulator.MouseLeftClick(_GameWindow, _TopLeftItem);
            ScreenShotHelper.TakeFullScreenShot(_GameWindow, _WindowBounds);

            return FullScreens;
        }

    }
}
