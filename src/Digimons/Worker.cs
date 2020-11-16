using System;
using System.Threading;
using Digimons.Services;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Digimons
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IDigimonService _service;

        public Worker(ILogger<Worker> logger,
                      IDigimonService service)
        {
            _logger = logger;
            _service = service;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                var watch = new Stopwatch();
                watch.Start();

                var names = await _service.GetNamesAsync();
                foreach (var name in names)
                {
                    await _service.RequestAsync($"https://digimon-api.vercel.app/api/digimon/name/{name}");
                }

                watch.Stop();
                _logger.LogInformation($"Execution Time: {watch.ElapsedMilliseconds} ms");

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
