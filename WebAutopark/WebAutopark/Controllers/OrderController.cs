using Microsoft.AspNetCore.Mvc;
using WebAutopark.DAL.Entities;
using WebAutopark.DAL.Interfaces;
using WebAutopark.DAL.Repositories;
using WebAutopark.ViewModels.Order;

namespace WebAutopark.Controllers
{
    public class OrderController : Controller
    {

        private IRepository<Vehicles> _vehiclesRepository;

        private IRepository<Components> _componentsRepository;

        private IRepository<Orders> _ordersRepository;

        private IRepository<OrderItems> _ordersItemsRepository;
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

        // check this controller if you replace depency on service IRepository<OrderItems>
        public async Task<IActionResult> Index()
        {
            IEnumerable<Orders> orders = await _ordersRepository.GetAll();
            List<OrderIndexViewModel> viewOrders = new List<OrderIndexViewModel>();
            Vehicles? vehicle = null;
            OrderItems? orderItem = new OrderItems();
            foreach (var order in orders)
            {
                vehicle = await _vehiclesRepository.Get(order.VehicleId);
                if (_ordersItemsRepository is SQLOrderItemsRepository ordersItemsSQLRep)
                {
                    orderItem = await ordersItemsSQLRep.GetInstanceByOrderId(order.OrderId);
                }

                OrderIndexViewModel oivm = new OrderIndexViewModel
                {
                    OrderId = order.OrderId,
                    Date = order.Date,
                    Vehicle = new VehicleIndexViewModel
                    {
                        VehicleId = vehicle.VehicleId,
                        RegistrationNumber = vehicle.RegistrationNumber,
                        Model = vehicle.Model
                    },
                    Component = await _componentsRepository.Get(orderItem.ComponentId),
                    Quantity = orderItem.Quantity
                };
                viewOrders.Add(oivm);
            }

            return View(viewOrders);
        }
    }
}
