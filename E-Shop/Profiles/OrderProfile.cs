using AutoMapper;
using E_Shop.Dtos;
using E_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            // Source -> Target
            CreateMap<Order, OrderReadDto>();
        }
    }
}
