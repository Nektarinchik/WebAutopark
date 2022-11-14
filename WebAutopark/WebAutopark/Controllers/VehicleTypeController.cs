using Microsoft.AspNetCore.Mvc;
using WebAutopark.DAL.Interfaces;
using WebAutopark.DAL.Entities;

namespace WebAutopark.Controllers
{
    public class VehicleTypeController : Controller
    {
        readonly IRepository<VehicleTypes> _vehicleTypesRepository;
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
                //return Redirect("~/VehicleType/Index");
            }
            await _vehicleTypesRepository.Delete(vehicleTypeId.Value);
            return Ok();
            //return Redirect("~/VehicleType/Index");
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

        [HttpPost]
        public async Task<IActionResult> Update(VehicleTypes vehicleType)
        {
            if (ModelState.IsValid)
            {
                await _vehicleTypesRepository.Update(vehicleType);
                return Redirect("~/VehicleType/Index");
            }

            return View(vehicleType);
        }
    }
}
