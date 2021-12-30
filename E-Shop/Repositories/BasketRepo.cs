using E_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop.Repositories
{
    public class BasketRepo : IBasketRepo
    {
        private readonly AppDbContext _context;

        public BasketRepo(AppDbContext context)
        {
            _context = context;

            foreach (var basket in _context.Baskets)
            {
                var basketItems = _context.BasketItems.Where(i => i.BasketId == basket.Id).ToList();

                foreach (var basketItem in basketItems)
                {
                    if (!_context.BasketItems.Contains(basketItem))
                    {
                        _context.BasketItems.Add(basketItem);
                        _context.SaveChanges();
                    }
                }
            }
        }

        public Basket GetCustomerBasket(Guid customerId)
        {
            return _context.Baskets.FirstOrDefault(b => b.CustomerId.Equals(customerId));
        }

        public Product GetProductFromBasket(Guid customerId, Guid productId)
        {
            var basketItem = _context.Baskets.FirstOrDefault(b => b.CustomerId.Equals(customerId))?.CartProducts?.FirstOrDefault(p => p.ProductId.Equals(productId));

            return (basketItem == null) ? null : _context.Products.FirstOrDefault(p => p.Id.Equals(basketItem.ProductId));
        }

        public Basket CreateBasket(Guid customerId)
        {
            var basket = new Basket(customerId) { Id = new Guid() };
            _context.Baskets.Add(basket);
            _context.SaveChanges();

            return basket;
        }

        public void AddProductToBasket(Guid basketId, Product product)
        {
            _context.Baskets.FirstOrDefault(b => b.Id.Equals(basketId))?.AddProduct(product);
            UpdateProductStockQuantity(product.Id, 1, false, true);
            _context.SaveChanges();
        }

        private void UpdateProductStockQuantity(Guid productId, int qty, bool increase, bool decrease)
        {
            if (decrease)
                _context.Products.FirstOrDefault(b => b.Id.Equals(productId)).StockQty -= qty;

            if (increase)
                _context.Products.FirstOrDefault(b => b.Id.Equals(productId)).StockQty += qty;
        }

        public bool CanBeProductAddedToCart(int productStockQty)
        {
            switch (_context.EshopStatus)
            {
                case AppDbContext.EshopStatusEnum.Status1:
                    return productStockQty > 0;
                case AppDbContext.EshopStatusEnum.Status2:
                    return productStockQty > 2;
                case AppDbContext.EshopStatusEnum.Status3:
                    return true;
                default:
                    return false;
            }
        }

        public void UpdateBasketItemQuantity(BasketItem basketItem, int newQty)
        {
            var basket = _context.Baskets.FirstOrDefault(b => b.Id.Equals(basketItem.BasketId));
            if (newQty == 0)
            {
                basket.CartProducts.Remove(basketItem);
                UpdateProductStockQuantity(basketItem.ProductId, basketItem.ProductQty, true, false);
            }

            if (newQty > basketItem.ProductQty)
            {
                var oldBasketItemQty = basket.CartProducts.FirstOrDefault(c => c.Id.Equals(basketItem.Id)).ProductQty;
                basket.CartProducts.FirstOrDefault(c => c.Id.Equals(basketItem.Id)).ProductQty = newQty;
                UpdateProductStockQuantity(basketItem.ProductId, newQty - oldBasketItemQty, false, true);
            }

            if (newQty < basketItem.ProductQty)
            {
                var oldBasketItemQty = basket.CartProducts.FirstOrDefault(c => c.Id.Equals(basketItem.Id)).ProductQty;
                basket.CartProducts.FirstOrDefault(c => c.Id.Equals(basketItem.Id)).ProductQty = newQty;
                UpdateProductStockQuantity(basketItem.ProductId, oldBasketItemQty - newQty, true, false);
            }

            _context.SaveChanges();
        }

        public void RemoveBasketItem(Basket basket, BasketItem basketItem)
        {
            basket.CartProducts.Remove(basketItem);
            var contextBasket = _context.Baskets.FirstOrDefault(b => b.Id.Equals(basket.Id));
            contextBasket.CartProducts?.Remove(basketItem);

            if (contextBasket.CartProducts.Count == 0)
            {
                _context.Baskets.Remove(contextBasket);
            }

            UpdateProductStockQuantity(basketItem.ProductId, basketItem.ProductQty, true, false);

            _context.SaveChanges();
        }

        public IQueryable<Basket> GetBaskets()
        {
            return _context.Baskets;
        }

        public Basket GetBasketByID(Guid basketId)
        {
            return _context.Baskets.FirstOrDefault(b => b.Id.Equals(basketId));
        }

        public void DeleteBasket(Guid basketId)
        {
            var basket = _context.Baskets.FirstOrDefault(b => b.Id.Equals(basketId));

            if (basket != null) {

                _context.BasketItems.Where(b => b.BasketId.Equals(basketId)).ToList().Clear();
                _context.Baskets.Remove(basket);
                _context.SaveChanges();
            }
        }
    }
}
