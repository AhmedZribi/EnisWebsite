using EnisProject.Core;
using EnisProject.Data;
using EnisProject.Models;
using EnisProject.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EnisProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EnisContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public HomeController(ILogger<HomeController> logger, EnisContext context, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var enisContext = _context.Internship.Include(i => i.Speciality);
            return View(await enisContext.ToListAsync());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public async Task<IActionResult> ListInternships(string userId,string searchString)
        {
            var user = _context.Users.Find(userId);
            
            if (user.SpecialityId != null )
            {
                var inships = from i in _context.Internship
                              where i.SpecialityId == user.SpecialityId
                              select i;
                if (!String.IsNullOrEmpty(searchString))
                {
                    inships = inships.Where(s => s.InternshipHeader!.Contains(searchString));
                }
                return View(await inships.ToListAsync());
            }

            var internships = from i in _context.Internship
                          select i;

            if (!String.IsNullOrEmpty(searchString))
            {
                internships = internships.Where(s => s.InternshipHeader!.Contains(searchString));
            }


            return View(await internships.ToListAsync());
        }

        public async Task<IActionResult> ListProfessors(string userId, string searchString)
        {
            var user = _context.Users.Find(userId);

            if (user.SpecialityId != null)
            {
                var profs = _userManager.GetUsersInRoleAsync(Constants.Roles.ProRole).Result.Where(
                    p => p.SpecialityId == user.SpecialityId);

                if (!String.IsNullOrEmpty(searchString))
                {
                    profs = profs.Where(s => s.UserName!.Contains(searchString));
                }

                return View(profs.ToList());
            }

            var professors = _userManager.GetUsersInRoleAsync(Constants.Roles.ProRole).Result;
            IEnumerable<ApplicationUser> ps = professors; 

            if (!String.IsNullOrEmpty(searchString))
            {
                ps = ps.Where(s => s.UserName!.Contains(searchString));
            }


            return View(ps.ToList());
        }

        public async Task<IActionResult> ListStudents(string userId, string searchString)
        {
            var user = _context.Users.Find(userId);

            if (user.SpecialityId != null)
            {
                var stds = _userManager.GetUsersInRoleAsync(Constants.Roles.StRole).Result.Where(
                    p => p.SpecialityId == user.SpecialityId);

                if (!String.IsNullOrEmpty(searchString))
                {
                    stds = stds.Where(s => s.UserName!.Contains(searchString));
                }

                return View(stds.ToList());
            }

            var students = _userManager.GetUsersInRoleAsync(Constants.Roles.StRole).Result;
            IEnumerable<ApplicationUser> sts = students;

            if (!String.IsNullOrEmpty(searchString))
            {
                sts = sts.Where(s => s.UserName!.Contains(searchString));
            }


            return View(sts.ToList());
        }
    }
}