using BlazorClientAuthHosted.Shared;
using System;
using System.Collections.Generic;

namespace BlazorClientAuthHosted.Server.Repository
{
    public interface ICheckoutRepository1
    {
        string CheckoutProcess();
        CheckoutModel PreviousOrderDetails(Guid orderId);
        List<CheckoutModel> PreviousOrdersList();
    }
}