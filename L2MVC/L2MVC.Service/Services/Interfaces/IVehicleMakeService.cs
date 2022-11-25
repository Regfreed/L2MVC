using L2MVC.Service.Models;

namespace L2MVC.Service.Services.Interfaces
{
    public interface IVehicleMakeService
    {
        Task<bool> DeleteVehicleMakeAsync(Guid id);
        Task<IEnumerable<VehicleMake>> FindVehicleMakeAsync(string sortOrder, string searchPhrase);
        //Task<IEnumerable<VehicleMake>> FindVehicleMakeAsync();
        Task<VehicleMake> GetVehicleMakeAsync(Guid id);
        Task<VehicleMake> InsertVehicleMakeAsync(VehicleMake vehicleMake);
        Task<VehicleMake> UpdateVehicleMakeAsync(VehicleMake vehicleMake);
    }
}