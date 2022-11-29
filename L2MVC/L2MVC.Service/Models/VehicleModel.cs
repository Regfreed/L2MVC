using System.ComponentModel.DataAnnotations.Schema;
using L2MVC.Service.Models.Interfaces;

namespace L2MVC.Service.Models
{
    public class VehicleModel : BaseModel, IVehicleModel
    {
        
        [ForeignKey("MakeId")]
        public Guid MakeId { get; set; }
        public VehicleMake Make { get; set; }
    }
}
