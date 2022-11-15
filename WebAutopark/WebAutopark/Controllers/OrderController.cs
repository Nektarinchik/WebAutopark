using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAutopark.DAL.Entities;
using WebAutopark.DAL.Interfaces;
using WebAutopark.DAL.Repositories;
using WebAutopark.ViewModels.Order;

namespace WebAutopark.Controllers
{
    public class OrderController : Controller
    {

        private readonly IRepository<Vehicles> _vehiclesRepository;

        private readonly IRepository<Components> _componentsRepository;

        private readonly IRepository<Orders> _ordersRepository;

        private readonly IRepository<OrderItems> _ordersItemsRepository;
        public OrderController(
            IRepository<Vehicles> vehicles,
            IRepository<Components> components,
            IRepository<Orders> orders,
            IRepository<OrderItems> ordersItems
            )
        {
            _vehiclesRepository = vehicles;
            _componentsRepository = components;
            _ordersRepository = orders;
            _ordersItemsRepository = ordersItems;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Orders> orders = await _ordersRepository.GetAll();
            List<IndexViewModel> viewOrders = new List<IndexViewModel>();
            Vehicles? vehicle = null;
            OrderItems? orderItem = new OrderItems();
            foreach (var order in orders)
            {
                if (order.VehicleId.HasValue)
                {
                    vehicle = await _vehiclesRepository.Get(order.VehicleId.Value);
                }
                orderItem = _ordersItemsRepository.GetAll().Result
                    .FirstOrDefault(o => o.OrderId == order.OrderId);

                IndexViewModel oivm = new IndexViewModel
                {
                    OrderId = order.OrderId,
                    Date = order.Date,
                    Vehicle = new VehicleViewModel
                    {
                        VehicleId = vehicle?.VehicleId,
                        RegistrationNumber = vehicle?.RegistrationNumber,
                        Model = vehicle?.Model
                    },
                    Component = null,
                    Quantity = 0
                };

                if (!ReferenceEquals(orderItem, null))
                {
                    oivm.Component = await _componentsRepository.Get(orderItem.ComponentId);
                    oivm.Quantity = orderItem.Quantity;
                }
                viewOrders.Add(oivm);
            }

            return View(viewOrders);
        }

        [HttpGet]
        public IActionResult Create()
        {
            CreateGetViewModel cvm = new CreateGetViewModel(_vehiclesRepository, _componentsRepository);
            ViewBag.CreateViewModel = cvm;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePostViewModel cpvm)
        {
            if (ModelState.IsValid)
            {
                Orders order = new Orders
                {
                    VehicleId = cpvm.VehicleId,
                    Date = cpvm.Date
                };

                await _ordersRepository.Create(order);

                OrderItems orderItem = new OrderItems
                {
                    OrderId = order.OrderId,
                    ComponentId = cpvm.ComponentId,
                    Quantity = cpvm.Quantity
                };
                await _ordersItemsRepository.Create(orderItem);

                return Redirect("~/Order/Index");
            }

            CreateGetViewModel cvm = new CreateGetViewModel(_vehiclesRepository, _componentsRepository);
            ViewBag.CreateViewModel = cvm;
            return View(cpvm);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? orderId)
        {
            if (!orderId.HasValue)
            {
                return Redirect("~/Order/Index");
            }

            var order = await _ordersRepository.Get(orderId.Value);
            Vehicles? vehicle = null;
            if (order.VehicleId.HasValue)
            {
                vehicle = await _vehiclesRepository.Get(order.VehicleId.Value);
            }
            var orderItem = _ordersItemsRepository.GetAll().Result
                .FirstOrDefault(oi => oi.OrderId == orderId);

            DetailViewModel dvm = new DetailViewModel
            {
                Vehicle = new VehicleViewModel
                {
                    VehicleId = vehicle?.VehicleId,
                    Model = vehicle?.Model,
                    RegistrationNumber = vehicle?.RegistrationNumber
                },
                Component = null,
                Date = order.Date,
                Quantity = null
            };

            if (orderItem != null)
            {
                dvm.Component = await _componentsRepository.Get(orderItem.ComponentId);
                dvm.Quantity = orderItem.Quantity;
            }

            return View(dvm);
        }
    }
}
