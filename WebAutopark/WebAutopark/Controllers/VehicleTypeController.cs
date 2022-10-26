using Microsoft.AspNetCore.Mvc;
using WebAutopark.DAL.Interfaces;
using WebAutopark.DAL.Entities;

namespace WebAutopark.Controllers
{
    public class VehicleTypeController : Controller
    {
        IRepository<VehicleTypes> _vehicleTypesRepository = null!;
        public VehicleTypeController(IRepository<VehicleTypes> vehicleTypesRepository)
        {
            _vehicleTypesRepository = vehicleTypesRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _vehicleTypesRepository.GetAll());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(VehicleTypes vehicleType)
        {
            await _vehicleTypesRepository.Create(vehicleType);
            return Redirect("~/VehicleType/Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? vehicleTypeId)
        {
            if (!vehicleTypeId.HasValue)
            {
                return Redirect("~/VehicleType/Index");
            }
            await _vehicleTypesRepository.Delete(vehicleTypeId.Value);
            return Redirect("~/VehicleType/Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? vehicleTypeId)
        {
            if (!vehicleTypeId.HasValue)
            {
                return Redirect("~/VehicleType/Index");
            }
            return View(await _vehicleTypesRepository.Get(vehicleTypeId.Value));
        }

        [HttpPost]
        public async Task<IActionResult> Update(VehicleTypes vehicleType)
        {
            await _vehicleTypesRepository.Update(vehicleType);
            return Redirect("~/VehicleType/Index");
        }
    }
}
