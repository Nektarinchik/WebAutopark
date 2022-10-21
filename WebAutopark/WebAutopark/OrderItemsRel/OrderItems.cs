using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAutopark.OrderItemsRel
{
    public sealed class OrderItems
    {
        public int OrderItemId { get; private set; }
        public int OrderId { get; private set; }
        public int ComponentId { get; private set; }
        public int Quantity { get; set; }
        public OrderItems(int orderItemId, int orderId, int componentId, int quantity)
        {
            OrderItemId = orderItemId;
            OrderId = orderId;
            ComponentId = componentId;
            Quantity = quantity;
        }
    }
}
