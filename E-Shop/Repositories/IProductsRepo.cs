using E_Shop.Helpers;
using E_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop.Repositories
{
    public interface IPrdctRepo
    {
        Task<Paging<Product>> GetProducts(ProductFilters productFilters);
        Task<Product> GetProductByID(Guid id);
        Task CreateProduct(Product product);
        Task DeleteProduct(Product product);
        Task UpdateProduct(Product product);
        bool CheckIfValidQuantity(Guid productId, int basketItemQty, int newQty);
        bool CheckIfBrandExists(Guid brandId);
    }
}
