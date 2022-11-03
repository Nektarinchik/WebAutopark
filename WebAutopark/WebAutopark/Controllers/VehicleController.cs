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

        [HttpGet]
        public async Task<IActionResult> Update(int? vehicleId)
        {
            if (!vehicleId.HasValue)
            {
                return Redirect("~/Vehicle/Index");
            }
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
            return View(await _vehiclesRepository.Get(vehicleId.Value));
        }

        [HttpPost]
        public async Task<IActionResult> Update(Vehicles vehicle)
        {
            await _vehiclesRepository.Update(vehicle);
            return Redirect("~/Vehicle/Index");
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? vehicleId)
        {
            if (!vehicleId.HasValue)
            {
                return Redirect("~/Vehicle/Index");
            }

            Vehicles vehicle = await _vehiclesRepository.Get(vehicleId.Value);
            //            public int VehicleId { get; set; }
            //public int VehicleTypeId { get; set; }
            //public double Weight { get; set; }
            //public string? RegistrationNumber { get; set; }
            //public string Model { get; set; } = null!;
            //public int Year { get; set; }
            //public double Mileage { get; set; }
            //public Colors Color { get; set; }
            //public double FuelConsumption { get; set; }
            //public VehicleTypes VehicleType { get; set; } = null!;
            //public int Volume { get; set; }
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
            //DetailViewModel dvm = new DetailViewModel
            //{
            //    Vehicle = vehicle,
            //    VehicleType = await _vehicleTypesRepository.Get(vehicle.VehicleTypeId)
            //};

            return View(vehicleDetailViewModel);
        }
        
    }
}
