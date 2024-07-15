using JewelleryShop.Business.Service.Interface;
using JewelleryShop.DataAccess.Models;
using JewelleryShop.DataAccess.Models.ViewModel.ImgBBViewModel;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JewelleryShop.Business.Service
{
    public class ExchangeRateResponse
    {
        public string Base { get; set; }
        public DateTime Date { get; set; }
        public long TimeLastUpdated { get; set; }
        public Dictionary<string, decimal> Rates { get; set; }
    }

    public class ExchangeRateService : IExchangeRateService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private const string BaseUrl = "https://api.exchangerate-api.com/v4/latest/USD";

        public ExchangeRateService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<ExchangeRateResponse> GetLatestRatesAsync()
        {
            var response = await _httpClient.GetAsync(BaseUrl);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ExchangeRateResponse>(content);
        }

        public async Task<decimal?> GetRateForVndAsync()
        {
            var response = await _httpClient.GetAsync(BaseUrl);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var exchangeRateResponse = JsonConvert.DeserializeObject<ExchangeRateResponse>(content);

            if (exchangeRateResponse.Rates.TryGetValue("VND", out var rate))
            {
                return rate;
            }

            return null;
        }
    }
}
