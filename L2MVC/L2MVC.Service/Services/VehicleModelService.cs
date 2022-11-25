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

        public async Task<IEnumerable<VehicleModel>> FindVehicleModelAsync(string sortOrder, string searchPhrase)//, int pageNumber, int pageSize, string sortBy )
        {
            var query = from q in DatabaseContext.VehicleModels select q;
            if (!string.IsNullOrWhiteSpace(searchPhrase))
            {
                query = DatabaseContext.VehicleModels.Where(x => x.Name.Contains(searchPhrase) || x.Abrv.Contains(searchPhrase));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    query = query.OrderByDescending(x => x.Name);
                    break;
                case "abrv_desc":
                    query = query.OrderByDescending(x => x.Abrv);
                    break;
                case "Abrv":
                    query = query.OrderBy(x => x.Abrv);
                    break;
                default:
                    query = query.OrderBy(x => x.Name);
                    break;
            }
            await query.AnyAsync();
            return query;
        }
        //public async Task<IEnumerable<VehicleModel>> FindVehicleModelAsync()
        //{
        //    return await DatabaseContext.VehicleModels.ToArrayAsync();
        //}
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

        public async Task<List<VehicleMake>> GetAllMakersAsync()
        {
            var makers = await DatabaseContext.VehicleMakes.ToListAsync();
            return makers;
        }
    }
}
