using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAutopark.DAL.Entities
{
    public sealed class OrderItems
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int ComponentId { get; set; }
        public int Quantity { get; set; }
        public OrderItems()
        { }
        public OrderItems(int orderItemId, int orderId, int componentId, int quantity)
        {
            OrderItemId = orderItemId;
            OrderId = orderId;
            ComponentId = componentId;
            Quantity = quantity;
        }
    }
}
