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
    public class CheckoutRepository : ICheckoutRepository
    {
        private readonly ApplicationDbContext _userContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICartRepository _cartRepository;

        public CheckoutRepository(ApplicationDbContext userContext,
            IHttpContextAccessor httpContextAccessor,
            ICartRepository cartRepository)
        {
            _userContext = userContext;
            _httpContextAccessor = httpContextAccessor;
            _cartRepository = cartRepository;
        }

        public string CheckoutProcess()
        {
            var signedInUserId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var data = _userContext.CartItems.Where(x => x.UserId == Guid.Parse(signedInUserId)).ToList();
            var orderId = Guid.NewGuid();
            var referenceId = UniqueReferenceID();
            if (data.Count == 1)
            {
                var checkout = new CheckoutModel()
                {
                    Id = Guid.NewGuid(),
                    OrderId = orderId,
                    ReferenceId = referenceId,
                    UserId = Guid.Parse(signedInUserId),
                    OrderOn = DateTime.Now,
                    ProductId = data[0].ProductId,
                    Quantity = data[0].Quantity,
                };
                _userContext.Checkout.Add(checkout);
                _userContext.SaveChanges();
                _cartRepository.DeleteCartItem(data[0].ProductId);
            }
            else
            {
                var date = DateTime.Now;
                var checkoutList = new List<CheckoutModel>();

                foreach (var product in data)
                {
                    var checkout = new CheckoutModel()
                    {
                        Id = Guid.NewGuid(),
                        OrderId = orderId,
                        ReferenceId = referenceId,
                        UserId = Guid.Parse(signedInUserId),
                        OrderOn = date,
                        ProductId = product.ProductId,
                        Quantity = product.Quantity,
                    };
                    checkoutList.Add(checkout);
                    _cartRepository.DeleteCartItem(product.ProductId);
                }

                _userContext.Checkout.AddRange(checkoutList);
                _userContext.SaveChanges();

            }

            return referenceId;
        }


        public List<CheckoutModel> PreviousOrdersList()
        {
            var signedInUserId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            //List containing all orders of particular user
            var data = _userContext.Checkout.Where(x => x.UserId == Guid.Parse(signedInUserId)).OrderBy(x => x.OrderId).ToList();

            //Distinct Order Ids
            var distinctOrderId = _userContext.Checkout.Select(x => x.OrderId).Distinct();

            //return
            var orders = new List<CheckoutModel>();

            foreach (var item in distinctOrderId)
            {
                //Selecting order from distinc order id
                var CompleteOrder = _userContext.Checkout.FirstOrDefault(x => x.OrderId == item);
                orders.Add(CompleteOrder);

            }
            return orders;
        }

        public CheckoutModel PreviousOrderDetails(Guid orderId)
        {
            var CompleteOrder = _userContext.Checkout.FirstOrDefault(x => x.OrderId == orderId);
            //Creating a list of Product Id for a Single order ID
            var prodIdList = _userContext.Checkout.Where(y => y.OrderId == orderId).Select(x => x.ProductId).ToList();

            //Looping through above fetched product list
            foreach (var prodId in prodIdList)
            {
                var product = _userContext.Products.FirstOrDefault(x => x.Id == prodId);
                CompleteOrder.Products.Add(product);
            }

            return CompleteOrder;
        }


        private string UniqueReferenceID()
        {
            string number = "";
            string num = string.Empty;
            var date = DateTime.Today.ToString("ddMMyyyy");
            var id = _userContext.Checkout.Where(x => x.ReferenceId.StartsWith("#ON" + date)).OrderByDescending(x => x.ReferenceId).FirstOrDefault();
            if (id == null)
            {
                number = DateTime.Now.ToString("ddMMyyyy") + "00001";
                return "#ON" + number;
            }
            else
            {
                var referenceId = id.ReferenceId;
                var referno = referenceId.Substring(11, 5);
                var referdate = referenceId.Substring(3, 8);//01062022

                var currDate = new DateTime(Convert.ToInt32(referdate.Substring(4, 4)), Convert.ToInt32(referdate.Substring(2, 2)), Convert.ToInt32(referdate.Substring(0, 2)));

                if ((DateTime.Today - currDate).TotalDays > 1)
                {
                    number = DateTime.Now.ToString("MMDDYYYY") + "00001";
                }

                else
                {
                    referno = "0000" + (Convert.ToInt32(referno) + 1).ToString();
                    if (referno.Length > 5)
                    {
                        referno = referno[^5..];
                    }
                    number = referdate + referno;
                }
                return "#ON" + number;
            }
        }
    }
}

