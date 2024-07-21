﻿using JewelleryShop.Business.Service.Interface;
using JewelleryShop.DataAccess.Models;
using JewelleryShop.DataAccess.Models.ViewModel.ImgBBViewModel;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace JewelleryShop.Business.Service
{
    public class GoldPriceResponse
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedAtReadable { get; set; }
    }
    public class GoldPrices
    {
        public string GoldPurity { get; set; }
        public decimal BuyPrice { get; set; }
        public decimal SellPrice { get; set; }
    }

    public class GoldPriceService : IGoldPriceService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://api.gold-api.com/price/XAU";
        private readonly IConfiguration _configuration;
        private readonly IExchangeRateService _exchangeRateService;

        public GoldPriceService(HttpClient httpClient, IConfiguration configuration, IExchangeRateService exchangeRateService)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _exchangeRateService = exchangeRateService;
        }

        public async Task<decimal?> GetGoldPriceAsync()
        {
            var response = await _httpClient.GetAsync(BaseUrl);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var goldPriceResponse = JsonConvert.DeserializeObject<GoldPriceResponse>(content);
            return goldPriceResponse.Price;
        }
        public async Task<List<GoldPrices>> GetGoldPricesTodayAsync()
        {
            List<GoldPrices> res = new();
            

            return res;
        }
    }
}
