using ItemReader.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ItemReader
{
    static class Program {

        /* CLASS VARIABLE(S) */

        public static IServiceProvider? ServiceProvider { get; private set; }

        /* GENERATED METHOD(S) */

        [STAThread]
        static void Main() {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var host = DependencyInjection.CreateHostBuilder().Build();
            ServiceProvider = host.Services;

            Application.Run(ServiceProvider.GetRequiredService<MainForm>());
        }

    }
}
