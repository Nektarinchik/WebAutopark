﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebAutopark.ViewModels.Vehicle
{
    public class CreateViewModel
    {
        public List<SelectListItem> VehicleTypeModels { get; set; } = new List<SelectListItem>(); //No need to initialize here
    }
}
