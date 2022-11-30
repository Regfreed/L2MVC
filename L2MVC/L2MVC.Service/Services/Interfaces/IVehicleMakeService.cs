using L2MVC.Service.Models;

namespace L2MVC.Service.Services.Interfaces
{
    public interface IVehicleMakeService
    {
        Task<bool> DeleteVehicleMakeAsync(Guid id);
        Task<IPaginatedList<VehicleMake>> FindVehicleMakeAsync(string sortOrder, string searchPhrase, int page, int pageSize);
        Task<VehicleMake> GetVehicleMakeAsync(Guid id);
        Task<Boolean> InsertVehicleMakeAsync(VehicleMake vehicleMake);
        Task<Boolean> UpdateVehicleMakeAsync(VehicleMake vehicleMake);
    }
}