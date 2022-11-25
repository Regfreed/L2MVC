using AutoMapper;
using L2MVC.MVC.Models;
using L2MVC.Service.Models;
using L2MVC.Service.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace L2MVC.MVC.Mapping
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<AddVehicleMakeViewModel, VehicleMake>().ReverseMap();
            CreateMap<UpdateVehicleMakeViewModel, VehicleMake>().ReverseMap();
            CreateMap<VehicleMakeViewModel, VehicleMake>().ReverseMap();
            CreateMap<VehicleMakeViewModel, UpdateVehicleMakeViewModel>().ReverseMap();
            //CreateMap<VehicleMakeViewModel[], VehicleMake>().ReverseMap();
            //CreateMap<VehicleMakeViewModel, VehicleMake[]>().ReverseMap();
            CreateMap<VehicleMake, IEnumerable<VehicleMakeViewModel[]>>().ReverseMap();

            CreateMap<AddVehicleModelViewModel, VehicleModel>().ReverseMap();
            CreateMap<UpdateVehicleModelViewModel, VehicleModel>().ReverseMap();
            CreateMap<VehicleModelViewModel, VehicleModel>().ReverseMap();
            CreateMap<VehicleModelViewModel, UpdateVehicleModelViewModel>().ReverseMap();
            CreateMap<VehicleModel, IEnumerable<VehicleModelViewModel[]>>().ReverseMap();
            CreateMap<SelectListItem, VehicleMake>().ReverseMap();

        }
    }
}
