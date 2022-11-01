namespace WebAutopark.ViewModels.Vehicle
{
    public class VehicleTypeModel
    {
        public int VehicleTypeId { get; set; }
        public string Name { get; set; } = null!;
        public VehicleTypeModel()
        { }
        public VehicleTypeModel(int vehicleTypeId, string name)
        {
            VehicleTypeId = vehicleTypeId;
            Name = name;
        }
    }
}
