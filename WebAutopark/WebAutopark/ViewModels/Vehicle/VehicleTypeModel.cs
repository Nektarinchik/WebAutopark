namespace WebAutopark.ViewModels.Vehicle
{
    public class VehicleTypeModel
    {
        public int VehicleTypeId { get; set; }
        public string? Name { get; set; } //no need to initialize here
        public VehicleTypeModel()
        { }
        public VehicleTypeModel(int vehicleTypeId, string name)
        {
            VehicleTypeId = vehicleTypeId;
            Name = name;
        }
    }
}
