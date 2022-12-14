using AutoMapper;
using L2MVC.MVC.Models;
using L2MVC.Service.Models;
using L2MVC.Service.Services;
using L2MVC.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


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
        }

        [HttpGet]
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchPhrase, int? page)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrWhiteSpace(sortOrder) ? "name_desc" : "";
            ViewData["AbrvSortParm"] = sortOrder == "abrv" ? "abrv_desc" : "abrv";

            if (searchPhrase != null)
            {
                page = 1;
            }
            else
            {
                searchPhrase = currentFilter;
            }

            ViewData["CurrentFilter"] = searchPhrase;
            int pageSize = 3;
            var result = await Service.FindVehicleMakeAsync(sortOrder, searchPhrase, page ?? 1 , pageSize);
            var map = Mapper.Map<PaginatedList<VehicleMakeViewModel>>(result);

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
            
            if(await Service.InsertVehicleMakeAsync(maker))
            {
                return RedirectToAction("Index");
            }
            
            return View("Add", addVehicleMakeRequest);     
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
            if(await Service.UpdateVehicleMakeAsync(entity)) return RedirectToAction("Index");
            return View("Update", model);
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
