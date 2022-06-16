using BlazorClientAuthHosted.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorClientAuthHosted.Model
{
    public class CartItemsModel
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("UserModel")]
        public Guid UserId { get; set; }

        [ForeignKey("ProductsModel")]
        public Guid ProductId { get; set; }

        [NotMapped]
        public ProductModel Product { get; set; }
        public int Quantity { get; set; }
    }
}
