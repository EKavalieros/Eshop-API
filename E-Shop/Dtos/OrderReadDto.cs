using E_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop.Dtos
{
    public class OrderReadDto
    {
        public Guid Id { get; set; }

        public Guid CustomerId { get; set; }

        public DateTime CreationDate { get; private set; }

        public decimal TotalPrice => OrderDetails.Sum(o => o.ProductPrice);

        public int TotalQuantity => OrderDetails.Sum(o => o.ProductQty);

        public ICollection<OrderDetails> OrderDetails => _orderDetails;
        private ICollection<OrderDetails> _orderDetails;

        public OrderReadDto()
        {
            _orderDetails = new List<OrderDetails>();
            CreationDate = DateTime.Now;
        }
    }
}
