using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EnisProject.Data;
using EnisProject.Models;
using Microsoft.AspNetCore.Authorization;
using EnisProject.Core;

namespace EnisProject.Controllers
{
    public class InternshipsController : Controller
    {
        private readonly EnisContext _context;

        public InternshipsController(EnisContext context)
        {
            _context = context;
        }
        [Authorize(Roles = Constants.Roles.AdminRole)]
        // GET: Internships
        public async Task<IActionResult> Index()
        {
            var enisContext = _context.Internship.Include(i => i.Speciality);
            return View(await enisContext.ToListAsync());
        }

        // GET: Internships/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Internship == null)
            {
                return NotFound();
            }

            var internship = await _context.Internship
                .Include(i => i.Speciality)
                .FirstOrDefaultAsync(m => m.InternshipId == id);
            if (internship == null)
            {
                return NotFound();
            }

            return View(internship);
        }

        // GET: Internships/Create
        [Authorize(Roles = Constants.Roles.AdminRole)]
        public IActionResult Create()
        {
            ViewData["SpecialityId"] = new SelectList(_context.Speciality, "Id", "Id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Constants.Roles.AdminRole)]
        public async Task<IActionResult> Create([Bind("InternshipId,InternshipHeader,InternshipInfo,InternshipPicture,InternshipLocation,SpecialityId")] Internship internship)
        {
            if (Request.Form.Files.Count > 0)
            {
                var file = Request.Form.Files.FirstOrDefault();
                using (var datastream = new MemoryStream())
                {
                    await file.CopyToAsync(datastream);
                    internship.InternshipPicture = datastream.ToArray();
                }
            }
            /*if (!ModelState.IsValid)
            {
                ViewData["SpecialityId"] = new SelectList(_context.Speciality, "Id", "Id", internship.SpecialityId);
                return View(internship);
                
            }*/
            _context.Add(internship);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Internships/Edit/5
        [Authorize(Roles = Constants.Roles.AdminRole)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Internship == null)
            {
                return NotFound();
            }

            var internship = await _context.Internship.FindAsync(id);
            if (internship == null)
            {
                return NotFound();
            }
            ViewData["SpecialityId"] = new SelectList(_context.Speciality, "Id", "Id", internship.SpecialityId);
            return View(internship);
        }

        // POST: Internships/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Constants.Roles.AdminRole)]
        public async Task<IActionResult> Edit(int id, [Bind("InternshipId,InternshipHeader,InternshipInfo,InternshipPicture,InternshipLocation,SpecialityId")] Internship internship)
        {
            if (id != internship.InternshipId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(internship);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InternshipExists(internship.InternshipId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["SpecialityId"] = new SelectList(_context.Speciality, "Id", "Id", internship.SpecialityId);
            return View(internship);
        }

        // GET: Internships/Delete/5
        [Authorize(Roles = Constants.Roles.AdminRole)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Internship == null)
            {
                return NotFound();
            }

            var internship = await _context.Internship
                .Include(i => i.Speciality)
                .FirstOrDefaultAsync(m => m.InternshipId == id);
            if (internship == null)
            {
                return NotFound();
            }

            return View(internship);
        }

        // POST: Internships/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Constants.Roles.AdminRole)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Internship == null)
            {
                return Problem("Entity set 'EnisContext.Internship'  is null.");
            }
            var internship = await _context.Internship.FindAsync(id);
            if (internship != null)
            {
                _context.Internship.Remove(internship);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InternshipExists(int id)
        {
          return _context.Internship.Any(e => e.InternshipId == id);
        }
    }
}
