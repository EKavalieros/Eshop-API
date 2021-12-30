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
        IQueryable<Product> GetAllProducts();
        Paging<Product> GetProducts(ProductFilters productFilters);
        Product GetProductByID(Guid id);
        void CreateProduct(Product product);
        void DeleteProduct(Product product);
        void UpdateProduct(Product product);
        bool CheckIfValidQuantity(Guid productId, int basketItemQty, int newQty);
        bool CheckIfBrandExists(Guid brandId);
    }
}
