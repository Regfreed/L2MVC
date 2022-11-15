using L2MVC.Service.Models;

namespace L2MVC.Service.Services
{
    public interface IVehicleMakeService
    {
        Task<bool> DeleteVehicleMakeAsync(Guid id);
        Task<IEnumerable<VehicleMake>> FindVehicleMakeAsync(string searchPhrase, int pageNumber, int pageSize, string sortBy, string sortOrder);

        Task<IEnumerable<VehicleMake>> FindVehicleMakeAsync();

       Task<VehicleMake> GetVehicleMakeAsync(Guid id);
        Task<VehicleMake> InsertVehicleMakeAsync(VehicleMake vehicleMake);
        Task<VehicleMake> UpdateVehicleMakeAsync(VehicleMake vehicleMake);
    }
}