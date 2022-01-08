using E_Shop.Helpers;
using E_Shop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop.Repositories
{
    public class ProductsRepo : IPrdctRepo
    {
        private readonly AppDbContext _context;

        public ProductsRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateProduct(Product product)
        {
            await _context.Products.AddAsync(product);
            await SaveAsync();
        }

        public async Task<Paging<Product>> GetProducts(ProductFilters productFilters)
        {
            var products = _context.Products.AsQueryable();

            if (productFilters.MinPrice != null)
                products = products.Where(p => p.Price >= productFilters.MinPrice);

            if (productFilters.MaxPrice != null)
                products = products.Where(p => p.Price <= productFilters.MaxPrice);

            if (productFilters.BrandID != null)
            {
                products = products.Where(p => p.BrandId.Equals(productFilters.BrandID));
            }

            if (productFilters.PageSize != null)
            {
                return await Paging<Product>.PagedList(products, 1, (int)productFilters.PageSize);
            }

            return await Paging<Product>.PagedList(products, 1, products.Count());
        }

        public async Task<Product> GetProductByID(Guid id)
        {
            return await _context.Products.FindAsync(id);
        }

        public bool CheckIfValidQuantity(Guid productId, int basketItemQty, int newQty)
        {
            return newQty >= 0 && (_context.Products.FirstOrDefault(p => p.Id.Equals(productId)).StockQty + basketItemQty) >= newQty;
        }

        public async Task DeleteProduct(Product product)
        {
            _context.Products.Remove(product);
            await SaveAsync();
        }

        public async Task UpdateProduct(Product product)
        {
            var oldProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id.Equals(product.Id));

            if (oldProduct != null)
            {
                oldProduct.Name = product.Name;
                oldProduct.BrandId = product.BrandId;
                oldProduct.Price = product.Price;
                oldProduct.StockQty = product.StockQty;
            }

            await SaveAsync();
        }

        public bool CheckIfBrandExists(Guid brandId)
        {
            return _context.Brands.Any(b => b.Id.Equals(brandId));
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
