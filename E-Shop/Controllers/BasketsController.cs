using AutoMapper;
using E_Shop.Dtos;
using E_Shop.Models;
using E_Shop.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketsController : ControllerBase
    {
        private readonly IEshopSevice _eshopService;
        private readonly IMapper _mapper;

        public BasketsController(IEshopSevice eshopService, IMapper mapper)
        {
            _eshopService = eshopService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<BasketReadDto>> GetAllBaskets()
        {
            var baskets = _eshopService.Baskets.GetBaskets();
            return Ok(_mapper.Map<IEnumerable<BasketReadDto>>(baskets));
        }

        [HttpGet("{customerId}", Name = "GetBasketProducts")]
        public ActionResult<IEnumerable<BasketItemsReadDto>> GetBasketProducts(Guid customerId)
        {
            var basket = _eshopService.Baskets.GetCustomerBasket(customerId);

            if (basket == null)
            {
                return NotFound("Basket Not Found");
            }

            return Ok(_mapper.Map<IEnumerable<BasketItemsReadDto>>(basket.CartProducts));
        }

        [HttpGet("CartProducts/{customerId}/{productId}", Name = "GetProductFromBasket")]
        public ActionResult<ProductReadDto> GetProductFromBasket(Guid customerId, Guid productId)
        {
            var basket = _eshopService.Baskets.GetCustomerBasket(customerId);

            if (basket == null)
            {
                return NotFound("Basket Not Found");
            }

            var product = _eshopService.Baskets.GetProductFromBasket(customerId, productId);
            if (product == null)
            {
                return NotFound("Product Not Found");
            }

            return Ok(_mapper.Map<ProductReadDto>(product));
        }

        [HttpPost("CartProducts/Add/{customerId}/{productId}", Name = "AddItemToBasket")]
        public ActionResult AddItemToBasket(Guid customerId, Guid productId)
        {
            if (customerId == Guid.Empty)
            {
                return NotFound("Customer Not Found");
            }

            var basketRepo = _eshopService.Baskets;
            var basket = basketRepo.GetCustomerBasket(customerId);

            if (basket == null)
            {
                basket = basketRepo.CreateBasket(customerId);
            }

            var cartProduct = basketRepo.GetProductFromBasket(customerId, productId);
            if (cartProduct == null)
            {
                var product = _eshopService.Products.GetProductByID(productId);
                if (product == null)
                {
                    return NotFound("Product Not Found");
                }

                if (basketRepo.CanBeProductAddedToCart(product.StockQty))
                {
                    basketRepo.AddProductToBasket(basket.Id, product);
                    var productReadDto = _mapper.Map<ProductReadDto>(product);
                    return CreatedAtRoute("GetProductFromBasket", new { customerId = customerId, productId = productId }, productReadDto);
                }
                else
                {
                    return BadRequest("Cannot be Added to Basket");
                }

            }
            else
            {
                return BadRequest("Product Already Exists in Basket");
            }
        }

        [HttpPut("CartProducts/UpdateQuantity/{customerId}/{basketItemId}/{newQty}", Name = "UpdateQuantity")]
        public ActionResult UpdateQuantity(Guid customerId, Guid basketItemId, int newQty)
        {
            var basket = _eshopService.Baskets.GetCustomerBasket(customerId);

            if (basket == null)
                return NotFound("Basket Not Found");

            var basketItem = basket.CartProducts.FirstOrDefault(b => b.Id.Equals(basketItemId));

            if (basketItem == null)
                return NotFound("Basket Item Not Found");

            if (_eshopService.Products.CheckIfValidQuantity(basketItem.ProductId, basketItem.ProductQty, newQty))
                _eshopService.Baskets.UpdateBasketItemQuantity(basketItem, newQty);

            return Ok($"Cart Item Quantity: {basketItem.ProductQty}");
        }

        [HttpDelete("CartProducts/Remove/{customerId}/{basketItemId}")]
        public ActionResult RemoveBasketItem(Guid customerId, Guid basketItemId)
        {
            var basket = _eshopService.Baskets.GetCustomerBasket(customerId);

            if (basket == null)
                return NotFound("Basket Not Found");

            var basketItem = basket.CartProducts.FirstOrDefault(b => b.Id.Equals(basketItemId));

            if (basketItem == null)
                return NotFound("Basket Item Not Found");

            _eshopService.Baskets.RemoveBasketItem(basket, basketItem);

            return Ok("Basket Item Deleted Successfully");
        }

        [HttpDelete("{basketId}")]
        public ActionResult DeleteBasket(Guid basketId)
        {
            _eshopService.Baskets.DeleteBasket(basketId);

            return Ok("Basket Deleted Successfully");
        }
    }
}
