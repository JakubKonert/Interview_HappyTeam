using AutoMapper;
using Interview_HappyTeam_Backend.Core.DataTransferObject.Car;
using Interview_HappyTeam_Backend.Core.DataTransferObject.CarModel;
using Interview_HappyTeam_Backend.Core.DataTransferObject.Client;
using Interview_HappyTeam_Backend.Core.DataTransferObject.Country;
using Interview_HappyTeam_Backend.Core.DataTransferObject.Location;
using Interview_HappyTeam_Backend.Core.DataTransferObject.Order;
using Interview_HappyTeam_Backend.Core.Entities;

namespace Interview_HappyTeam_Backend.Core.AutoMapperConfig
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<CountryCreateDTO, Country>();
            CreateMap<Country,CountryReadDTO>();

            CreateMap<LocationCreateDTO, Location>();
            CreateMap<Location, LocationReadDTO>();

            CreateMap<CarModelCreateDTO, CarModel>();
            CreateMap<CarModel, CarModelReadDTO>();

            CreateMap<CarCreateDTO, Car>();
            CreateMap<Car, CarReadDTO>()
                .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.CarModel.Model))
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.CarModel.Brand))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.CarModel.Price));

            CreateMap<ClientCreateDTO, Client>();
            CreateMap<Client, ClientReadDTO>();

            CreateMap<OrderCreateDTO, Order>();
            CreateMap<Order, OrderReadDTO>()
                .ForMember(dest => dest.LocationStartName, opt => opt.MapFrom(src => src.LocationStart.Name))
                .ForMember(dest => dest.LocationEndName, opt => opt.MapFrom(src => src.LocationEnd.Name))
                .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.Country.Name));
        }
    }
}
