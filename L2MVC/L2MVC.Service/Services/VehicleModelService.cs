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

        public async Task<IEnumerable<VehicleModel>> FindVehicleModelAsync(string sortOrder, string searchPhrase)
        {
            var query = from q in DatabaseContext.VehicleModels select q;
       
            if (!string.IsNullOrWhiteSpace(searchPhrase))
            {
                query = DatabaseContext.VehicleModels.Where(x => x.Name.Contains(searchPhrase) || x.Abrv.Contains(searchPhrase));
            }
            foreach (var item in query)
            {
                item.Make = await GetMakeAsync(item);
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

            return await query.ToArrayAsync();
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

        public async Task<VehicleMake> GetMakeAsync(string makerName)
        {
            var model = await DatabaseContext.VehicleMakes.Where(x => x.Name == makerName).FirstAsync();
            return model;
        }

        public async Task<VehicleMake> GetMakeAsync(VehicleModel model)
        {
            var make = await DatabaseContext.VehicleMakes.FindAsync(model.MakeId);
            
            if (make != null)
            {
                return make;
            }
            throw new Exception("not found!");
        }
    }
}
