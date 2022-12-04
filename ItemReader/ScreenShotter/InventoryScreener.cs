using ItemReader.Interfaces;
using ItemReader.Utility;

namespace ItemReader.ScreenShotter
{

    internal class InventoryScreener : IInventoryScreener
    {

        /* CLASS VARIABLE(S) */

        private Point _materialTabClick;
        private Point _specialitiesTabClick;
        private Rectangle _bagIconCoords;
        public IntPtr _gameWindow { get; set; }
        public Rect _windowBounds { get; set; }
        private ScreenShotHelper _screenShotter { get; set; }
        private MouseEmulator _mouseEmulator { get; set; }
        private ImageComparator _imageComparator { get; set; }

        /* PUBLIC METHOD(S) */

        public InventoryScreener()
        {
            _screenShotter = new ScreenShotHelper();
            _mouseEmulator = new MouseEmulator();
            _imageComparator = new ImageComparator();
        }

        public InventoryScreener(IntPtr gameWindow, Rect windowBounds)
        {
            _screenShotter = new ScreenShotHelper();
            _mouseEmulator = new MouseEmulator();
            _imageComparator = new ImageComparator();
            setGameWindow(gameWindow);
            setWindowBounds(windowBounds);
        }

        public void setGameWindow(IntPtr gameWindow)
        {
            _gameWindow = gameWindow;
        }
        public void setWindowBounds(Rect windowBounds)
        {
            _windowBounds = windowBounds;
            _materialTabClick = new Point(550 + _windowBounds.topLeft.X, 90 + _windowBounds.topLeft.Y);
            _specialitiesTabClick = new Point(800 + _windowBounds.topLeft.X, 90 + _windowBounds.topLeft.Y);
            _bagIconCoords = new Rectangle(13, 152, 47, 51);
        }
        public void setWindowInfo(IntPtr gameWindow, Rect windowBounds)
        {
            setGameWindow(gameWindow);
            setWindowBounds(windowBounds);
        }

        public bool isInventoryOpen()
        {
            Bitmap inventoryScreen = _screenShotter.TakePartialScreenShot(_gameWindow, _windowBounds, _bagIconCoords);
            Bitmap bagIcon = new Bitmap(Resources.BagIcon);

            bool result = _imageComparator.SameImage(inventoryScreen, bagIcon);

            inventoryScreen.Dispose();
            bagIcon.Dispose();

            return result;
        }

        public List<Bitmap> getItems()
        {
            List<Bitmap> items = new List<Bitmap>();

            items.Add(_screenShotter.TakePartialScreenShot(_gameWindow, _windowBounds, new Rectangle(8, 30, 900, 120)));
            MouseEmulator.MouseLeftClick(_materialTabClick);
            items.Add(_screenShotter.TakePartialScreenShot(_gameWindow, _windowBounds, _bagIconCoords));
            items.Add(_screenShotter.TakePartialScreenShot(_gameWindow, _windowBounds, new Rectangle(8, 30, 900, 120)));

            Thread.Sleep(1000);

            MouseEmulator.MouseScrollDown(_specialitiesTabClick, 1);
            items.Add(_screenShotter.TakePartialScreenShot(_gameWindow, _windowBounds, new Rectangle(8, 30, 900, 120)));

            return items;
        }

        /* PRIVATE METHOD(S) */

    }
}
