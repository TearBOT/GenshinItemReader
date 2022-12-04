using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ItemReader;
using System.ComponentModel.Design;
using ItemReader.ScreenShotter;
using ItemReader.InventoryParser;
using ItemReader.InventoryChecker;
using ItemReader.Interfaces;

namespace ItemReader
{
    static class Program
    {
        public static IServiceProvider? ServiceProvider { get; private set; }

        static IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) => {
                    services.AddTransient<IWindowCatcher, WindowCatcher.WindowCatcher>();
                    services.AddTransient<IInventoryScreener, InventoryScreener>();
                    services.AddTransient<IInventoryParser, InventoryParser.InventoryParser>();
                    services.AddTransient<IInventoryChecker, InventoryChecker.InventoryChecker>();
                    services.AddTransient<Form1>();
                });
        }

        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var host = CreateHostBuilder().Build();
            ServiceProvider = host.Services;

            Application.Run(ServiceProvider.GetRequiredService<Form1>());
        }
    }
}
