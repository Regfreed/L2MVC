using L2MVC.Service.Models;
using L2MVC.Service.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace L2MVC.Service.Services
{
    public class VehicleMakeService : IVehicleMakeService
    {
        protected DatabaseContext DatabaseContext { get; private set; }
        
        public VehicleMakeService(DatabaseContext databaseContext)
        {
            DatabaseContext = databaseContext;
        }

        public async Task<IPaginatedList<VehicleMake>> FindVehicleMakeAsync(string sortOrder, string searchPhrase, int page, int pageSize)
        {
            var query = from q in DatabaseContext.VehicleMakes select q;
            if (!string.IsNullOrWhiteSpace(searchPhrase))
            {
                query = DatabaseContext.VehicleMakes.Where(x => x.Name.Contains(searchPhrase) || x.Abrv.Contains(searchPhrase));
            }
            
            switch(sortOrder)
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

            return await PaginatedList<VehicleMake>.CreateAsync(query,page,pageSize);
        }

        public async Task<VehicleMake> GetVehicleMakeAsync(Guid id)
        {
            var maker = await DatabaseContext.VehicleMakes.FindAsync(id);
            if (maker != null)
            {
                return maker;
            }
            throw new Exception("not found!");
        }

        public async Task<Boolean> InsertVehicleMakeAsync(VehicleMake vehicleMake)
        {
            if (DatabaseContext.VehicleMakes.FirstOrDefault(x => x.Name == vehicleMake.Name) == null)
            {
                await DatabaseContext.VehicleMakes.AddAsync(vehicleMake);
                await DatabaseContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<Boolean> DeleteVehicleMakeAsync(Guid id)
        {
            var maker = await DatabaseContext.VehicleMakes.FindAsync(id);

            if (maker != null)
            {
                DatabaseContext.VehicleMakes.Remove(maker);
                await DatabaseContext.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<Boolean> UpdateVehicleMakeAsync(VehicleMake vehicleMake)
        {
            if (DatabaseContext.VehicleMakes.FirstOrDefault(x => x.Name == vehicleMake.Name) == null)
            {
                DatabaseContext.VehicleMakes.Update(vehicleMake);
                await DatabaseContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
