using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAutopark.DAL.Entities;

namespace WebAutopark.ViewModels.Order
{
    public class CreateGetViewModel
    {
        public List<SelectListItem> Vehicles { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Components { get; set; } = new List<SelectListItem>();
    }
}
