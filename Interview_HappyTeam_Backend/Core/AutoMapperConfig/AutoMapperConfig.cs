using AutoMapper;
using Interview_HappyTeam_Backend.Core.DataTransferObject.Config;
using Interview_HappyTeam_Backend.Core.DataTransferObject.Order;
using Interview_HappyTeam_Backend.Core.Entities;

namespace Interview_HappyTeam_Backend.Core.AutoMapperConfig
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Order, OrderReadDTO>();
            CreateMap<Config, ConfigReadDTO>();
        }
    }
}
