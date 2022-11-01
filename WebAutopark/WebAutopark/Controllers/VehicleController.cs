using Microsoft.AspNetCore.Mvc;
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
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Vehicles vehicle)
        {
            await _vehiclesRepository.Create(vehicle);
            return Redirect("~/Vehicle/Index");
        }
    }
}
