using L2MVC.Service.Models;
using L2MVC.Service.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2MVC.Service.Services
{
    public class VehicleModelService : IVehicleModelService
    {
        protected DatabaseContext DatabaseContext { get; set; }

        public VehicleModelService(DatabaseContext databaseContext)
        {
            DatabaseContext = databaseContext;
        }

        public async Task<IEnumerable<VehicleModel>> FindVehicleModelAsync(string searchPhrase, int pageNumber, int pageSize, string sortBy, string sortOrder)
        {
            var query = DatabaseContext.VehicleModels.Where(x => x.Name == searchPhrase).Skip((pageNumber - 1) * pageSize).Take(pageSize);

            switch (sortOrder)
            {
                case "asc":
                    query = query.OrderBy(x => x.Name);
                    break;
                case "desc":
                    query = query.OrderByDescending(x => x.Name);
                    break;
                default:
                    throw new Exception("invalid sort order");
            }

            return await query.ToArrayAsync();
        }

        public async Task<IEnumerable<VehicleModel>> FindVehicleModelAsync()
        {
            return await DatabaseContext.VehicleModels.ToArrayAsync();
        }

        public async Task<VehicleModel> GetVehicleModelAsync(Guid id)
        {
            var model = await DatabaseContext.VehicleModels.FindAsync(id);
            if (model != null)
            {
                return model;
            }
            throw new Exception("not found");
        }

        public async Task<VehicleModel> InsertVehicleModelAsync(VehicleModel vehicleModel)
        {
            await DatabaseContext.VehicleModels.AddAsync(vehicleModel);
            await DatabaseContext.SaveChangesAsync();
            return vehicleModel;
        }

        public async Task<Boolean> DeleteVehicleModelAsync(Guid id)
        {
            var model = await DatabaseContext.VehicleModels.FindAsync(id);
            if (model != null)
            {
                DatabaseContext.VehicleModels.Remove(model);
                await DatabaseContext.SaveChangesAsync();

                return true;
            }
            return false;
        }

        public async Task<VehicleModel> UpdateVehicleModelAsync(VehicleModel vehicleModel)
        {
            DatabaseContext.VehicleModels.Update(vehicleModel);
            await DatabaseContext.SaveChangesAsync();
            return vehicleModel;
        }
    }
}
