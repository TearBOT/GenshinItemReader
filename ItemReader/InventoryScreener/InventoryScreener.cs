using ItemReader.Interfaces;
using ItemReader.Models;
using ItemReader.Utility;
using System.Collections.Generic;
using System.Drawing.Imaging;

namespace ItemReader.InventoryScreener
{

    internal class InventoryScreener : IInventoryScreener {

        /* CLASS VARIABLE(S) */

        private Point _MaterialTabClick;
        private Point _TopLeftItem;
        private Point _BottomLeftItem;
        private Point _SpecialitiesTabClick;

        public GenshinItemCoordinates _GenshinItemCoordinates { get; set; }
        public IntPtr _GameWindow { get; set; }
        public Rect _GameWindowBounds { get; set; }

        /* PUBLIC METHOD(S) */

        public InventoryScreener() {}

        public InventoryScreener(IntPtr gameWindow, Rect windowBounds, GenshinItemCoordinates GenshinItemsPos)
        {
            ProcessGameWindowInfo(gameWindow, windowBounds, GenshinItemsPos);
        }

        public bool ProcessGameWindowInfo(IntPtr GameWindow, Rect GameWindowBounds, GenshinItemCoordinates GenshinItemsPos)
        {
            if (GenshinItemsPos.FirstLineItemsPos.Count <= 0) {
                return false;
            }

            _GameWindow = GameWindow;
            _GameWindowBounds = GameWindowBounds;
            _GenshinItemCoordinates = GenshinItemsPos;

            _TopLeftItem = new Point(_GameWindowBounds.topLeft.X + 180, _GameWindowBounds.topLeft.Y + 180);
            _BottomLeftItem = new Point(_GameWindowBounds.topLeft.X + 180, _GameWindowBounds.topLeft.Y + 900);
            _MaterialTabClick = new Point(_GameWindowBounds.topLeft.X + 770, _GameWindowBounds.topLeft.Y + 65);
            _SpecialitiesTabClick = new Point(_GameWindowBounds.topLeft.X + 960, _GameWindowBounds.topLeft.Y + 65);

            return true;
        }

        public bool IsInventoryOpen()
        {
            Bitmap inventoryScreen = ScreenShotter.TakeCroppedScreenShot(
                _GameWindow,
                _GameWindowBounds,
                new Rectangle(
                    _GenshinItemCoordinates.BagIconPos.topLeft,
                    _GenshinItemCoordinates.BagIconPos.rectSize
                    )
                );

            Bitmap BagIcon = new Bitmap(@"..\..\..\Resources\BagIcon.png");

            bool Result = ImageComparator.ComapreImages(inventoryScreen, BagIcon);

            if (inventoryScreen != null) {
                inventoryScreen.Dispose();
            }
            BagIcon.Dispose();

            return Result;
        }

        public GenshinInventoryScreenshots TakeInventoryScreenShots()
        {
            var GenshinScreenShots = new GenshinInventoryScreenshots();
            GenshinScreenShots.MaterialTab = new List<Bitmap>();
            GenshinScreenShots.SpecialitiesTab = new List<Bitmap>();

            var MaterialsTab = TakeScreenShotsInTab(_MaterialTabClick, 8);
            if (MaterialsTab is null) {
                return null;
            }

            GenshinScreenShots.MaterialTab.AddRange(MaterialsTab);

            var SpecialitiesTab = TakeScreenShotsInTab(_SpecialitiesTabClick, 4);
            if (SpecialitiesTab is null) {
                return GenshinScreenShots;
            }

            GenshinScreenShots.SpecialitiesTab.AddRange(SpecialitiesTab);

            return GenshinScreenShots;
        }

