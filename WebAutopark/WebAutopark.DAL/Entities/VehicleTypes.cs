using System.ComponentModel.DataAnnotations;


namespace WebAutopark.DAL.Entities
{
    public sealed class VehicleTypes
    {
        public int VehicleTypeId { get; set; }

        [Required]
        public string Name { get; set; } = null!;
        public double TaxCoefficient { get; set; }
        public VehicleTypes()
        { }
        public VehicleTypes(int vehicleTypeId, string name, double taxCoefficient = 1.0)
        {
            VehicleTypeId = vehicleTypeId;
            Name = name;
            TaxCoefficient = taxCoefficient;
        }
        public override string ToString() => $"{Name}, \"{TaxCoefficient}\"";

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            try
            {
                return Equals((VehicleTypes)obj);
            }
            catch (InvalidCastException)
            {
                return false;
            }
        }
        public bool Equals(VehicleTypes? other)
        {
            if (ReferenceEquals(null, other)) return false;

            if (ReferenceEquals(this, other)) return true;

            if (Name == null)
                return false;
            else
                if (Name.Equals(other.Name) && TaxCoefficient.Equals(other.TaxCoefficient))
                return true;

            return false;
        }
        public override int GetHashCode()
        {
            if (!ReferenceEquals(null, Name))
                return TaxCoefficient.GetHashCode() + Name.GetHashCode();

            return 0;
        }

    }
}
