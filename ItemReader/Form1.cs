using ItemReader.Interfaces;
using ItemReader.ScreenShotter;
using ItemReader.Utility;
using System.ComponentModel.Design;

namespace ItemReader
{
    public partial class Form1 : Form {

        private readonly IWindowCatcher _WindowCatcher;
        private IInventoryScreener _InventoryScreener;
        private IInventoryParser _InventoryParser;
        private IInventoryChecker _InventoryChecker;

        public Form1()
        {
            InitializeComponent();
            // MessageBox.Show(_windowCatcher!.testMessage());
            _WindowCatcher = (IWindowCatcher)Program.ServiceProvider.GetService(typeof(IWindowCatcher));
            if (_WindowCatcher == null) {
                throw new NullReferenceException();
            }
            _InventoryScreener = (IInventoryScreener)Program.ServiceProvider.GetService(typeof(IInventoryScreener));
            if (_InventoryScreener == null) {
                throw new NullReferenceException();
            }
            _InventoryParser = (IInventoryParser)Program.ServiceProvider.GetService(typeof(IInventoryParser));
            if (_InventoryParser == null) {
                throw new NullReferenceException();
            }
            _InventoryChecker = (IInventoryChecker)Program.ServiceProvider.GetService(typeof(IInventoryChecker));
            if (_InventoryChecker == null) {
                throw new NullReferenceException();
            }
            if (_WindowCatcher.catchGameWindow() == false) {
                logger.Text += "Genshin Not Found\n";
            }
            else {
                logger.Text += "Genshin Found\n";
            }
        }

        private void onClick_GenshinFinder(object sender, EventArgs e)
        {
            if (_WindowCatcher.catchGameWindow() == false) {
                logger.Text += "Genshin Not Found\n";
            } else {
                logger.Text += "Genshin Found\n";
            }
        }

        private void onClick_ScanItems(object sender, EventArgs e)
        {

            _InventoryScreener.setWindowInfo(_WindowCatcher.getGameWindow(), _WindowCatcher.getWindowBouds(), AllCoordinatesFactory.getAllCoordinates());
            if (_InventoryScreener.isInventoryOpen() == false) {
                logger.Text += "Inventory is not open\n";
            } else {
                logger.Text += "Inventory open\nStarting Scan\n";
            }

            List<Bitmap> screens = _InventoryScreener.GetFullInventory().ToList();

            foreach (Bitmap screen in screens) {
                screen.Dispose();
            }
            logger.Text += "Over\n";
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e) { }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void jsonOpener_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
    }
}