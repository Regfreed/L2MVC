using Azure;
using L2MVC.Service.Models;
using L2MVC.Service.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace L2MVC.Service.Services
{
    public class VehicleModelService : IVehicleModelService
    {
        protected DatabaseContext DatabaseContext { get; set; }

        public VehicleModelService(DatabaseContext databaseContext)
        {
            DatabaseContext = databaseContext;
        }

        public async Task<IPaginatedList<VehicleModel>> FindVehicleModelAsync(string sortOrder, string searchPhrase, int page, int pageSize)
        {
            var query = from q in DatabaseContext.VehicleModels.Include(x => x.Make) select q;
       
            if (!string.IsNullOrWhiteSpace(searchPhrase))
            {
                query = query.Where(x => x.Name.Contains(searchPhrase) || x.Abrv.Contains(searchPhrase));
            }

            switch (sortOrder)
            {
                case "make_desc":
                    query = query.OrderByDescending(x => x.Make.Name);
                    break;
                case "name":
                    query = query.OrderBy(x => x.Name);
                    break;
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
                    query = query.OrderBy(x => x.Make.Name);
                    break;
            }
            
            return await PaginatedList<VehicleModel>.CreateAsync(query, page, pageSize);
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

        public async Task<List<VehicleMake>> GetAllMakersAsync()
        {
            var makers = await DatabaseContext.VehicleMakes.ToListAsync();
            return makers;
        }
    }
}
