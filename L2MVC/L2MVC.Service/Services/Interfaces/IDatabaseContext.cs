using L2MVC.Service.Models;
using Microsoft.EntityFrameworkCore;

namespace L2MVC.Service.Services.Interfaces
{
    public interface IDatabaseContext
    {
        DbSet<VehicleMake> VehicleMakes { get; set; }
        DbSet<VehicleModel> VehicleModels { get; set; }
    }
}