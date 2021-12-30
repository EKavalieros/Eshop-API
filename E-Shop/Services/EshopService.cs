using E_Shop.Helpers;
using E_Shop.Models;
using E_Shop.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop.Services
{
    public class EshopService : IEshopSevice
    {
        private AppDbContext _context;
        private IBasketRepo _basketRepo;
        private IPrdctRepo _productRepo;
        private IOrdersRepo _ordersRepo;

        public IBasketRepo Baskets
        {
            get
            {
                if (_basketRepo == null)
                {
                    _basketRepo = new BasketRepo(_context);
                }

                return _basketRepo;
            }
        }

        public IPrdctRepo Products
        {
            get
            {
                if (_productRepo == null)
                {
                    _productRepo = new ProductsRepo(_context);
                }

                return _productRepo;
            }
        }

        public IOrdersRepo Orders
        {
            get
            {
                if (_ordersRepo == null)
                {
                    _ordersRepo = new OrdersRepo(_context);
                }

                return _ordersRepo;
            }
        }

        public EshopService(AppDbContext context)
        {
            _context = context;
            _context.EshopStatus = AppDbContext.EshopStatusEnum.Status3;
        }
    }
}
