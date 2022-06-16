using BlazorClientAuthHosted.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorClientAuthHosted.Server.Repository
{
    public interface ICartRepository
    {
        Task AddItemToCartAsync(Guid productId);
        void DeleteCartItem(Guid id);
        List<ProductModel> DisplayCartItems();
    }
}