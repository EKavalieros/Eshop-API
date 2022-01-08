using E_Shop.Models;
using Microsoft.EntityFrameworkCore;
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

        public async Task<Basket> GetCustomerBasket(Guid customerId)
        {
            return await _context.Baskets.FirstOrDefaultAsync(b=>b.CustomerId.Equals(customerId));
        }

        public async Task<Product> GetProductFromBasket(Guid customerId, Guid productId)
        {
            var basketItem = _context.Baskets.FirstOrDefault(b => b.CustomerId.Equals(customerId))?.CartProducts?.FirstOrDefault(p => p.ProductId.Equals(productId));

            return (basketItem == null) ? null : await _context.Products.FirstOrDefaultAsync(p => p.Id.Equals(basketItem.ProductId));
        }

        public async Task<Basket> CreateBasket(Guid customerId)
        {
            var basket = new Basket(customerId) { Id = new Guid() };
            await _context.Baskets.AddAsync(basket);
            await SaveAsync();

            return basket;
        }

        public async Task AddProductToBasket(Guid basketId, Product product)
        {
            _context.Baskets.FirstOrDefault(b => b.Id.Equals(basketId))?.AddProduct(product);
            await UpdateProductStockQuantity(product.Id, 1, false, true);
            await SaveAsync();
        }

        private async Task UpdateProductStockQuantity(Guid productId, int qty, bool increase, bool decrease)
        {
            var product = await _context.Products.FirstOrDefaultAsync(b => b.Id.Equals(productId));
            if (decrease)
                product.StockQty -= qty;

            if (increase)
                product.StockQty += qty;
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

        public async Task UpdateBasketItemQuantity(BasketItem basketItem, int newQty)
        {
            var basket = await _context.Baskets.FirstOrDefaultAsync(b => b.Id.Equals(basketItem.BasketId));
            if (newQty == 0)
            {
                basket.CartProducts.Remove(basketItem);
                await UpdateProductStockQuantity(basketItem.ProductId, basketItem.ProductQty, true, false);
            }

            if (newQty > basketItem.ProductQty)
            {
                var oldBasketItemQty = basket.CartProducts.FirstOrDefault(c => c.Id.Equals(basketItem.Id)).ProductQty;
                basket.CartProducts.FirstOrDefault(c => c.Id.Equals(basketItem.Id)).ProductQty = newQty;
                await UpdateProductStockQuantity(basketItem.ProductId, newQty - oldBasketItemQty, false, true);
            }

            if (newQty < basketItem.ProductQty)
            {
                var oldBasketItemQty = basket.CartProducts.FirstOrDefault(c => c.Id.Equals(basketItem.Id)).ProductQty;
                basket.CartProducts.FirstOrDefault(c => c.Id.Equals(basketItem.Id)).ProductQty = newQty;
                await UpdateProductStockQuantity(basketItem.ProductId, oldBasketItemQty - newQty, true, false);
            }

            await SaveAsync();
        }

        public async Task RemoveBasketItem(Basket basket, BasketItem basketItem)
        {
            basket.CartProducts.Remove(basketItem);
            var contextBasket = await _context.Baskets.FirstOrDefaultAsync(b => b.Id.Equals(basket.Id));
            contextBasket.CartProducts?.Remove(basketItem);

            if (contextBasket.CartProducts.Count == 0)
            {
                _context.Baskets.Remove(contextBasket);
            }

            await UpdateProductStockQuantity (basketItem.ProductId, basketItem.ProductQty, true, false);

            await SaveAsync();
        }

        public async Task<List<Basket>> GetBaskets()
        {
            return await _context.Baskets.ToListAsync();
        }

        public async Task<Basket> GetBasketByID(Guid basketId)
        {
            return await _context.Baskets.FirstOrDefaultAsync(b => b.Id.Equals(basketId));
        }

        public async Task DeleteBasket(Guid basketId)
        {
            var basket = await _context.Baskets.FirstOrDefaultAsync(b => b.Id.Equals(basketId));

            if (basket != null)
            {
                _context.BasketItems.Where(b => b.BasketId.Equals(basketId)).ToList().Clear();
                _context.Baskets.Remove(basket);
                await SaveAsync();
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}