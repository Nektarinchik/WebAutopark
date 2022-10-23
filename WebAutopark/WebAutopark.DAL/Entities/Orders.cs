using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAutopark.DAL.Entities
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
