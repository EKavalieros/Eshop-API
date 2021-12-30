using E_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop.Repositories
{
    public interface IOrdersRepo
    {
        IQueryable<Order> GetAllOrders();
        Order GetOrderByID(Guid orderId);
        Order SubmitOrder(Basket basket);
        void DeleteOrder(Order order);
    }
}
