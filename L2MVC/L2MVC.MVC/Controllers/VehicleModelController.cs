using AutoMapper;
using L2MVC.MVC.Models;
using L2MVC.Service.Models;
using L2MVC.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace L2MVC.MVC.Controllers
{
    public class VehicleModelController : Controller
    {
        protected IVehicleModelService Service { get;  private set; }
        protected IMapper Mapper { get; private set; }

        public VehicleModelController(IVehicleModelService service, IMapper mapper)
        {
            Service = service;
            Mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await Service.FindVehicleModelAsync();
            var map = Mapper.Map<VehicleModelViewModel[]>(model);
            return View("Index", map);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return await Task.Run(() => View("Add"));
        }


        [HttpPut]
        public async Task<IActionResult> Update(UpdateVehicleModelViewModel model)
        {
            var entity = Mapper.Map<VehicleModel>(model);
            await Service.UpdateVehicleModelAsync(entity);
            return RedirectToAction("Index");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(UpdateVehicleModelViewModel model)
        {
            var result = await Service.DeleteVehicleModelAsync(model.Id);
            if (result) return RedirectToAction("Index");
            return View("Error");
        }
    }
}
