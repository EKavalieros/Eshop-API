using E_Shop.Helpers;
using E_Shop.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop.Repositories
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
            }
        }

        private static void SeedData(AppDbContext context)
        {
            if (!context.Brands.Any())
            {
                context.Brands.AddRange(
                    new Brand() { Id = new Guid("e7173059-82b2-4557-925c-40f6164cff9e"), Name = "xiaomi" },
                    new Brand() { Id = new Guid("f409ca82-270d-4449-9e74-b27ebf2551ee"), Name = "huawei" },
                    new Brand() { Id = new Guid("8bb0c9c0-c826-4653-a604-10eef6df4f62"), Name = "apple" },
                    new Brand() { Id = new Guid("84b53e26-21bd-4074-970b-1fd969e75933"), Name = "samsung" }
                );
            }

            if (!context.Products.Any())
            {
                context.Products.AddRange(
                    new Product() { Id = new Guid("C66C3A30-D92A-44F6-86FE-28973C886580"), Name = "Xiaomi Poco X3", Price = 100, StockQty = 10, BrandId = new Guid("e7173059-82b2-4557-925c-40f6164cff9e") },
                    new Product() { Id = new Guid("83FF3747-3C3A-459F-9266-72B357A9E139"), Name = "Xiaomi Redmi Note 9 Pro", Price = 150, StockQty = 15, BrandId = new Guid("e7173059-82b2-4557-925c-40f6164cff9e") },
                    new Product() { Id = new Guid("01131EB0-50BD-4BEA-AC01-CC4AF10D3C76"), Name = "Xiaomi 11T", Price = 125, StockQty = 20, BrandId = new Guid("e7173059-82b2-4557-925c-40f6164cff9e") },
                    new Product() { Id = new Guid("17B14822-DF4F-4A96-A3F2-C750D3089A09"), Name = "Xiaomi 10T", Price = 213, StockQty = 5, BrandId = new Guid("e7173059-82b2-4557-925c-40f6164cff9e") },
                    new Product() { Id = new Guid("0B233791-50C7-445E-AD3C-58DF02FB51D7"), Name = "Huawei nova 9", Price = 214, StockQty = 6, BrandId = new Guid("f409ca82-270d-4449-9e74-b27ebf2551ee") },
                    new Product() { Id = new Guid("B2902F15-48BF-4C2B-BA14-EAAA8E071679"), Name = "Huawei p40 pro", Price = 215, StockQty = 12, BrandId = new Guid("f409ca82-270d-4449-9e74-b27ebf2551ee") },
                    new Product() { Id = new Guid("AAEAD16C-405F-4E46-8731-CF118C647704"), Name = "Huawei p30", Price = 216, StockQty = 50, BrandId = new Guid("f409ca82-270d-4449-9e74-b27ebf2551ee") },
                    new Product() { Id = new Guid("891DE3F2-0FE1-40CF-8B54-E41C644E1788"), Name = "Huawei y6p", Price = 217, StockQty = 4, BrandId = new Guid("f409ca82-270d-4449-9e74-b27ebf2551ee") },
                    new Product() { Id = new Guid("6DD466EA-95EA-4DA5-855C-B3C7965CB07B"), Name = "Huawei p smart", Price = 218, StockQty = 8, BrandId = new Guid("f409ca82-270d-4449-9e74-b27ebf2551ee") },
                    new Product() { Id = new Guid("B2471AF5-0617-4F63-826F-BFE7675076BF"), Name = "Apple Iphone 8", Price = 162, StockQty = 2, BrandId = new Guid("8bb0c9c0-c826-4653-a604-10eef6df4f62") },
                    new Product() { Id = new Guid("57E5CFBB-E67B-4549-B476-0EE7CAD8C6E7"), Name = "Apple Iphone XR", Price = 172, StockQty = 25, BrandId = new Guid("8bb0c9c0-c826-4653-a604-10eef6df4f62") },
                    new Product() { Id = new Guid("DDF65476-970F-433A-A933-AB348D2410AA"), Name = "Apple Iphone 12", Price = 164, StockQty = 9, BrandId = new Guid("8bb0c9c0-c826-4653-a604-10eef6df4f62") },
                    new Product() { Id = new Guid("32BC9950-5CE5-4D93-A222-FEA4F9F107F2"), Name = "Apple Iphone 13", Price = 364, StockQty = 0, BrandId = new Guid("8bb0c9c0-c826-4653-a604-10eef6df4f62") },
                    new Product() { Id = new Guid("F59DE6BE-5ECF-4BAE-A2D7-8049E7899702"), Name = "Samsung galaxy a52", Price = 364, StockQty = 4, BrandId = new Guid("84b53e26-21bd-4074-970b-1fd969e75933") },
                    new Product() { Id = new Guid("0B15E8E2-DA23-49BF-B7E2-E73CBF0DC4DB"), Name = "Samsung galaxy z", Price = 364, StockQty = 6, BrandId = new Guid("84b53e26-21bd-4074-970b-1fd969e75933") },
                    new Product() { Id = new Guid("FBF25CCF-454E-41D2-B3EF-4D384BC6BE2D"), Name = "Samsung galaxy s21", Price = 152, StockQty = 16, BrandId = new Guid("84b53e26-21bd-4074-970b-1fd969e75933") }
                );

            }

            if (!context.Customers.Any())
            {
                context.Customers.AddRange(
                    new Customer() { Id = new Guid("C9275F4E-2DA8-4825-8008-041684113B51"), Name = "John", Email = "tt@gmail.com" },
                    new Customer() { Id = new Guid("97DA867E-DDE1-4717-B4B8-FDBBA58841B1"), Name = "Nick", Email = "ll@gmail.com" },
                    new Customer() { Id = new Guid("994E8B64-FB77-4A43-B03B-177D8CA3167B"), Name = "Alex", Email = "kk@gmail.com" }
                );
            }

            if (!context.Baskets.Any())
            {
                context.Baskets.AddRange(
                    new Basket(new Guid("C9275F4E-2DA8-4825-8008-041684113B51")) { Id = new Guid("8151c718-4f4d-47c0-a592-ea953026c82f") }    
                );
            }

            if (!context.BasketItems.Any())
            {
                context.BasketItems.AddRange(
                    new BasketItem() { Id = new Guid("62913e9a-3026-452f-882e-ef5e3a82ae63"), BasketId = new Guid("8151c718-4f4d-47c0-a592-ea953026c82f"), ProductId = new Guid("C66C3A30-D92A-44F6-86FE-28973C886580"), ProductName = "Xiaomi Poco X3", ProductPrice = 100, ProductQty = 2 },
                    new BasketItem() { Id = new Guid("a750c448-740d-435c-bde8-7ef992709718"), BasketId = new Guid("8151c718-4f4d-47c0-a592-ea953026c82f"), ProductId = new Guid("83FF3747-3C3A-459F-9266-72B357A9E139"), ProductName = "Xiaomi Redmi Note 9 Pro", ProductPrice = 150, ProductQty = 3 }
                );
            }

            if (!context.Orders.Any())
            {
                context.Orders.AddRange(
                    new Order() { Id = new Guid("47017248-0bf8-429d-90b6-a225bead891e"), CustomerId = new Guid("C9275F4E-2DA8-4825-8008-041684113B51") }    
                );
            }

            if (!context.OrderDetails.Any())
            {
                context.OrderDetails.AddRange(
                    new OrderDetails() {Id = new Guid("e8039bc9-316c-4467-8da0-52f056bd9a14"), OrderId = new Guid("47017248-0bf8-429d-90b6-a225bead891e"), ProductId = new Guid("C66C3A30-D92A-44F6-86FE-28973C886580"), ProductName = "Xiaomi Poco X3", ProductPrice = 100 , ProductQty = 2 },    
                    new OrderDetails() {Id = new Guid("7c1fdaf8-1db9-4d81-829f-00729b777aa3"), OrderId = new Guid("47017248-0bf8-429d-90b6-a225bead891e"), ProductId = new Guid("83FF3747-3C3A-459F-9266-72B357A9E139"), ProductName = "Xiaomi Redmi Note 9 Pro", ProductPrice = 150 , ProductQty = 3 }   
                    
                );
            }

            context.SaveChanges();
        }
    }
}
