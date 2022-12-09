using ItemReader.Interfaces;
using ItemReader.Models;
using ItemReader.Utils;
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
        private GenshinItemCoordinates _GenshinItemCoordinates;
        private IntPtr _GameWindow;
        private Rect _GameWindowBounds;

        /* PUBLIC METHOD(S) */

        public bool IsInventoryOpen()
        {
            Bitmap inventoryScreen = ScreenShotter.TakeCroppedScreenShot(
                _GameWindow,
                _GameWindowBounds,
                new Rectangle(
                    _GenshinItemCoordinates.BagIconPos.TopLeft,
                    _GenshinItemCoordinates.BagIconPos.RectSize
                    )
                );
            Bitmap BagIcon = new Bitmap(@"..\..\..\Resources\BagIcon.png"); 

            bool Result = ImageComparator.ComapreImages(inventoryScreen, BagIcon);

            if (inventoryScreen is not null) {
                inventoryScreen.Dispose();
            }
            BagIcon.Dispose();

            return Result;
        }

        public bool ProcessGameWindowInfo(IntPtr GameWindow, Rect GameWindowBounds, GenshinItemCoordinates GenshinItemsPos)
        {
            if (GenshinItemsPos.FirstLineItemsPos.Count <= 0) {
                return false;
            }

            _GameWindow = GameWindow;
            _GameWindowBounds = GameWindowBounds;
            _GenshinItemCoordinates = GenshinItemsPos;

            _TopLeftItem = new Point(
                _GameWindowBounds.TopLeft.X + 180,
                _GameWindowBounds.TopLeft.Y + 180
                );
            _BottomLeftItem = new Point(
                _GameWindowBounds.TopLeft.X + 180,
                _GameWindowBounds.TopLeft.Y + 900
                );
            _MaterialTabClick = new Point(
                _GameWindowBounds.TopLeft.X + 770,
                _GameWindowBounds.TopLeft.Y + 65
                );
            _SpecialitiesTabClick = new Point(
                _GameWindowBounds.TopLeft.X + 960,
                _GameWindowBounds.TopLeft.Y + 65
                );

            return true;
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

        /* PRIVATE METHOD(S) */

        private IEnumerable<Bitmap> TakeScreenShotsInTab(Point TabToSelect, int loops)
        {
            List<Bitmap> FullScreens = new List<Bitmap>();

            // Click on the tab you want to take screenshots of
            MouseEmulator.MouseLeftClick(_GameWindow, TabToSelect);
            Thread.Sleep(1000);
            // Wait for the tab to load and scroll up after clicking on the first item
            MouseEmulator.MouseScrollUp(_GameWindow, _TopLeftItem, 400);
            Thread.Sleep(500);
            // Wait for the scroll's velocity to settle down and click on the bottom left-most item
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

        private List<GenshinItem> ExtractItems(List<Bitmap> InventoryScreenshots)
        {
            List<GenshinItem> ItemList = new List<GenshinItem>();
            int PixelShift = 0;

            // Loop for every screenshot of the inventory taken
            foreach (var InventoryScreenshot in InventoryScreenshots) {
                // Loop for every item line within that screenshot
                for (int line = 0; line < 4; line++) {
                    // Loop for every item within the line
                    foreach (var ItemPos in _GenshinItemCoordinates.FirstLineItemsPos) {
                        ItemList.Add(CreateGenshinItem(InventoryScreenshot, ItemPos, line, PixelShift));
                    }
                }
                // Genshin's inventory is shit so you need to shift the pixels with every "scroll"
                // otherwise you'd scroll too much and end up getting the item's value in the "ItemImage" part
                PixelShift--;
                if (PixelShift % 4 == 0) {
                    PixelShift--;
                }
            }

            // Same loop as above but for the end of the inventory
            for (int line = 0; line < 4; line++) {
                foreach (var ItemPos in _GenshinItemCoordinates.LastLineItemsPos) {
                    ItemList.Add(CreateGenshinItem(InventoryScreenshots.Last(), ItemPos, line));
                }
            }

            return ItemList;
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
                        ItemPos.TopLeft.X,
                        ItemPos.TopLeft.Y
                            + 175 * line
                            + OffsetForNew
                            + PixelShift,
                        ItemPos.RectSize.Width,
                        ItemPos.RectSize.Height - OffsetForNew
                        ),
                    PixelFormat.Format32bppArgb
                    ),
                InventoryScreenshot.Clone(
                    new Rectangle(
                        ItemPos.TopLeft.X,
                        ItemPos.TopLeft.Y
                            + 175 * line
                            + ItemPos.RectSize.Height
                            + OffsetForAmount
                            + PixelShift,
                        ItemPos.RectSize.Width,
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
