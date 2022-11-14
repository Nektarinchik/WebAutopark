using Microsoft.AspNetCore.Mvc.Rendering;
using WebAutopark.DAL.Repositories;
using WebAutopark.DAL.Entities;
using WebAutopark.DAL.Interfaces;

namespace WebAutopark.ViewModels.Vehicle
{
    public class CreateViewModel
    {
        private IRepository<VehicleTypes> _vehicleTypesRepository;
        public List<SelectListItem> VehicleTypeModels { get; set; }
        public CreateViewModel(IRepository<VehicleTypes> vehicleTypesRepository)
        {
            _vehicleTypesRepository = vehicleTypesRepository;

            var asyncResult = _vehicleTypesRepository.GetAll();
            IEnumerable<VehicleTypeModel> vehicleTypeModels = _vehicleTypesRepository.GetAll().Result
               .Select(vt => new VehicleTypeModel
               {
                   Name = vt.Name,
                   VehicleTypeId = vt.VehicleTypeId
               })
               .ToList();

            VehicleTypeModels = new List<SelectListItem>();
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
