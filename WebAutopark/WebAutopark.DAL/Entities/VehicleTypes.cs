using System.ComponentModel.DataAnnotations;


namespace WebAutopark.DAL.Entities
{
    public sealed class VehicleTypes
    {
        public int VehicleTypeId { get; set; }

        [Required]
        public string? Name { get; set; }
        public double TaxCoefficient { get; set; }
        public VehicleTypes()
        { }
    }
}
