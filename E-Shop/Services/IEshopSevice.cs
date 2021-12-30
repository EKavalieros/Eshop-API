using E_Shop.Helpers;
using E_Shop.Models;
using E_Shop.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop.Services
{
    public interface IEshopSevice
    {
        IPrdctRepo Products { get; }
        IBasketRepo Baskets { get; }
        IOrdersRepo Orders { get; }
    }
}
