using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAutopark.DAL.Entities
{
    public sealed class Vehicles : IEquatable<Vehicles>
    {
        public int VehicleId { get; set; }
        public int VehicleTypeId { get; set; }
        public double Weight { get; set; }
        public string? RegistrationNumber { get; set; }
        public string Model { get; set; } = null!;
        public int Year { get; set; }
        public double Mileage { get; set; }
        public Colors Color { get; set; }
        public double FuelConsumption { get; set; }
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
