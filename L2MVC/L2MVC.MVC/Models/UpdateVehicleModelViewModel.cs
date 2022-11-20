using L2MVC.Service.Models;

namespace L2MVC.MVC.Models
{
    public class UpdateVehicleModelViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }   
        public Guid MakerId { get; set; }
        public string Abrv { get; set; }
        public VehicleMake Make { get; set; }   
    }
}
