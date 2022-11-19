using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using WebAutopark.DAL.Interfaces;
using WebAutopark.DAL.Entities;
using WebAutopark.ViewModels.Vehicle;
using WebAutopark.DAL.Repositories;

namespace WebAutopark.Controllers
{
    
    public class VehicleController : Controller
    {
        private readonly IRepository<Vehicles> _vehiclesRepository;
        private readonly IRepository<VehicleTypes> _vehicleTypesRepository;
        public VehicleController(IRepository<Vehicles> vehiclesRepository, IRepository<VehicleTypes> vehicleTypesRepository)
        {
            _vehiclesRepository = vehiclesRepository;
            _vehicleTypesRepository = vehicleTypesRepository;
        }
        public async Task<IActionResult> Index(SortState state = SortState.DEFAULT)
        {
            IndexViewModel ivm = new IndexViewModel { 
                VehicleTypes = await _vehicleTypesRepository.GetAll(), 
                Vehicles = await _vehiclesRepository.GetAll() 
            };

            switch (state)
            {
                case SortState.MODEL:
                    ivm.Vehicles = ivm.Vehicles.OrderBy(v => v.Model);
                    break;
                case SortState.VEHICLETYPE:
                    ivm.Vehicles = ivm.Vehicles.Join(ivm.VehicleTypes,
                        v => v.VehicleTypeId,
                        vt => vt.VehicleTypeId,
                        (v, vt) => new { Vehicle = v, VehicleType = vt })
                        .OrderBy(vvt => vvt.VehicleType.Name)
                        .Select(vvt => vvt.Vehicle);
                    break;
                case SortState.MILEAGE:
                    ivm.Vehicles = ivm.Vehicles.OrderBy(v => v.Mileage);
                    break;
                default:
                    break;
            }
            
            return View(ivm);
        }

        [HttpGet]
        public IActionResult GetCreate()
        {
            CreateViewModel cvm = new CreateViewModel(_vehicleTypesRepository);
            ViewBag.CreateViewModel = cvm;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Vehicles vehicle)
        {
            if (ModelState.IsValid)
            {
                await _vehiclesRepository.Create(vehicle);
                return Redirect("~/Vehicle/Index");
            }
            else
            {
                CreateViewModel cvm = new CreateViewModel(_vehicleTypesRepository);
                ViewBag.CreateViewModel = cvm;
                return View(vehicle);
            }

        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int? vehicleId)
        {
            if (!vehicleId.HasValue)
            {
                return Ok();
            }

            await _vehiclesRepository.Delete(vehicleId.Value);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetUpdate(int? vehicleId)
        {
            if (!vehicleId.HasValue)
            {
                return Redirect("~/Vehicle/Index");
            }
            CreateViewModel cvm = new CreateViewModel(_vehicleTypesRepository);
            ViewBag.CreateViewModel = cvm;
            return View(await _vehiclesRepository.Get(vehicleId.Value));
        }

        [HttpPost]
        public async Task<IActionResult> Update(Vehicles vehicle)
        {
            if (ModelState.IsValid)
            {
                await _vehiclesRepository.Update(vehicle);
                return Redirect("~/Vehicle/Index");
            }
            else
            {
                CreateViewModel cvm = new CreateViewModel(_vehicleTypesRepository);
                ViewBag.CreateViewModel = cvm;
                return View("GetUpdate", vehicle);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? vehicleId)
        {
            if (!vehicleId.HasValue)
            {
                return Redirect("~/Vehicle/Index");
            }

            Vehicles vehicle = await _vehiclesRepository.Get(vehicleId.Value);
            VehicleDetailViewModel vehicleDetailViewModel = new VehicleDetailViewModel
            {
                VehicleId = vehicle.VehicleId,
                Weight = vehicle.Weight,
                RegistrationNumber = vehicle.RegistrationNumber,
                Model = vehicle.Model,
                Year = vehicle.Year,
                Mileage = vehicle.Mileage,
                Color = vehicle.Color,
                FuelConsumption = vehicle.FuelConsumption,
                Volume = vehicle.Volume,
                VehicleType = await _vehicleTypesRepository.Get(vehicle.VehicleTypeId)
            };

            return View(vehicleDetailViewModel);
        }
    }
}
