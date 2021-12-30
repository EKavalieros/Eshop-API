using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop.Models
{   
    public class Basket
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid CustomerId { get; private set; }

        [Required]
        public DateTime CreationDate { get; private set; }

        public decimal TotalPrice => CartProducts.Sum(p => p.ProductPrice);

        public int TotalQuantity => CartProducts.Sum(p => p.ProductQty);

        public ICollection<BasketItem> CartProducts => _cartProducts;
        private ICollection<BasketItem> _cartProducts;

        public Basket(Guid customerId)
        {
            CustomerId = customerId;
            _cartProducts = new List<BasketItem>();
            CreationDate = DateTime.Now;
        }

        public void AddProduct(Product product)
        {
            if (!_cartProducts.Any(p => p.ProductId.Equals(product.Id)))
            {
                var cartProduct = new BasketItem()
                {
                    Id = new Guid(),
                    BasketId = Id,
                    ProductId = product.Id,
                    ProductName = product.Name,
                    ProductPrice = product.Price,
                    ProductQty = 1

                };
                _cartProducts.Add(cartProduct);
            }
        }
    }
}
