using AutoMapper;
using E_Shop.Dtos;
using E_Shop.Helpers;
using E_Shop.Models;
using E_Shop.Repositories;
using E_Shop.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace E_Shop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IEshopSevice _eshopService;
        private readonly IMapper _mapper;

        public ProductsController(IEshopSevice eshopService, IMapper mapper)
        {
            _eshopService = eshopService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProductReadDto>> GetAllProducts([FromQuery] ProductFilters productFilters)
        {
            if (!productFilters.IsPriceValid())
            {
                return BadRequest("Invalid Price");
            }

            var products = _eshopService.Products.GetProducts(productFilters);

            return Ok(_mapper.Map<IEnumerable<ProductReadDto>>(products));
        }

        [HttpGet("{id}", Name = "GetProductById")]
        public ActionResult<ProductReadDto> GetProductById(Guid id)
        {
            var product = _eshopService.Products.GetProductByID(id);

            if (product == null)
                return NotFound();

            return Ok(_mapper.Map<ProductReadDto>(product));
        }

        [HttpPost]
        public ActionResult<ProductReadDto> CreateProduct(ProductCreateDto productCreateDto)
        {
            var product = _mapper.Map<Product>(productCreateDto);

            if (!_eshopService.Products.CheckIfBrandExists(product.BrandId))
                return NotFound("Brand does not exist");

            _eshopService.Products.CreateProduct(product);

            var productReadDto = _mapper.Map<ProductReadDto>(product);

            return CreatedAtRoute(nameof(GetProductById), new { id = productReadDto.Id }, productReadDto);
        }

        [HttpPut]
        public ActionResult<ProductReadDto> UpdateProduct(ProductReadDto productReadDto)
        {
            var productFromBody = _mapper.Map<Product>(productReadDto);

            if (_eshopService.Products.GetProductByID(productFromBody.Id) == null)
                return NotFound("Product Not Found");

            _eshopService.Products.UpdateProduct(productFromBody);
            return Ok(productFromBody);
        }

        [HttpDelete("{productId}")]
        public ActionResult DeleteProduct(Guid productId)
        {
            var product = _eshopService.Products.GetProductByID(productId);

            if (product == null)
            {
                return NotFound("Product does not exist");
            }

            _eshopService.Products.DeleteProduct(product);

            return Ok("Deleted Successfully");
        }
    }
}
