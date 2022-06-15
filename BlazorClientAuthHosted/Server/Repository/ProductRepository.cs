using BlazorClientAuthHosted.Server.Data;
using BlazorClientAuthHosted.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorClientAuthHosted.Server.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductModel>> GetAllProductsAsync()
        {
            var data = await _context.Products.OrderBy(x=>x.Name).ToListAsync();
            return data;
        }

        public async Task CreateProductAsync(ProductModel model)
        {
            var product = new ProductModel()
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Description = model.Description,
                Quantity = model.Quantity,
                Cost = model.Cost,
                Category = model.Category,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now
            };

            await _context.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        //DELETE PRODUCT
        public void DeleteProduct(Guid id)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == id);
            _context.Products.Remove(product);
            _context.SaveChanges();
        }


        //GET A SINGLE PRODUCT
        public async Task<ProductModel> GetProductAsync(Guid id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            return product;
        }


        //UPDATE PRODUCT
        public async Task<ProductModel> UpdateProductAsync(Guid id, ProductModel model)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == id);

            //product = model;

            product.Name = model.Name;
            product.Description = model.Description;
            product.UpdatedOn = DateTime.Now;
            product.Quantity = model.Quantity;
            product.Cost = model.Cost;

            await _context.SaveChangesAsync();

            return product;
        }
    }
}
