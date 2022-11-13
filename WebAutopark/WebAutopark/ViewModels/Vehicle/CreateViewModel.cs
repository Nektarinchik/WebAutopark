using Microsoft.AspNetCore.Mvc.Rendering;
using WebAutopark.DAL.Repositories;
using WebAutopark.DAL.Entities;
using WebAutopark.DAL.Interfaces;

namespace WebAutopark.ViewModels.Vehicle
{
    public class CreateViewModel
    {
        private IRepository<VehicleTypes> _vehicleTypesRepository = null!;
        public List<SelectListItem> VehicleTypeModels { get; set; } = new List<SelectListItem>();
        public CreateViewModel(IRepository<VehicleTypes> vehicleTypesRepository)
        {
            _vehicleTypesRepository = vehicleTypesRepository;

            IEnumerable<VehicleTypeModel> vehicleTypeModels = _vehicleTypesRepository.GetAll().Result
               .Select(vt => new VehicleTypeModel
               {
                   Name = vt.Name,
                   VehicleTypeId = vt.VehicleTypeId
               })
               .ToList();

            foreach (var vehicleTypeModel in vehicleTypeModels)
            {
                VehicleTypeModels.Add(new SelectListItem
                {
                    Value = vehicleTypeModel.VehicleTypeId.ToString(),
                    Text = vehicleTypeModel.Name
                });
            }
        }
    }
}
