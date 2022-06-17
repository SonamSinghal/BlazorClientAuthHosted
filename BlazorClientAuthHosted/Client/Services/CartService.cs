using BlazorClientAuthHosted.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorClientAuthHosted.Client.Services
{
    public class CartService : ICartService
    {
        private readonly HttpClient _httpClient;

        public CartService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Guid> AddProductToCartAsync(Guid id)
        {
            var data = await _httpClient.GetFromJsonAsync<Guid>($"Checkout/addItemToCart/{id}");
            return data;
        }

        public async Task<IList<ProductModel>> ViewCartItemsAsync()
        {
            var data = await _httpClient.GetFromJsonAsync<List<ProductModel>>("Checkout/viewCartItems");
            return data;
        }

        public async Task DeleteCartItemAsync(Guid id)
        {
            await _httpClient.DeleteAsync($"Checkout/deleteCartItem/{id}");
        }


    }
}
