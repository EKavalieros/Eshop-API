using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop.Dtos
{
    public class BasketItemsReadDto
    {
        public Guid Id { get; set; }

        public Guid BasketId { get; set; }

        public Guid ProductId { get; set; }

        public string ProductName { get; set; }

        public int ProductQty { get; set; }

        public decimal ProductPrice { get; set; }
    }
}
