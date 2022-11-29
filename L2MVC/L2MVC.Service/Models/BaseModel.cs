using L2MVC.Service.Models.Interfaces;

namespace L2MVC.Service.Models
{
    public class BaseModel : IBaseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
    }
}
