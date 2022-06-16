using BlazorClientAuthHosted.Server.Repository;
using BlazorClientAuthHosted.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorClientAuthHosted.Server.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        [HttpGet("allProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            var data = await _productRepository.GetAllProductsAsync();
            return Ok(data);
        }

        [HttpGet("productDetails/{id}")]
        public async Task<IActionResult> ProductDetails(string id)
        {
            var data = await _productRepository.GetProductAsync(Guid.Parse(id));
            return Ok(data);
        }

        [HttpPost("createProduct")]
        [Authorize(Policy ="Admin")]
        public async Task<IActionResult> CreateProduct(ProductModel product)
        {
            if (ModelState.IsValid)
            {
                await _productRepository.CreateProductAsync(product);
            }
            else
            {
                ModelState.AddModelError("", "Enter all the Fields!");
            }

            return Ok(product);
        }

        [HttpPut("updateProduct/{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, ProductModel product)
        {
            if (ModelState.IsValid)
            {
                var newProduct = await _productRepository.UpdateProductAsync(id, product);
                return Ok(newProduct);
            }
            return Ok(product);
        }

        [HttpDelete("deleteProduct/{id}")]
        public IActionResult DeleteProduct(Guid id)
        {
            _productRepository.DeleteProduct(id);
            return Ok();
        }
    }
}
