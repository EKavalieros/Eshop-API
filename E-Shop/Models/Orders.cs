using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop.Models
{
    public class Order
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid CustomerId { get; set; }

        [Required]
        public DateTime CreationDate { get; private set; }

        [Required]
        public decimal TotalPrice => OrderDetails.Sum(o => o.ProductPrice);    
        
        [Required]
        public int TotalQuantity => OrderDetails.Sum(o => o.ProductQty);

        public ICollection<OrderDetails> OrderDetails => _orderDetails;
        private ICollection<OrderDetails> _orderDetails;

        public Order()
        {
            _orderDetails = new List<OrderDetails>();
            CreationDate = DateTime.Now;
        }
    }
}
