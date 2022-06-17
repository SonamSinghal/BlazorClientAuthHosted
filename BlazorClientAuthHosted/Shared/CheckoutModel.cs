using BlazorClientAuthHosted.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorClientAuthHosted.Shared
{
    public class CheckoutModel
    {
        [Key]
        public Guid Id { get; set; }

        public Guid OrderId { get; set; }

        [NotNull]
        public string ReferenceId { get; set; }

        [ForeignKey("UserModel")]
        public Guid UserId { get; set; }

        [ForeignKey("ProductModel")]
        public Guid ProductId { get; set; }

        [NotMapped]
        public List<ProductModel> Products { get; set; } = new List<ProductModel>();

        public int Quantity { get; set; }

        public DateTime OrderOn { get; set; }
    }
}
