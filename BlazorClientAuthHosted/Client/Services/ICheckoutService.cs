using BlazorClientAuthHosted.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorClientAuthHosted.Client.Services
{
    public interface ICheckoutService
    {
        Task<string> InitiateCheckoutAsync();
        Task<List<CheckoutModel>> PreviousListAsync();
        Task<CheckoutModel> PreviousOrderDetails(Guid id);
    }
}