using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAutopark.DAL.Entities
{
    public sealed class Orders
    {
        public int OrderId { get; set; }
        public int VehicleId { get; set; }
        public DateTime Date { get; set; }
        public Orders()
        { }
        public Orders(int orderId, int vehicleId, DateTime date)
        {
            OrderId = orderId;
            VehicleId = vehicleId;
            Date = date;
        }
    }
}
