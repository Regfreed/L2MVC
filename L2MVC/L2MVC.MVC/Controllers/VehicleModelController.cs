using AutoMapper;
using L2MVC.MVC.Models;
using L2MVC.Service.Models;
using L2MVC.Service.Services;
using L2MVC.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            var model = await Service.FindVehicleModelAsync(sortOrder, searchPhrase);
            var map = Mapper.Map<IEnumerable<VehicleModelViewModel>>(model);

            return View("Index", PaginatedList<VehicleModelViewModel>.Create(map, page ?? 1, pageSize));
        }

        //[HttpGet]
        //public async Task<IActionResult> Index()
        //{
        //    var model = await Service.FindVehicleModelAsync();
        //    var map = Mapper.Map<VehicleModelViewModel[]>(model);
        //    return View("Index", map);
        //}

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var makers = Service.GetAllMakersAsync();
            if (makers == null)
            {
                throw new Exception("no makers");
            }
            else
            {
                var map = Mapper.Map<List<SelectListItem>>(makers);
                return View(map);
            }
            //return await Task.Run(() => View("Add"));
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddVehicleModelViewModel addVehicleModelRequest)
        {
            var model = Mapper.Map<VehicleModel>(addVehicleModelRequest);
            await Service.InsertVehicleModelAsync(model);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var vehicleModel = Mapper.Map<UpdateVehicleMakeViewModel>(await Service.GetVehicleModelAsync(id));
            return View(vehicleModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateVehicleModelViewModel model)
        {
            var entity = Mapper.Map<VehicleModel>(model);
            await Service.UpdateVehicleModelAsync(entity);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateVehicleModelViewModel model)
        {
            var result = await Service.DeleteVehicleModelAsync(model.Id);
            if (result) return RedirectToAction("Index");
            return View("Error");
        }
    }
}
