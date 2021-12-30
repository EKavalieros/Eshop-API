using E_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop.Repositories
{
    public interface IBasketRepo
    {
        IQueryable<Basket> GetBaskets();
        Basket GetBasketByID(Guid basketId);
        Basket GetCustomerBasket(Guid customerId);
        Product GetProductFromBasket(Guid customerId, Guid productId);
        Basket CreateBasket(Guid customerId);
        void AddProductToBasket(Guid basketId, Product product);
        void RemoveBasketItem(Basket basket, BasketItem basketItem);
        void DeleteBasket(Guid basketId);
        void UpdateBasketItemQuantity(BasketItem basketItem, int newQty);
        bool CanBeProductAddedToCart(int productStockQty);
    }
}
