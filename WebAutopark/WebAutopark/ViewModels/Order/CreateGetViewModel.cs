using Microsoft.AspNetCore.Mvc.Rendering;
using WebAutopark.DAL.Entities;
using WebAutopark.DAL.Interfaces;
using System.Threading;

namespace WebAutopark.ViewModels.Order
{
    public class CreateGetViewModel
    {
        public List<SelectListItem> Vehicles { get; set; }
        public List<SelectListItem> Components { get; set; }
        public CreateGetViewModel(IRepository<Vehicles> vehiclesRepository, IRepository<Components> componentsRepository)
        {
            Vehicles = new List<SelectListItem>();
            IEnumerable<Vehicles> vehicles = vehiclesRepository.GetAll().Result;
            foreach (var vehicle in vehicles)
            {
                Vehicles.Add(new SelectListItem
                {
                    Value = vehicle.VehicleId.ToString(),
                    Text = $"Name: {vehicle.Model}\nRegistration Number: {vehicle.RegistrationNumber}"
                });
            }

            Components = new List<SelectListItem>();
            IEnumerable<Components> components = componentsRepository.GetAll().Result;
            foreach (var component in components)
            {
                Components.Add(new SelectListItem
                {
                    Value = component.ComponentId.ToString(),
                    Text = component.Name
                });
            }
        }
    }
}
