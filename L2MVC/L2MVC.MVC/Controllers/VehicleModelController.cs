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
            ViewData["MakeSortParm"] = String.IsNullOrWhiteSpace(sortOrder) ? "make_desc" : "";
            ViewData["NameSortParm"] = sortOrder == "name" ? "name_desc" : "name";
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
            int pageSize = 5;

            var result = await Service.FindVehicleModelAsync(sortOrder, searchPhrase, page ?? 1, pageSize);           
            var map = Mapper.Map<PaginatedList<VehicleModelViewModel>>(result);

            return View("Index", map);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {   
            var makers = await Service.GetAllMakersAsync();
            var model = new AddVehicleModelViewModel() { MakerList = new List<SelectListItem>()};

            foreach (var maker in makers)
            {
                model.MakerList.Add(new SelectListItem { Text = maker.Name, Value = maker.Id.ToString()});
            }

            return View(model);
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
            var vehicleModel = Mapper.Map<UpdateVehicleModelViewModel>(await Service.GetVehicleModelAsync(id));
            var makers = await Service.GetAllMakersAsync();
            vehicleModel.MakerList = new List<SelectListItem>();
            foreach (var maker in makers)
            {
                vehicleModel.MakerList.Add(new SelectListItem { Text = maker.Name, Value = maker.Id.ToString(), Selected = maker.Id == vehicleModel.MakeId });
            }
            return View(vehicleModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateVehicleModelViewModel model)
        {
            if (model.MakeId != Guid.Empty)
            {
                var entity = Mapper.Map<VehicleModel>(model);

                await Service.UpdateVehicleModelAsync(entity);
                return RedirectToAction("Index");
            }

            return await Task.Run(() => View("Update", model));
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
