using crud_with_ajax.Models;
using crud_with_ajax.Services.Interfaces;
using crud_with_ajax.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace crud_with_ajax.Controllers
{
    public class CityController : Controller
    {
        private readonly IProfileRepository _cityRepository;

        public CityController(IProfileRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public IActionResult Index()
        {
            return View(_cityRepository.GetCities());
        }


        public async Task<IActionResult> CreateOrUpdate(int id = 0)
        {
            if (id == 0)
            {
                return View(new City());
            }
            else
            {
                var city = _cityRepository.GetCity(id);
                if (city == null)
                {
                    return NotFound();
                }
                return View(city);
            }

        }




        [HttpPost]
        public async Task<IActionResult> CreateOrUpdate(int id, City city)
        {
            if (ModelState.IsValid)
            {
                if (id == 0)
                {
                    _cityRepository.Create(city);
                    //return RedirectToAction(nameof(Index));
                }

                else
                {
                    _cityRepository.Update(city);
                    //return RedirectToAction(nameof(Index));
                }

                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", _cityRepository.GetCities()) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "CreateOrUpdate", city) });
        }




        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (ModelState.IsValid)
            {
                var city = _cityRepository.GetCity(id);

                _cityRepository.Delete(city);
                return Json(new { html = Helper.RenderRazorViewToString(this, "_ViewAll", _cityRepository.GetCities()) });
            }
            return View();
        }

    }
}