        public IEnumerable<GenshinItem> SplitInventoryItems(GenshinInventoryScreenshots InventoryScreenshots)
        {
            List<GenshinItem> ItemList = new List<GenshinItem>();

            var ExtractedMaterialItems = ExtractItems(InventoryScreenshots.MaterialTab);

            if (ExtractedMaterialItems is null) {
                return null;
            }
            ItemList.AddRange(ExtractedMaterialItems);

            var ExtractedSpecialityItems = ExtractItems(InventoryScreenshots.SpecialitiesTab);

            if (ExtractedSpecialityItems is null) {
                return null;
            }
            ItemList.AddRange(ExtractedSpecialityItems);

            return ItemList;
        }

        private List<GenshinItem> ExtractItems(List<Bitmap> InventoryScreenshots)
        {
            List<GenshinItem> ItemList = new List<GenshinItem>();
            int PixelShift = 0;

            foreach (var InventoryScreenshot in InventoryScreenshots) {
                for (int line = 0; line < 4; line++) {
                    foreach (var ItemPos in _GenshinItemCoordinates.FirstLineItemsPos) {
                        ItemList.Add(CreateGenshinItem(InventoryScreenshot, ItemPos, line, PixelShift));
                    }
                }

                PixelShift--;

                if (PixelShift % 4 == 0) {
                    PixelShift--;
                }
            }

            for (int line = 0; line < 4; line++) {
                foreach (var ItemPos in _GenshinItemCoordinates.LastLineItemsPos) {
                    ItemList.Add(CreateGenshinItem(InventoryScreenshots.Last(), ItemPos, line));
                }
            }

            return ItemList;
        }

        /* PRIVATE METHOD(S) */

        private IEnumerable<Bitmap> TakeScreenShotsInTab(Point TabToSelect, int loops)
        {
            List<Bitmap> FullScreens = new List<Bitmap>();

            MouseEmulator.MouseLeftClick(_GameWindow, TabToSelect);
            Thread.Sleep(1000);
            MouseEmulator.MouseScrollUp(_GameWindow, _TopLeftItem, 400);
            Thread.Sleep(500);
            MouseEmulator.MouseLeftClick(_GameWindow, _BottomLeftItem);

            for (var i = 0; i < loops; i++) {
                var ScreenShot = ScreenShotter.TakeScreenShot(_GameWindow, _GameWindowBounds);

                if (ScreenShot is null) {
                    return null;
                }
                FullScreens.Add(ScreenShot);

                MouseEmulator.MouseScrollDown(_GameWindow, _BottomLeftItem, 39);
                Thread.Sleep(200);
                MouseEmulator.MouseLeftClick(_GameWindow, _BottomLeftItem);
            }

            MouseEmulator.MouseLeftClick(_GameWindow, _TopLeftItem);

            var LastScreenShot = ScreenShotter.TakeScreenShot(_GameWindow, _GameWindowBounds);

            if (LastScreenShot is null) {
                return null;
            }
            FullScreens.Add(LastScreenShot);

            return FullScreens;
        }

        private GenshinItem CreateGenshinItem(Bitmap InventoryScreenshot, Rect ItemPos, int line, int PixelShift = 0)
        {
            // Pixel offset in case there is a "NEW" on the item
            int OffsetForNew = 30;

            // Pixel offset after the image for the amount
            int OffsetForAmount = 5;

            return new GenshinItem(
                InventoryScreenshot.Clone(
                    new Rectangle(
                        ItemPos.topLeft.X,
                        ItemPos.topLeft.Y
                            + 175 * line
                            + OffsetForNew
                            + PixelShift,
                        ItemPos.rectSize.Width,
                        ItemPos.rectSize.Height - OffsetForNew
                        ),
                    PixelFormat.Format32bppArgb
                    ),
                InventoryScreenshot.Clone(
                    new Rectangle(
                        ItemPos.topLeft.X,
                        ItemPos.topLeft.Y
                            + 175 * line
                            + ItemPos.rectSize.Height
                            + OffsetForAmount
                            + PixelShift,
                        ItemPos.rectSize.Width,
                        20
                        ),
                    PixelFormat.Format32bppArgb
                    ),
                "",
                0
                );
        }

    }
}
