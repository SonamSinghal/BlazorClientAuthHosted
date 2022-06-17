using BlazorClientAuthHosted.Server.Data;
using BlazorClientAuthHosted.Shared;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlazorClientAuthHosted.Server.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _userContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartRepository(ApplicationDbContext userContext,
            IHttpContextAccessor httpContextAccessor)
        {
            _userContext = userContext;
            _httpContextAccessor = httpContextAccessor;
        }

        //ADD ITEMS TO CART
        public async Task<Guid> AddItemToCartAsync(Guid productId)
        {

            var cartItem = _userContext.CartItems.SingleOrDefault(x => x.ProductId == productId);
            var Id = Guid.NewGuid();

            if (cartItem == null)
            {
                cartItem = new CartItemsModel()
                {
                    Id = Id,
                    UserId = Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)),
                    ProductId = productId,

                    Quantity = 1,
                };
                await _userContext.CartItems.AddAsync(cartItem);
                await _userContext.SaveChangesAsync();

                return Id;
            }
            else
            {
                cartItem.Quantity++;
                await _userContext.SaveChangesAsync();
                return productId;
            }
        }

        public void DeleteCartItem(Guid id)
        {
            var item = _userContext.CartItems.FirstOrDefault(x => x.ProductId == id);
            _userContext.CartItems.Remove(item);
            _userContext.SaveChanges();
        }

        public List<ProductModel> DisplayCartItems()
        {
            var signedInUserId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var data = _userContext.CartItems.Where(x => x.UserId == Guid.Parse(signedInUserId)).ToList();
            var Product = new List<ProductModel>();
            foreach (var productId in data)
            {
                var item = _userContext.Products.Where(x => x.Id == productId.ProductId).ToList();
                Product.Add(item[0]);
            }

            return Product;
        }
    }
}
