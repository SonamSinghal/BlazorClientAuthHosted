using BlazorClientAuthHosted.Model;
using System;
using System.Collections.Generic;

namespace BlazorClientAuthHosted.Server.Repository
{
    public interface ICheckoutRepository
    {
        string CheckoutProcess();
        CheckoutModel PreviousOrderDetails(Guid orderId);
        List<CheckoutModel> PreviousOrdersList();
    }
}