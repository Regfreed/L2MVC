using Microsoft.AspNetCore.Mvc.Rendering;

namespace L2MVC.MVC.Models
{
    public class AddVehicleModelViewModel
    {
        public string? Name { get; set; }
        public string? Abrv { get; set; }
        public Guid MakeId { get; set; }

        public List<SelectListItem>? MakerList { get; set; }
    }
}
