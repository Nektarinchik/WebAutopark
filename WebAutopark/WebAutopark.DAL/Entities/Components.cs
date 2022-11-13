using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAutopark.DAL.Entities
{
    public sealed class Components
    {
        public int ComponentId { get; set; }
        public string? Name { get; set; } = null!; //Don't initialize it here 
        public Components() 
        { }
        public Components(int componentId, string name)
        {
            ComponentId = componentId;
            Name = name;
        }
    }
}
