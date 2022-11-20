namespace L2MVC.MVC.Models
{
    public class VehicleModelViewModel
    {
        public Guid Id { get; set; }
        public Guid MakeId { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
        public VehicleMakeViewModel Make { get; set; }
    }
}
