using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L2MVC.Service.Models
{
    public class VehicleModel : BaseModel
    {
        [ForeignKey("MakeId")]
        public int MakeId { get; set; }
        public VehicleMake Make { get; set; }
    }
}
