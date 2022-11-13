﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        private IRepository<Vehicles> _vehiclesRepository; // make it readonly

        private IRepository<Components> _componentsRepository; // make it readonly

        private IRepository<Orders> _ordersRepository; // make it readonly

        private IRepository<OrderItems> _ordersItemsRepository; // make it readonly
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
            Vehicles? vehicle = null; //Why nullable?
            OrderItems? orderItem = new OrderItems(); //Why nullable? No need to initialize here
            foreach (var order in orders)
            {
                vehicle = await _vehiclesRepository.Get(order.VehicleId);
                orderItem = _ordersItemsRepository.GetAll().Result
                    .Select(o => o)
                    .Where(o => o.OrderId == order.OrderId)
                    .FirstOrDefault(); // We can simplify this.

                IndexViewModel oivm = new IndexViewModel
                {
                    OrderId = order.OrderId,
                    Date = order.Date,
                    Vehicle = new VehicleViewModel
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

        [HttpGet]
        public async Task<IActionResult> Create() //Rename
        {
            CreateGetViewModel cvm = new CreateGetViewModel();
            IEnumerable <Vehicles> vehicles = await _vehiclesRepository.GetAll();
            foreach (var vehicle in vehicles)
            {
                cvm.Vehicles.Add(new SelectListItem
                {
                    Value = vehicle.VehicleId.ToString(),
                    Text = $"Name: {vehicle.Model}\nRegistration Number: {vehicle.RegistrationNumber}"
                });
            }

            IEnumerable<Components> components = await _componentsRepository.GetAll();
            foreach (var component in components)
            {
                cvm.Components.Add(new SelectListItem
                {
                    Value = component.ComponentId.ToString(),
                    Text = component.Name
                });
            }

            ViewBag.CreateViewModel = cvm;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePostViewModel cpvm)
        {
            Orders order = new Orders
            {
                VehicleId = cpvm.VehicleId,
                Date = cpvm.Date
            };
            _ = _ordersRepository.Create(order);

            OrderItems orderItem = new OrderItems
            {
                OrderId = order.OrderId,
                ComponentId = cpvm.ComponentId,
                Quantity = cpvm.Quantity
            };
            await _ordersItemsRepository.Create(orderItem);

            return Redirect("~/Order/Index");
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? orderId)
        {
            if (!orderId.HasValue)
            {
                return Redirect("~/Order/Index");
            }

            var order = await _ordersRepository.Get(orderId.Value);
            var vehicle = await _vehiclesRepository.Get(order.VehicleId);
            var orderItem = _ordersItemsRepository.GetAll().Result
                .Select(oi => oi)
                .Where(oi => oi.OrderId == orderId)
                .FirstOrDefault(); // We can simplify this
            DetailViewModel dvm = new DetailViewModel
            {
                Vehicle = new VehicleViewModel
                {
                    VehicleId = vehicle.VehicleId,
                    Model = vehicle.Model,
                    RegistrationNumber = vehicle.RegistrationNumber
                },
                Component = await _componentsRepository.Get(orderItem.ComponentId),
                Date = order.Date,
                Quantity = orderItem.Quantity
            };

            return View(dvm);
        }
    }
}
