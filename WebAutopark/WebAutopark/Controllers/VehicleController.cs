using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAutopark.DAL.Interfaces;
using WebAutopark.DAL.Entities;
using WebAutopark.ViewModels.Vehicle;

namespace WebAutopark.Controllers
{
    
    public class VehicleController : Controller
    {
        private IRepository<Vehicles> _vehiclesRepository;
        private IRepository<VehicleTypes> _vehicleTypesRepository;
        public VehicleController(IRepository<Vehicles> vehiclesRepository, IRepository<VehicleTypes> vehicleTypesRepository)
        {
            _vehiclesRepository = vehiclesRepository;
            _vehicleTypesRepository = vehicleTypesRepository;
        }
        public async Task<IActionResult> Index()
        {
            IndexViewModel ivm = new IndexViewModel { 
                VehicleTypes = await _vehicleTypesRepository.GetAll(), 
                Vehicles = await _vehiclesRepository.GetAll() 
            };
            return View(ivm);
        }

        [HttpGet]
        public IActionResult Create()
        {
            IEnumerable<VehicleTypeModel> vehicleTypeModels = _vehicleTypesRepository.GetAll().Result
                .Select(vt => new VehicleTypeModel
                {
                    Name = vt.Name,
                    VehicleTypeId = vt.VehicleTypeId
                })
                .ToList();
            CreateViewModel cvm = new CreateViewModel();
            foreach (var vehicleTypeModel in vehicleTypeModels)
            {
                cvm.VehicleTypeModels.Add(new SelectListItem
                {
                    Value = vehicleTypeModel.VehicleTypeId.ToString(),
                    Text = vehicleTypeModel.Name
                });
            }
            ViewBag.CreateViewModel = cvm;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Vehicles vehicle)
        {
            await _vehiclesRepository.Create(vehicle);
            return Redirect("~/Vehicle/Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? vehicleId)
        {
            if (!vehicleId.HasValue)
            {
                return Redirect("~/Vehicle/Index");
            }

            await _vehiclesRepository.Delete(vehicleId.Value);
            return Redirect("~/Vehicle/Index");
        }
    }
}
