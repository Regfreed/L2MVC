using AutoMapper;
using L2MVC.MVC.Models;
using L2MVC.Service.Models;
using L2MVC.Service.Services;
using L2MVC.Service.Services.Interfaces;

namespace L2MVC.MVC.Mapping
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<AddVehicleMakeViewModel, VehicleMake>().ReverseMap();
            CreateMap<UpdateVehicleMakeViewModel, VehicleMake>().ReverseMap();
            CreateMap<VehicleMakeViewModel, VehicleMake>().ReverseMap();
            CreateMap<IPaginatedList<VehicleMake>, PaginatedList<VehicleMakeViewModel>>().ForMember("Item", opt => opt.Ignore()).ConstructUsing((source, contex) =>
            {
                var list = new List<VehicleMakeViewModel>();
                foreach (var item in source)
                {
                    var destinationItem = contex.Mapper.Map<VehicleMakeViewModel>(item);
                    list.Add(destinationItem);
                }
                return new PaginatedList<VehicleMakeViewModel>(list, source.TotalItems, source.PageIndex, source.PageSize);
            });

            CreateMap<AddVehicleModelViewModel, VehicleModel>().ReverseMap();
            CreateMap<UpdateVehicleModelViewModel, VehicleModel>().ReverseMap();
            CreateMap<VehicleModelViewModel, VehicleModel>().ReverseMap();
            CreateMap<AddVehicleModelViewModel, VehicleMake>().ReverseMap();

            CreateMap<IPaginatedList<VehicleModel>, PaginatedList<VehicleModelViewModel>>().ForMember("Item", opt => opt.Ignore()).ConstructUsing((source, contex) => 
            { 
                var list = new List<VehicleModelViewModel>();
                foreach(var item in source)
                {    
                    var destinationItem = contex.Mapper.Map<VehicleModelViewModel>(item);
                    list.Add(destinationItem);
                }
                return new PaginatedList<VehicleModelViewModel>(list, source.TotalItems, source.PageIndex, source.PageSize);
            });
        }
    }
}
