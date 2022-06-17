using BlazorClientAuthHosted.Server.Repository;
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
    public class CheckoutController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;
        private readonly ICheckoutRepository _checkoutRepository;

        public CheckoutController(ICartRepository cartRepository,
            ICheckoutRepository checkoutRepository)
        {
            _cartRepository = cartRepository;
            _checkoutRepository = checkoutRepository;
        }

        //VIEW CART DATA
        [HttpGet("viewCartItems")]
        public IActionResult ViewCartItems()
        {
            var data = _cartRepository.DisplayCartItems();
            return Ok(data);
        }

        [HttpGet("addItemToCart/{id}")]
        public async Task<IActionResult> AddProductToCart(Guid id)
        {
            var cartId = await _cartRepository.AddItemToCartAsync(id);
            return Ok(cartId);
        }

        [HttpDelete("deleteCartItem/{id}")]
        public IActionResult DeleteCartProduct(Guid id)
        {
            _cartRepository.DeleteCartItem(id);
            return Ok();
        }


        //------------------CHECKOUT--------------------------------//
        [HttpGet("checkout")]
        public IActionResult Checkout()
        {
            var orderId = _checkoutRepository.CheckoutProcess();
            orderId = orderId.Remove(0, 1);
            return Ok(orderId); //ON17062022....
        }

        [HttpGet("previousOrderList")]
        public IActionResult PreviousOrders()
        {
            var data = _checkoutRepository.PreviousOrdersList();
            return Ok(data);
        }

        [HttpGet("previousOrderDetails/{id}")]
        public IActionResult PreviousOrderDetails(Guid id)
        {
            var data = _checkoutRepository.PreviousOrderDetails(id);
            return Ok(data);
        }

    }
}
