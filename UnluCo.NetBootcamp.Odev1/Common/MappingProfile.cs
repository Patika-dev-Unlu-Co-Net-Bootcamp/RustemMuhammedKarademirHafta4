using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnluCo.NetBootcamp.Odev4.Entities.Concrete;
using UnluCo.NetBootcamp.Odev4.Application.BrandOperations.Queries.GetBrandById;
using UnluCo.NetBootcamp.Odev4.Application.BrandOperations.Queries.GetBrands;
using static UnluCo.NetBootcamp.Odev4.Application.CarOperations.Commands.CreateCar.CreateCarCommand;
using static UnluCo.NetBootcamp.Odev4.Application.BrandOperations.Commands.CreateBrand.CreateBrandCommand;
using UnluCo.NetBootcamp.Odev4.Application.CarOperations.Queries.GetCarById;
using UnluCo.NetBootcamp.Odev4.Extensions;
using UnluCo.NetBootcamp.Odev4.Application.CarOperations.Queries.GetCars;

namespace UnluCo.NetBootcamp.Odev4.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBrandModel, Brand>();//CreateBrandModel nesnesindeki bilgileri Brand nesnesi ile esler.
            CreateMap<Brand, BrandsViewModel>().ForMember(dest => dest.BrandNameShortening,//BrandsViewModel nesnesindeki BrandNameShortening alanini verilen sarta gore esler.
                opt => opt.MapFrom(src => src.BrandName.GetBrandNameShortening()));
            CreateMap<Brand, BrandIdViewModel>().ForMember(dest => dest.BrandNameShortening,
                opt => opt.MapFrom(src => src.BrandName.GetBrandNameShortening()));
            CreateMap<CreateCarModel, Car>();
            CreateMap<Car, CarsViewModel>().ForMember(dest => dest.BrandName,
              opt => opt.MapFrom(src => src.Brand.BrandName));//Brands listesi ile eslestirip BrandId ye karsilik gelen BrandName alir
            CreateMap<Car, CarIdViewModel>().ForMember(dest => dest.BrandName,
              opt => opt.MapFrom(src => src.Brand.BrandName));
        }

    }
}
