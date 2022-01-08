using E_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop.Repositories
{
    public interface IOrdersRepo
    {
        Task<List<Order>> GetAllOrders();
        Task<Order> GetOrderByID(Guid orderId);
        Task<Order> SubmitOrder(Basket basket);
        Task DeleteOrder(Order order);
    }
}
