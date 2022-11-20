namespace L2MVC.Service.Models.Interfaces
{
    public interface IBaseModel
    {
        string Abrv { get; set; }
        Guid Id { get; set; }
        string Name { get; set; }
    }
}