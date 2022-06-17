using BlazorClientAuthHosted.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorClientAuthHosted.Client.Services
{
    public class CheckoutService : ICheckoutService
    {
        private readonly HttpClient _httpClient;

        public CheckoutService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> InitiateCheckoutAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<string>("Checkout/checkout");
            return result;
        }

        public async Task<List<CheckoutModel>> PreviousListAsync()
        {
            var data = await _httpClient.GetFromJsonAsync<List<CheckoutModel>>("Checkout/previousOrderList");
            return data;
        }

        public async Task<CheckoutModel> PreviousOrderDetails(Guid id)
        {
            var data = await _httpClient.GetFromJsonAsync<CheckoutModel>($"Checkout/previousOrderDetails/{id}");
            return data;
        }
    }

}
