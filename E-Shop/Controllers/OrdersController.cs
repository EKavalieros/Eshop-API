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
    public class OrdersController : ControllerBase
    {
        private readonly IEshopSevice _eshopService;
        private readonly IMapper _mapper;

        public OrdersController(IEshopSevice eshopService, IMapper mapper)
        {
            _eshopService = eshopService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderReadDto>>> GetAllOrders()
        {
            var orders = await _eshopService.Orders.GetAllOrders();

            return Ok(_mapper.Map<IEnumerable<OrderReadDto>>(orders));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<OrderReadDto>>> GetOrderByID(Guid id)
        {
            var order = await _eshopService.Orders.GetOrderByID(id);

            if (order == null)
                return NotFound("Order Not Found");

            return Ok(_mapper.Map<OrderReadDto>(order));
        }

        [HttpPost("{basketId}", Name = "PlaceOrder")]
        public async Task<ActionResult<OrderReadDto>> PlaceOrder(Guid basketId)
        {
            if (basketId == Guid.Empty)
            {
                return NotFound("Basket Not Found");
            }

            var basket = await _eshopService.Baskets.GetBasketByID(basketId);

            if (basket == null)
            {
                return NotFound("Basket Not Found");
            }

            var order = await _eshopService.Orders.SubmitOrder(basket);

            return Ok(_mapper.Map<OrderReadDto>(order));
        }

        [HttpDelete("{orderId}")]
        public async Task<ActionResult> DeleteOrder(Guid orderId)
        {
            var order = await _eshopService.Orders.GetOrderByID(orderId);
            if (order == null)
                return NotFound("Order Not Found");

            await _eshopService.Orders.DeleteOrder(order);

            return Ok("Order Deleted Successfully");
        }
    }
}
