using WebAutopark.DAL.Entities;

namespace WebAutopark.ViewModels.Order
{
    public class IndexViewModel
    {
        public int OrderId { get; set; }
        public DateTime Date { get; set; }
        public VehicleViewModel Vehicle { get; set; } = null!;
        public Components? Component { get; set; } = null!;
        public int Quantity { get; set; }
    }
}
