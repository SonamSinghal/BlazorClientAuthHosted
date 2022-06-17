using BlazorClientAuthHosted.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorClientAuthHosted.Client.Services
{
    public interface ICartService
    {
        Task<Guid> AddProductToCartAsync(Guid id);
        Task DeleteCartItemAsync(Guid id);
        Task<IList<ProductModel>> ViewCartItemsAsync();
    }
}