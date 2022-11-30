using L2MVC.Service.Models;

namespace L2MVC.Service.Services.Interfaces
{
    public interface IVehicleModelService
    {
        Task<bool> DeleteVehicleModelAsync(Guid id);
        Task<IPaginatedList<VehicleModel>> FindVehicleModelAsync(string sortOrder, string searchPhrase, int page, int pageSize);
        Task<VehicleModel> GetVehicleModelAsync(Guid id);
        Task<VehicleModel> InsertVehicleModelAsync(VehicleModel vehicleModel);
        Task<VehicleModel> UpdateVehicleModelAsync(VehicleModel vehicleModel);
        Task<List<VehicleMake>> GetAllMakersAsync();

    }
}