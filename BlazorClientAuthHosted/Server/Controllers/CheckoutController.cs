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
        public IActionResult ViewCartItems()
        {
            var data = _cartRepository.DisplayCartItems();
            return Ok(data);
        }

        public async Task<IActionResult> AddProductToCart(Guid id)
        {
            await _cartRepository.AddItemToCartAsync(id);
            return Ok("ViewCartItems", "User");
        }


        //------------------CHECKOUT--------------------------------//
        public IActionResult Checkout()
        {
            var orderId = _checkoutRepository.CheckoutProcess();
            return Ok(orderId);
        }

        public IActionResult PreviousOrders()
        {
            var data = _checkoutRepository.PreviousOrdersList();
            return Ok(data);
        }

        public IActionResult PreviousOrderDetails(Guid id)
        {
            var data = _checkoutRepository.PreviousOrderDetails(id);
            return Ok(data);
        }

    }
}
