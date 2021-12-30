using AutoMapper;
using E_Shop.Dtos;
using E_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop.Profiles
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            // Source -> Target
            CreateMap<Basket, BasketReadDto>();
            CreateMap<BasketReadDto, Basket>();
            CreateMap<BasketItem, BasketItemsReadDto>();
        }
    }
}
