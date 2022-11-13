using System.Collections.Generic;
using WebAutopark.DAL.Entities;

namespace WebAutopark.ViewModels.Vehicle
{
    public class IndexViewModel
    {
        public IEnumerable<Vehicles> Vehicles { get; set; } = null!;
        public IEnumerable<VehicleTypes> VehicleTypes { get; set; } = null!;
    }
}
