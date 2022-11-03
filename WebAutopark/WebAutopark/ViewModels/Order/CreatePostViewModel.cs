using Microsoft.AspNetCore.Mvc;

namespace WebAutopark.ViewModels.Order
{
    public class CreatePostViewModel
    {
        public int VehicleId { get; set; }
        public int ComponentId { get; set; }
        public int Quantity { get; set; }

        [BindProperty]
        public DateTime Date { get; set; }
    }
}
