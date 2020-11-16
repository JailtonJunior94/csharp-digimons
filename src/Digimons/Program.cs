using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Digimons.Services;

namespace Digimons
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHttpClient<IDigimonService, DigimonService>();
                    services.AddHostedService<Worker>();
                });
    }
}
