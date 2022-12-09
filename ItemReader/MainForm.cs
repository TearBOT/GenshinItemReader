using ItemReader.Interfaces;
using ItemReader.InventoryScreener;
using ItemReader.Models;
using Newtonsoft.Json;
using System.Drawing.Imaging;

namespace ItemReader
{
    public partial class MainForm : Form {

        /* CLASS VARIABLE(S) */

        private IWindowCatcher _WindowCatcher;
        private IInventoryScreener _InventoryScreener;
        private IInventoryParser _InventoryParser;
        private IInventoryChecker _InventoryChecker;
        private Config JsonConfig = null;

        /* GENERATED METHODS */

        public MainForm()
        {
            // GENERATED METHOD
            InitializeComponent();
            
            // DI & CONFIG FILE
            AssignStartingValues();

            // CHECK IF GAME IS OPEN (GAME NAME IS TAKEN FROM CONFIG FILE)
            EvaluateGameWindowState();
        }

        private void LoggerTextChanged(object Sender, EventArgs E) {}

        private void LoadForm(object Sender, EventArgs E) {}

        private void JsonOpenerFileOk(object Sender, System.ComponentModel.CancelEventArgs E) {}


        /* CODE WRITTEN IN GENERATED METHODS */

        private void GenshinFinderOnClick(object Sender, EventArgs E)
        {
            EvaluateGameWindowState();
        }

        private void ScanItemsOnClick(object Sender, EventArgs E)
        {
            if (EvaluateInventoryState() is false) {
                WriteText("Checking for inventory failed\n");
                return;
            }

            var ItemList = ProcessInventoryScreenshots();

            if (ItemList is null) {
                WriteText("Inventory Reading failed\n");
                return;
            }

#if DEBUG
            foreach (var Item in ItemList) {
                var TimeStamp = $"{DateTime.Now.Hour}-{DateTime.Now.Minute}-{DateTime.Now.Second}-{DateTime.Now.Millisecond}";
                Item.ItemImage.Save($@"C:\Users\Mini-Soo\Projects\test\Images\test_{TimeStamp}.png", ImageFormat.Png);
                Item.AmountImage.Save($@"C:\Users\Mini-Soo\Projects\test\Amounts\test_{TimeStamp}.png", ImageFormat.Png);
            }
#endif
            WriteText(
                "Over\n"
                + "Your file can be found at"
                + $@"{JsonConfig.DebugFolderPath}\Mats.json"
                );
        }

        /* ACTUAL WRITTEN CODE*/

        private void AssignStartingValues()
        {
            _WindowCatcher = (IWindowCatcher)Program.ServiceProvider.GetService(typeof(IWindowCatcher));
            _InventoryScreener = (IInventoryScreener)Program.ServiceProvider.GetService(typeof(IInventoryScreener));
            _InventoryParser = (IInventoryParser)Program.ServiceProvider.GetService(typeof(IInventoryParser));
            _InventoryChecker = (IInventoryChecker)Program.ServiceProvider.GetService(typeof(IInventoryChecker));

            string jsonString = File.ReadAllText(@"..\..\..\Resources\config.json");
            JsonConfig = JsonConvert.DeserializeObject<Config>(jsonString);

            if (_WindowCatcher is null
                || _InventoryScreener is null
                || _InventoryParser is null
                || _InventoryChecker is null
                || JsonConfig is null) {
                throw new NullReferenceException();
            }
        }

        private void WriteText(string Text)
        {
            // ADD TEXT TO LOG BOX
            logger.AppendText(Text);

            // SCOLL DOWN TO BOTTOM
            logger.ScrollToCaret();

            // REFRESH THE WHOLE WIDGET TO DISPLAY THE ADDED TEXT
            Application.DoEvents();
        }

        private bool EvaluateGameWindowState()
        {
            if (_WindowCatcher.IsGameWindowOpen(JsonConfig.GameWindowName)) {
                WriteText("Genshin Found\n");
                return true;
            }

            WriteText("Genshin Not Found\n");
            return false;
        }

        private bool EvaluateInventoryState()
        {
            if (EvaluateGameWindowState() is false) {
                return false;
            }

            if (_InventoryScreener.ProcessGameWindowInfo(
                _WindowCatcher.GameWindow,
                _WindowCatcher.GameWindowBounds,
                GenshinItemCoordinatesReader.EvaluateAllItemsPositions()
                ) is false) {
                WriteText("Error Loading Coordinates Json File\n");
                return false;
            }

            if (_InventoryScreener.IsInventoryOpen()) {
                WriteText(
                    "Inventory open\n"
                    + "Starting Scan\n\n"
                    + "PLEASE DO NOT MOVE YOUR GENSHIN WINDOW\n"
                    + "PLEASE DO NOT TOUCH YOUR MOUSE\n\n");

                return true;
            }

            WriteText("Inventory is not open\n");

            return false;
        }

        private IEnumerable<GenshinItem> ProcessInventoryScreenshots()
        {            
            var InventoryScreenshots = _InventoryScreener.TakeInventoryScreenShots();

            if (InventoryScreenshots is null) {
                WriteText("Taking Screenshots has failed\n");
                return null;
            }

            WriteText(
                "Taking Screenshots over\n\n"
                + "You can go back to the game and play!\n"
                + "Processing Screenshots\n\n");

            var ItemList = _InventoryScreener.SplitInventoryItems(InventoryScreenshots);

            return ItemList;
        }

    }
}