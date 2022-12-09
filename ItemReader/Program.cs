using ItemReader.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ItemReader
{
    static class Program {

        public static IServiceProvider? ServiceProvider { get; private set; }

        static IHostBuilder CreateHostBuilder()
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

        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var host = CreateHostBuilder().Build();
            ServiceProvider = host.Services;

            Application.Run(ServiceProvider.GetRequiredService<MainForm>());
        }

    }
}
