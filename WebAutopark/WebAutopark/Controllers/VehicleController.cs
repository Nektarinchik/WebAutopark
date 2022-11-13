using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAutopark.DAL.Interfaces;
using WebAutopark.DAL.Entities;
using WebAutopark.ViewModels.Vehicle;
using WebAutopark.DAL.Repositories;

namespace WebAutopark.Controllers
{
    
    public class VehicleController : Controller
    {
        private IRepository<Vehicles> _vehiclesRepository; // make it readonly
        private IRepository<VehicleTypes> _vehicleTypesRepository; // make it readonly
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

            if (_vehiclesRepository is SQLVehiclesRepository vehicles)
            {
                switch (state)
                {
                    case SortState.MODEL:
                        ivm.Vehicles = await vehicles.GetSortedByModel();
                        break;
                    case SortState.VEHICLETYPE:
                        ivm.Vehicles = await vehicles.GetSortedByVehicleType();
                        break;
                    case SortState.MILEAGE:
                        ivm.Vehicles = await vehicles.GetSortedByMileage();
                        break;
                    default:
                        break;
                }
            }
            
            return View(ivm);
        }

        [HttpGet]
        public IActionResult Create() //rename
        {
            IEnumerable<VehicleTypeModel> vehicleTypeModels = _vehicleTypesRepository.GetAll().Result //use await instead of result
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

        [HttpGet] //it's better to use HttpDelete for delete method
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
        public async Task<IActionResult> Update(int? vehicleId) //Rename
        {
            if (!vehicleId.HasValue)
            {
                return Redirect("~/Vehicle/Index");
            }
            IEnumerable<VehicleTypeModel> vehicleTypeModels = _vehicleTypesRepository.GetAll().Result //use await instead of result
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

        [HttpPost] //Usually we use HttpPut to update method 
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

        [HttpPost]
        public async Task<IActionResult> VehicleSort(SortState state = SortState.DEFAULT) //Where do we use this method?
        {
            IndexViewModel ivm = new IndexViewModel
            {
                VehicleTypes = await _vehicleTypesRepository.GetAll(),
                Vehicles = await _vehiclesRepository.GetAll()
            };
            switch (state)
            {
                case SortState.DEFAULT:
                    break;
                case SortState.MODEL:
                    ivm.Vehicles.ToList().Sort();
                    break;
                case SortState.VEHICLETYPE:
                    ivm.Vehicles.ToList().Reverse();
                    break;
                case SortState.MILEAGE:
                    ivm.Vehicles.ToList().Sort();
                    break;
                default:
                    break;
            }
            return PartialView(ivm);
        }
        
    }
}
