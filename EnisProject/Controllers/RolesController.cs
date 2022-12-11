using EnisProject.Core;
using EnisProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnisProject.Controllers
{
    [Authorize( Roles = Constants.Roles.AdminRole)]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(RoleFormViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View("Index", await _roleManager.Roles.ToListAsync());
            }

            if (await _roleManager.RoleExistsAsync(model.RoleName))
            {
                ModelState.AddModelError("Name", "Role exists!");

                return View("Index", await _roleManager.Roles.ToListAsync());
            }

            await _roleManager.CreateAsync(new IdentityRole(model.RoleName.Trim()));

            return RedirectToAction(nameof(Index));
        }
    }
}
