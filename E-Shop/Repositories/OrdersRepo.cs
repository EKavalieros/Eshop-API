using E_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop.Repositories
{
    public class OrdersRepo : IOrdersRepo
    {
        private readonly AppDbContext _context;

        public OrdersRepo(AppDbContext context)
        {
            _context = context;

            foreach (var order in _context.Orders)
            {
                var orderDetails = _context.OrderDetails.Where(i => i.OrderId == order.Id).ToList();

                foreach (var orderDetail in orderDetails)
                {
                    if (!_context.OrderDetails.Contains(orderDetail))
                    {
                        _context.OrderDetails.Add(orderDetail);
                        _context.SaveChanges();
                    }
                }
            }
        }

        public void DeleteOrder(Order order)
        {
            _context.OrderDetails.Where(b => b.OrderId.Equals(order.Id)).ToList().Clear();
            _context.Orders.Remove(order);
            _context.SaveChanges();
        }

        public IQueryable<Order> GetAllOrders()
        {
            return _context.Orders;
        }

        public Order GetOrderByID(Guid orderId)
        {
            return _context.Orders.FirstOrDefault(o => o.Id.Equals(orderId));
        }

        public Order SubmitOrder(Basket basket)
        {
            var order = new Order()
            {
                Id = new Guid(),
                CustomerId = basket.CustomerId
            };

            _context.Orders.Add(order);

            var basketItems = basket.CartProducts;

            foreach (var basketItem in basketItems)
            {
                var orderDetails = new OrderDetails()
                {
                    Id = new Guid(),
                    OrderId = order.Id,
                    ProductId = basketItem.ProductId,
                    ProductName = basketItem.ProductName,
                    ProductPrice = basketItem.ProductPrice,
                    ProductQty = basketItem.ProductQty
                };

                _context.OrderDetails.Add(orderDetails);
            }

            _context.Baskets.Remove(basket);
            _context.BasketItems.RemoveRange(basketItems);

            _context.SaveChanges();

            return order;
        }
    }
}
