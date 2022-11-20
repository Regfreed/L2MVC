using Azure;
using L2MVC.Service.Models;
using L2MVC.Service.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2MVC.Service.Services
{
    public class VehicleMakeService : IVehicleMakeService
    {
        protected DatabaseContext DatabaseContext { get; private set; }

        public VehicleMakeService(DatabaseContext databaseContext)
        {
            DatabaseContext = databaseContext;
        }

        public async Task<IEnumerable<VehicleMake>> FindVehicleMakeAsync(string searchPhrase, int pageNumber, int pageSize, string sortBy, string sortOrder)
        {
            var query = DatabaseContext.VehicleMakes.Where(x => x.Name == searchPhrase).Skip((pageNumber - 1) * pageSize).Take(pageSize);

            //makni hardcodirano!!
            switch (sortOrder)
            {
                case "asc":
                    query = query.OrderBy(x => x.Name);
                    break;
                case "desc":
                    query = query.OrderByDescending(query => query.Name);
                    break;
                default:
                    throw new Exception("invalid sort order");
            }

            return await query.ToArrayAsync();
        }
        public async Task<IEnumerable<VehicleMake>> FindVehicleMakeAsync()
        {
            return await DatabaseContext.VehicleMakes.ToArrayAsync();
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
        public async Task<VehicleMake> InsertVehicleMakeAsync(VehicleMake vehicleMake)
        {
            await DatabaseContext.VehicleMakes.AddAsync(vehicleMake);
            await DatabaseContext.SaveChangesAsync();
            return vehicleMake;
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
        public async Task<VehicleMake> UpdateVehicleMakeAsync(VehicleMake vehicleMake)
        {
            DatabaseContext.VehicleMakes.Update(vehicleMake);
            await DatabaseContext.SaveChangesAsync();
            return vehicleMake;
        }
    }
}
