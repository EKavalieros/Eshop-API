using E_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop.Dtos
{
    public class BasketReadDto
    {
        public Guid Id { get; set; }

        public Guid CustomerId { get; private set; }

        public DateTime CreationDate { get; private set; }

        public decimal TotalPrice => CartProducts.Sum(p => p.ProductPrice);

        public int TotalQuantity => CartProducts.Sum(p => p.ProductQty);

        public ICollection<BasketItem> CartProducts => _cartProducts;
        private ICollection<BasketItem> _cartProducts;

        public BasketReadDto(Guid customerId)
        {
            CustomerId = customerId;
            _cartProducts = new List<BasketItem>();
            CreationDate = DateTime.Now;
        }
    }
}
