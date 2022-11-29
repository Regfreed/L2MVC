using AutoMapper;
using L2MVC.MVC.Models;
using L2MVC.Service.Models;

namespace L2MVC.MVC.Mapping
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<AddVehicleMakeViewModel, VehicleMake>().ReverseMap();
            CreateMap<UpdateVehicleMakeViewModel, VehicleMake>().ReverseMap();
            CreateMap<VehicleMakeViewModel, VehicleMake>().ReverseMap();
   
            CreateMap<AddVehicleModelViewModel, VehicleModel>().ReverseMap();
            CreateMap<UpdateVehicleModelViewModel, VehicleModel>().ReverseMap();
            CreateMap<VehicleModelViewModel, VehicleModel>().ReverseMap();
            CreateMap<AddVehicleModelViewModel, VehicleMake>().ReverseMap();
        }
    }
}
