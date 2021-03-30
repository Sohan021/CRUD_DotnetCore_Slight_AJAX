using crud_with_ajax.Models;
using crud_with_ajax.Persistence;
using crud_with_ajax.Utilities;
using crud_with_ajax.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace crud_with_ajax.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly AppDbContext _context;
        private IHostingEnvironment _env;

        public UserController(UserManager<User> userManager, RoleManager<Role> roleManager,
             SignInManager<User> signInManager, IHostingEnvironment env, AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _env = env;
            _context = context;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View(_userManager.Users);
        }

        [HttpGet]
        public IActionResult RegisterOrUpdate(string id = null)
        {
            if (id == null)
            {
                return View(new User());
            }
            else
            {
                var user = _userManager.FindByIdAsync(id);
                if (user == null)
                {
                    return NotFound();
                }
                return View(user);


            }


        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterOrUpdate(string id, RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {

                if (id == null)
                {
                    var user = new User
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        UserName = model.Email,
                        Email = model.Email,
                        //Role = role


                    };
                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {

                        //var assignRole = await _context.UserRoles.AddAsync(
                        //    new UserRole { RoleId = role.Id, UserId = user.Id }
                        //    );
                        //await _context.SaveChangesAsync();

                        return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", _userManager.Users) });
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                //string uniqueFileName = ProcessUploadedFile(model);

                //var role = _context.Roles.Where(x => x.Id == model.RoleId).FirstOrDefault();


            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(
                    model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("index", "home");
                    }
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }

            return View(model);
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }


        private string ProcessUploadedFile(RegisterViewModel model)
        {
            string uniqueFileName = null;

            if (model.Photo != null)
            {
                string uploadsFolder = Path.Combine(_env.WebRootPath, "photos");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);
                }

            }

            return uniqueFileName;

        }
    }
}
