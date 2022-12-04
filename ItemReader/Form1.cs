using ItemReader.Interfaces;
using ItemReader.ScreenShotter;
using System.ComponentModel.Design;

namespace ItemReader
{
    public partial class Form1 : Form
    {
        private readonly IWindowCatcher _windowCatcher;
        private IInventoryScreener _inventoryScreener;
        private IInventoryParser _inventoryParser;
        private IInventoryChecker _inventoryChecker;

        public Form1()
        {
            _windowCatcher = (IWindowCatcher)Program.ServiceProvider.GetService(typeof(IWindowCatcher));
            if (_windowCatcher == null)
            {
                throw new NullReferenceException();
            }
            // MessageBox.Show(_windowCatcher!.testMessage());
            InitializeComponent();
        }

        private void onClick_GenshinFinder(object sender, EventArgs e)
        {
            _windowCatcher.catchGameWindow();
        }

        private void onClick_ScanItems(object sender, EventArgs e)
        {
            _inventoryScreener = (IInventoryScreener)Program.ServiceProvider.GetService(typeof(IInventoryScreener));
            if (_inventoryScreener == null)
            {
                throw new NullReferenceException();
            }
            _inventoryScreener.setWindowInfo(_windowCatcher.getGameWindow(), _windowCatcher.getWindowBouds());
            if (_inventoryScreener.isInventoryOpen() == false)
            {
                logger.Text += "Inventory is not open\n";
            }
            List<Bitmap> screens = _inventoryScreener.getItems();
            foreach (Bitmap screen in screens)
            {
                screen.Dispose();
            }
            _inventoryParser = (IInventoryParser)Program.ServiceProvider.GetService(typeof(IInventoryParser));
            _inventoryChecker = (IInventoryChecker)Program.ServiceProvider.GetService(typeof(IInventoryChecker));

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void jsonOpener_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
    }
}