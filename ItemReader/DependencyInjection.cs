using ItemReader.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ItemReader
{
    internal class DependencyInjection
    {
        public static IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) => {
                    services.AddTransient<IWindowCatcher, WindowCatcher.WindowCatcher>();
                    services.AddTransient<IInventoryScreener, InventoryScreener.InventoryScreener>();
                    services.AddTransient<IInventoryParser, InventoryParser.InventoryParser>();
                    services.AddTransient<IInventoryChecker, InventoryChecker.InventoryChecker>();
                    services.AddTransient<MainForm>();
                });
        }
    }
}
