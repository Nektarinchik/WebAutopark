using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace WebAutopark.DAL.Entities
{
    public sealed class Components
    {
        public int ComponentId { get; set; }

        [Required(ErrorMessage = "This is required field!")]
        public string? Name { get; set; }
        public Components() 
        { }
    }
}
