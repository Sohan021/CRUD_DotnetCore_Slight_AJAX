using crud_with_ajax.Models;
using crud_with_ajax.Persistence;
using crud_with_ajax.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace crud_with_ajax.Controllers
{
    public class RoleController : Controller
    {
        private readonly AppDbContext _context;

        public RoleController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var roles = _context.Roles.ToList();

            return View(roles);

        }




        public async Task<IActionResult> CreateOrUpdate(string id = null)
        {
            if (id == null)
            {
                return View(new Role());
            }
            else
            {
                var role = _context.Roles.Where(_ => _.Id == id).FirstOrDefault();
                if (role == null)
                {
                    return NotFound();
                }
                return View(role);
            }

        }

        //[HttpPost]
        //public async Task<IActionResult> Create(Role role)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        await _context.AddAsync(role);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(role);
        //}




        //public IActionResult Update(string id)
        //{
        //    var role = _context.Roles.Where(_ => _.Id == id).FirstOrDefault();
        //    if (role == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(role);
        //}


        [HttpPost]
        public async Task<IActionResult> CreateOrUpdate(string id, Role role)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    await _context.Roles.AddAsync(role);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    _context.Update(role);
                    _context.SaveChanges();
                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", _context.UserRoles) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "CreateOrUpdate", role) });
        }




        //public IActionResult Delete(string id)
        //{
        //    var role = _context.Roles.Where(_ => _.Id == id).FirstOrDefault();
        //    if (role == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(role);
        //}

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            if (ModelState.IsValid)
            {
                var role = _context.Roles.Where(_ => _.Id == id).FirstOrDefault();
                _context.Remove(role);
                return Json(new { html = Helper.RenderRazorViewToString(this, "_ViewAll", _context.UserRoles) });
            }
            return View();
        }

    }
}
