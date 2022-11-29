using L2MVC.Service.Models;

namespace L2MVC.Service.Services.Interfaces
{
    public interface IVehicleModelService
    {
        Task<bool> DeleteVehicleModelAsync(Guid id);
        Task<IEnumerable<VehicleModel>> FindVehicleModelAsync(string sortOrder, string searchPhrase);
        Task<VehicleModel> GetVehicleModelAsync(Guid id);
        Task<VehicleModel> InsertVehicleModelAsync(VehicleModel vehicleModel);
        Task<VehicleModel> UpdateVehicleModelAsync(VehicleModel vehicleModel);
        Task<List<VehicleMake>> GetAllMakersAsync();
        Task<VehicleMake> GetMakeAsync(string makerName);
        Task<VehicleMake> GetMakeAsync(VehicleModel model);
    }
}