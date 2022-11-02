using WebAutopark.DAL.Entities;

namespace WebAutopark.ViewModels.Vehicle
{
    public class DetailViewModel
    {
        public VehicleTypes VehicleType { get; set; } = null!;
        public Vehicles Vehicle { get; set; } = null!;
    }
}
