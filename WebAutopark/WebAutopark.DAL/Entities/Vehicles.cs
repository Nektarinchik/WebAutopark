using System.ComponentModel.DataAnnotations;
using WebAutopark.DAL.Validation.Interfaces;
using WebAutopark.DAL.Validation.Entities;

namespace WebAutopark.DAL.Entities
{
    public sealed class Vehicles
    {
        public int VehicleId { get; set; }
        public int VehicleTypeId { get; set; }
        public double Weight { get; set; }

        [RegistrationNumberValidation(typeof(BelarusianNumberValidator), ErrorMessage = "Registration number must be 0000 XX-0 or X000XX-0")]
        public string? RegistrationNumber { get; set; }

        [Required(ErrorMessage = "Enter name of the model")]
        public string? Model { get; set; }

        [Required(ErrorMessage = "Enter manufacturer year")]
        [Range(1900, 2023, ErrorMessage = "Enter valid year")]
        public int Year { get; set; }
        public double Mileage { get; set; }
        public Colors Color { get; set; }
        public double FuelConsumption { get; set; }

        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Volume must be a positive number (> 0.1)")]
        public int Volume { get; set; }
        public Vehicles()
        { }

    }

}
