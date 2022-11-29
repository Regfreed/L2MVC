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

        public async Task<IEnumerable<VehicleMake>> FindVehicleMakeAsync(string sortOrder, string searchPhrase)
        {
            var query = from q in DatabaseContext.VehicleMakes select q;
            if (!string.IsNullOrWhiteSpace(searchPhrase))
            {
                query = DatabaseContext.VehicleMakes.Where(x => x.Name.Contains(searchPhrase) || x.Abrv.Contains(searchPhrase));//.Skip((pageNumber - 1) * pageSize).Take(pageSize);
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

            return await query.ToArrayAsync();
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
            if (DatabaseContext.VehicleMakes.Where(x => x.Name == vehicleMake.Name).IsNullOrEmpty())
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
            if (DatabaseContext.VehicleMakes.Where(x => x.Name == vehicleMake.Name).IsNullOrEmpty())
            {
                DatabaseContext.VehicleMakes.Update(vehicleMake);
                await DatabaseContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
