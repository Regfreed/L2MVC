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

        public async Task<IEnumerable<VehicleMake>> FindVehicleMakeAsync(string sortOrder, string searchPhrase, int pageNumber, int pageSize)
        {
            IQueryable<VehicleMake> query;
            if (!string.IsNullOrWhiteSpace(searchPhrase))
            {
                query = DatabaseContext.VehicleMakes.Where(x => x.Name.Contains(searchPhrase) || x.Abrv.Contains(searchPhrase)).Skip((pageNumber - 1) * pageSize).Take(pageSize);
            }
            else
            {
                query = DatabaseContext.VehicleMakes.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            }
            //if (!string.IsNullOrEmpty(searchphrase))
            //{
            //    query = query.where(q => q.name.contains(searchphrase)
            //                           || q.abrv.contains(searchphrase));
            //}
            //makni hardcodirano!!
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
