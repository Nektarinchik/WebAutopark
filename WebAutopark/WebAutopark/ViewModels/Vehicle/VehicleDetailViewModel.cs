using WebAutopark.DAL.Entities;

namespace WebAutopark.ViewModels.Vehicle
{
    public class VehicleDetailViewModel
    {
        public int VehicleId { get; set; }
        public double Weight { get; set; }
        public string? RegistrationNumber { get; set; }
        public string? Model { get; set; }
        public int Year { get; set; }
        public double Mileage { get; set; }
        public Colors Color { get; set; }
        public double FuelConsumption { get; set; }
        public VehicleTypes VehicleType { get; set; } = null!;
        public int Volume { get; set; }
        public double GetCalcTaxPerMonth()
        {
            return Weight * 0.0013 + VehicleType.TaxCoefficient * 30 + 5;
        }
        public double GetMaxKilometers()
        {
            if (FuelConsumption == 0)
            {
                return 0.0;
            }

            return (Volume / FuelConsumption) * 100.0;
        }

    }
}
