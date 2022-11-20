﻿using AutoMapper;
using L2MVC.MVC.Mapping;
using L2MVC.MVC.Models;
using L2MVC.Service.Models;
using L2MVC.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L2MVC.MVC.Controllers
{
    public class VehicleMakeController : Controller
    {
        protected IVehicleMakeService Service { get; private set; }
        protected IMapper Mapper { get; private set; }

        public VehicleMakeController(IVehicleMakeService vehicleMakeService, IMapper mapper)
        {
            Service = vehicleMakeService;
            Mapper = mapper;
            //this.databaseContext = databaseContext;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await Service.FindVehicleMakeAsync();
            var map = Mapper.Map<VehicleMakeViewModel[]>(model);
            return View("Index", map);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return await Task.Run(() => View("Add"));
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddVehicleMakeViewModel addVehicleMakeRequest)
        {
            var maker = Mapper.Map<VehicleMake>(addVehicleMakeRequest);
            //var maker = new VehicleMake()
            //{
            //    Id = Guid.NewGuid(),
            //    Name = addVehicleMakeRequest.Name,
            //    Abrv = addVehicleMakeRequest.Abrv
            //};
            await Service.InsertVehicleMakeAsync(maker);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var vehicleMake = Mapper.Map<UpdateVehicleMakeViewModel>(await Service.GetVehicleMakeAsync(id));
            return View(vehicleMake);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateVehicleMakeViewModel model)
        {
            var entity = Mapper.Map<VehicleMake>(model);
            await Service.UpdateVehicleMakeAsync(entity);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await Service.DeleteVehicleMakeAsync(id);
            if (result) return RedirectToAction("Index");

            return View("Error");
        }
    }
}
