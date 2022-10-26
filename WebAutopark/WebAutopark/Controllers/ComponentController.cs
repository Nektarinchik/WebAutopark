using Microsoft.AspNetCore.Mvc;
using WebAutopark.DAL.Interfaces;
using WebAutopark.DAL.Entities;

namespace WebAutopark.Controllers
{
    public class ComponentController : Controller
    {
        private IRepository<Components> _componentsRepository;

        public ComponentController(IRepository<Components> componentsRepository)
        {
            _componentsRepository = componentsRepository;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _componentsRepository.GetAll());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Components component)
        {
            await _componentsRepository.Create(component);
            return Redirect("~/Component/Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? componentId)
        {
            if (!componentId.HasValue)
            {
                return Redirect("~/Component/Index");
            }
            await _componentsRepository.Delete(componentId.Value);
            return Redirect("~/Component/Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? componentId)
        {
            if (!componentId.HasValue)
            {
                return Redirect("~/Component/Index");
            }
            return View(await _componentsRepository.Get(componentId.Value));
        }

        [HttpPost]
        public async Task<IActionResult> Update(Components component)
        {
            await _componentsRepository.Update(component);
            return Redirect("~/Component/Index");
        }
    }
}
