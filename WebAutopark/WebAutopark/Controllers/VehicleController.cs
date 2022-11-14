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
        readonly IRepository<Vehicles> _vehiclesRepository;
        readonly IRepository<VehicleTypes> _vehicleTypesRepository;
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
                //foreach (var pair in ModelState)
                //{
                //    if (pair.Value.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                //    {
                //        if (pair.Key != "FuelConsumption" && 
                //            pair.Key != "Weight" && 
                //            pair.Key != "Mileage"
                //            )
                //        {
                //            CreateViewModel cvModel = new CreateViewModel(_vehicleTypesRepository);
                //            ViewBag.CreateViewModel = cvModel;
                //            return View(vehicle);
                //        }
                //    }
                //}
                //string? rawFuelConsumption = ModelState?["FuelConsumption"]?.RawValue?.ToString();
                //string? rawWeight = ModelState?["Weight"]?.RawValue?.ToString();
                //string? rawMileage = ModelState?["Mileage"]?.RawValue?.ToString();

                //if (!string.IsNullOrEmpty(rawFuelConsumption) &&
                //    !string.IsNullOrEmpty(rawWeight) &&
                //    !string.IsNullOrEmpty(rawMileage)
                //    )
                //{
                //    try
                //    {
                //        vehicle.FuelConsumption = Convert.ToDouble(rawFuelConsumption, CultureInfo.InvariantCulture);
                //        vehicle.Weight = Convert.ToDouble(rawWeight, CultureInfo.InvariantCulture);
                //        vehicle.Mileage = Convert.ToDouble(rawMileage, CultureInfo.InvariantCulture);
                //    }
                //    catch
                //    {
                //        CreateViewModel cvModel = new CreateViewModel(_vehicleTypesRepository);
                //        ViewBag.CreateViewModel = cvModel;
                //        return View(vehicle);
                //    }

                //    await _vehiclesRepository.Create(vehicle);
                //    return Redirect("~/Vehicle/Index");
                //}

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
                return Redirect("~/Vehicle/Index");
            }

            await _vehiclesRepository.Delete(vehicleId.Value);
            return Redirect("~/Vehicle/Index");
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

        [HttpPut]
        public async Task<IActionResult> Update(Vehicles vehicle)
        {
            if (ModelState.IsValid)
            {
                await _vehiclesRepository.Update(vehicle);
                return Redirect("~/Vehicle/Index");
            }
            else
            {
                //foreach (var pair in ModelState)
                //{
                //    if (pair.Value.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                //    {
                //        if (pair.Key != "FuelConsumption" &&
                //            pair.Key != "Weight" &&
                //            pair.Key != "Mileage"
                //            )
                //        {
                //            CreateViewModel cvModel = new CreateViewModel(_vehicleTypesRepository);
                //            ViewBag.CreateViewModel = cvModel;
                //            return View(vehicle);
                //        }
                //    }
                //}
                //string? rawFuelConsumption = ModelState?["FuelConsumption"]?.RawValue?.ToString();
                //string? rawWeight = ModelState?["Weight"]?.RawValue?.ToString();
                //string? rawMileage = ModelState?["Mileage"]?.RawValue?.ToString();

                //if (!string.IsNullOrEmpty(rawFuelConsumption) &&
                //    !string.IsNullOrEmpty(rawWeight) &&
                //    !string.IsNullOrEmpty(rawMileage)
                //    )
                //{
                //    try
                //    {
                //        vehicle.FuelConsumption = Convert.ToDouble(rawFuelConsumption, CultureInfo.InvariantCulture);
                //        vehicle.Weight = Convert.ToDouble(rawWeight, CultureInfo.InvariantCulture);
                //        vehicle.Mileage = Convert.ToDouble(rawMileage, CultureInfo.InvariantCulture);
                //    }
                //    catch
                //    {
                //        CreateViewModel cvModel = new CreateViewModel(_vehicleTypesRepository);
                //        ViewBag.CreateViewModel = cvModel;
                //        return View(vehicle);
                //    }

                //    await _vehiclesRepository.Update(vehicle);
                //    return Redirect("~/Vehicle/Index");
                //}

                CreateViewModel cvm = new CreateViewModel(_vehicleTypesRepository);
                ViewBag.CreateViewModel = cvm;
                return View(vehicle);
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
