namespace WebAutopark.OrdersLogic
{
    public sealed class Orders
    {
        public int OrderId { get; private set; }
        public int VehicleId { get; private set; }
        public DateTime Date { get; private set; }
        public Orders(int orderId, int vehicleId, DateTime date)
        {
            OrderId = orderId;
            VehicleId = vehicleId;
            Date = date;
        }
    }
}
