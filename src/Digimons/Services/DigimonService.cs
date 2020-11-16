using System;
using System.IO;
using System.Net;
using Digimons.Models;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Digimons.Services
{
    public class DigimonService : IDigimonService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<DigimonService> _logger;

        public DigimonService(HttpClient httpClient,
                              ILogger<DigimonService> logger)
        {
            _logger = logger;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://digimon-api.vercel.app");
        }

        public async Task<string[]> GetNamesAsync()
        {
            var names = await File.ReadAllLinesAsync(@"C:\\Git\\CSharp.Digimons\\src\\Digimons\\Files\\Digimon.txt");
            return names;
        }

        public async Task RequestAsync(string url)
        {
            _logger.LogInformation("Iniciando consulta de digimon");

            var response = await _httpClient.GetAsync(url);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new InvalidOperationException("Erro ao obter digimon");
            }

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var digimon = JsonSerializer.Deserialize<IList<Digimon>>(await response.Content.ReadAsStringAsync(), options);

            _logger.LogInformation($"Encontrado o Digimon: {digimon}");
        }
    }
}
