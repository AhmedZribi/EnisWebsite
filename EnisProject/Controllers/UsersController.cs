using EnisProject.Core;
using EnisProject.Data;
using EnisProject.Models;
using EnisProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EnisProject.Controllers
{
    
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly EnisContext _context;

        public UsersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, EnisContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }
        [Authorize(Roles = Constants.Roles.AdminRole)]
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.Select(user => new UserViewModel
            {
                UserId = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                Roles = _userManager.GetRolesAsync(user).Result
            }).ToListAsync();

            return View(users);
        }
        [Authorize(Roles = Constants.Roles.AdminRole)]
        public async Task<IActionResult> Add()
        {
            var roles = await _roleManager.Roles.Select(r => new RoleViewModel { RoleId = r.Id, RoleName = r.Name }).ToListAsync();
            ViewData["ClassId"] = new SelectList(_context.Class, "Id", "Id");

            var viewModel = new AddUserViewModel
            {
                Roles = roles,
            };

            return View(viewModel);
        }
        [Authorize(Roles = Constants.Roles.AdminRole)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AddUserViewModel model)
        {
            /*if (!ModelState.IsValid) 
                return View(model);*/

            if (!model.Roles.Any(r => r.IsSelected))
            {
                ModelState.AddModelError("Roles", "Please select at least 1 Role");
                return View(model);
            }

            if (await _userManager.FindByEmailAsync(model.Email) != null)
            {
                ModelState.AddModelError("Email", "Email Already Exists");
                return View(model);
            }

            if (await _userManager.FindByNameAsync(model.UserName) != null)
            {
                ModelState.AddModelError("UserName", "Username Already Exists");
                return View(model);
            }

            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                ClassId = model.ClassId
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("Roles", error.Description);
                }
            }

            await _userManager.AddToRolesAsync(user, model.Roles.Where(r => r.IsSelected).Select(r => r.RoleName));

            ViewData["ClassId"] = new SelectList(_context.Class, "Id", "Id", user.ClassId);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ManageSpeciality(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return NotFound();

            var specialities = await _context.Speciality.ToListAsync();

            var viewModel = new UserSpecialityViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                Specialities = specialities.Select(speciality => new SpecialityViewModel
                {
                    SpecialityId = speciality.Id,
                    SpecialityName = speciality.Name,
                }).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageSpeciality(UserSpecialityViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);

            if (user == null)
            {
                return NotFound();
            }

            var userSpeciality = await _context.Speciality.ToListAsync();
           
            foreach (var speciality in model.Specialities)
            {
                foreach (var spec in userSpeciality)
                {
                    if (speciality.IsSelected && speciality.SpecialityName == spec.Name)
                    {
                        user.SpecialityId = spec.Id;
                        Console.WriteLine(user.SpecialityId);

                        var result = await _userManager.UpdateAsync(user);
                        if (!result.Succeeded)
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError("Specialities", error.Description);
                            }
                        }
                    }
                }
            }

            return RedirectToAction("Index", "Home");
        }
        [Authorize(Roles = Constants.Roles.AdminRole)]
        public async Task<IActionResult> ManageRoles(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            var roles = await _roleManager.Roles.ToListAsync();

            var viewModel = new UserRolesViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                Roles = roles.Select(role => new RoleViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name,
                    IsSelected = _userManager.IsInRoleAsync(user, role.Name).Result
                }).ToList()
            };

            return View(viewModel);
        }
        [Authorize(Roles = Constants.Roles.AdminRole)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageRoles(UserRolesViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);

            if (user == null)
            {
                return NotFound();
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            foreach (var role in model.Roles)
            {
                if (userRoles.Any(r => r == role.RoleName) && !role.IsSelected)
                {
                    await _userManager.RemoveFromRoleAsync(user, role.RoleName);
                }
                if (!userRoles.Any(r => r == role.RoleName) && role.IsSelected)
                {
                    await _userManager.AddToRoleAsync(user, role.RoleName);
                }
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
