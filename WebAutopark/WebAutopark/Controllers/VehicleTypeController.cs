using Microsoft.AspNetCore.Mvc;
using WebAutopark.DAL.Interfaces;
using WebAutopark.DAL.Entities;

namespace WebAutopark.Controllers
{
    public class VehicleTypeController : Controller
    {
        private readonly IRepository<VehicleTypes> _vehicleTypesRepository;
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
        public IActionResult GetCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(VehicleTypes vehicleType)
        {
            if (ModelState.IsValid)
            {
                await _vehicleTypesRepository.Create(vehicleType);
                return Redirect("~/VehicleType/Index");
            }

            return View(vehicleType);

        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromForm] int? vehicleTypeId)
        {
            if (!vehicleTypeId.HasValue)
            {
                return Ok();
            }
            await _vehicleTypesRepository.Delete(vehicleTypeId.Value);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetUpdate(int? vehicleTypeId)
        {
            if (!vehicleTypeId.HasValue)
            {
                return Redirect("~/VehicleType/Index");
            }
            return View(await _vehicleTypesRepository.Get(vehicleTypeId.Value));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm]VehicleTypes vehicleType)
        {
            if (ModelState.IsValid)
            {
                await _vehicleTypesRepository.Update(vehicleType);
                return Ok();
            }

            return View(vehicleType);
        }
    }
}
