using BlazorClientAuthHosted.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorClientAuthHosted.Client.Services
{
    public interface IProductsService
    {
        Task<List<ProductModel>> GetProductModelsAsync();
        Task<ProductModel> ProductDetailsAsync(Guid id);
        Task<ProductModel> CreateProductAsync(ProductModel product);
        Task<ProductModel> UpdateProductAsync(Guid id, ProductModel product);
        Task DeleteProductAsync(Guid id);
    }
}