using L2MVC.Service.Models;

namespace L2MVC.Service.Services.Interfaces
{
    public interface IVehicleModelService
    {
        Task<bool> DeleteVehicleModelAsync(Guid id);
        //Task<IEnumerable<VehicleModel>> FindVehicleModelAsync();
        Task<IEnumerable<VehicleModel>> FindVehicleModelAsync(string sortOrder, string searchPhrase);//, int pageNumber, int pageSize, string sortBy, );
        Task<VehicleModel> GetVehicleModelAsync(Guid id);
        Task<VehicleModel> InsertVehicleModelAsync(VehicleModel vehicleModel);
        Task<VehicleModel> UpdateVehicleModelAsync(VehicleModel vehicleModel);
        Task<List<VehicleMake>> GetAllMakersAsync();
    }
}