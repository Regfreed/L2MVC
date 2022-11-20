using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
