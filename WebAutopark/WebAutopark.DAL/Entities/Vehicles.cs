using System.ComponentModel.DataAnnotations;
using WebAutopark.DAL.Validation.Interfaces;
using WebAutopark.DAL.Validation.Entities;

namespace WebAutopark.DAL.Entities
{
    public sealed class Vehicles : IEquatable<Vehicles>
    {
        public int VehicleId { get; set; }
        public int VehicleTypeId { get; set; }
        public double Weight { get; set; }

        [RegistrationNumberValidation(typeof(BelarusianNumberValidator), ErrorMessage = "Registration number must be 0000 XX-0 or X000XX-0")]
        public string? RegistrationNumber { get; set; }

        [Required(ErrorMessage = "Enter name of the model")]
        public string Model { get; set; } = null!;

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
        public Vehicles(int vehicleId, int vehicleTypeId, string model,
            string registrationNumber, double weight, int year,
            double mileage, Colors color, double fuelConsumption)
        {
            VehicleId = vehicleId;
            VehicleTypeId = vehicleTypeId;
            Model = model;
            RegistrationNumber = registrationNumber;
            Weight = weight;
            Year = year;
            Mileage = mileage;
            Color = color;
            FuelConsumption = fuelConsumption;
        }
        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;

            try
            {
                return Equals((Vehicles)obj);
            }
            catch (InvalidCastException)
            {
                return false;
            }
        }
        public bool Equals(Vehicles? other)
        {
            if (other == null)
            {
                return false;
            }

            if (other == this)
            {
                return true;
            }

            if (Model == null)
            {
                return false;
            }
            else
            {
                if (Model.Equals(other.Model) && VehicleTypeId.Equals(other.VehicleTypeId))
                {
                    return true;
                }
            }

            return false;
        }

        public override int GetHashCode()
        {
            if (!ReferenceEquals(null, Model))
            {
                int hashCode = Model.GetHashCode() + VehicleTypeId.GetHashCode();
                return hashCode;
            }

            return 0;
        }

    }

}
