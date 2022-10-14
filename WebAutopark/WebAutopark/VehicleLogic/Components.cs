using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAutopark.VehicleLogic
{
    public sealed class Components
    {
        public int ComponentId { get; private set; }
        public string Name { get; set; }
        public Components(int componentId, string name)
        {
            ComponentId = componentId;
            Name = name;
        }
    }
}
