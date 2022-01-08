using E_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop.Repositories
{
    public interface IBasketRepo
    {
        Task<List<Basket>> GetBaskets();
        Task<Basket> GetBasketByID(Guid basketId);
        Task<Basket> GetCustomerBasket(Guid customerId);
        Task<Product> GetProductFromBasket(Guid customerId, Guid productId);
        Task<Basket> CreateBasket(Guid customerId);
        Task AddProductToBasket(Guid basketId, Product product);
        Task RemoveBasketItem(Basket basket, BasketItem basketItem);
        Task DeleteBasket(Guid basketId);
        Task UpdateBasketItemQuantity(BasketItem basketItem, int newQty);
        bool CanBeProductAddedToCart(int productStockQty);
    }
}
