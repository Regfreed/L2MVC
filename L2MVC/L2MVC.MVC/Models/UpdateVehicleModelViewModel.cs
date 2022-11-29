using Microsoft.AspNetCore.Mvc.Rendering;

namespace L2MVC.MVC.Models
{
    public class UpdateVehicleModelViewModel
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }   
        public Guid MakeId { get; set; }
        public string? MakeName { get; set; }
        public string? Abrv { get; set; }
        
        public List<SelectListItem>? MakerList { get; set; }
    }
}
