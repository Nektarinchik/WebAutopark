using WebAutopark.DAL.Entities;

namespace WebAutopark.ViewModels.Order
{
    public class DetailViewModel
    {
        public VehicleViewModel Vehicle { get; set; } = null!;
        public Components? Component { get; set; } = null!;
        public DateTime Date { get; set; }
        public int? Quantity { get; set; }
    }
}
