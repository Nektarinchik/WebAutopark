using Microsoft.AspNetCore.Mvc;
using WebAutopark.DAL.Interfaces;
using WebAutopark.DAL.Entities;

namespace WebAutopark.Controllers
{
    public class ComponentController : Controller
    {
        private readonly IRepository<Components> _componentsRepository;
        public ComponentController(IRepository<Components> componentsRepository)
        {
            _componentsRepository = componentsRepository;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _componentsRepository.GetAll());
        }

        [HttpGet]
        public IActionResult GetCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Components component)
        {
            if (ModelState.IsValid)
            {
                await _componentsRepository.Create(component);
                return Redirect("~/Component/Index");
            }

            return View(component);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int? componentId)
        {
            if (!componentId.HasValue)
            {
                return Ok();
            }
            await _componentsRepository.Delete(componentId.Value);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetUpdate(int? componentId)
        {
            if (!componentId.HasValue)
            {
                return Redirect("~/Component/Index");
            }
            return View(await _componentsRepository.Get(componentId.Value));
        }

        [HttpPut]
        public async Task<IActionResult> Update(Components component)
        {
            if (ModelState.IsValid)
            {
                await _componentsRepository.Update(component);
                return Ok();
            }

            return View(component);
        }
    }
}
