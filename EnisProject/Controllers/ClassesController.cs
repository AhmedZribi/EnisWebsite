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
    [Authorize(Roles = Constants.Roles.AdminRole)]
    public class ClassesController : Controller
    {
        private readonly EnisContext _context;

        public ClassesController(EnisContext context)
        {
            _context = context;
        }

        // GET: Classes
        public async Task<IActionResult> Index()
        {
              return View(await _context.Class.ToListAsync());
        }

        // GET: Classes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Class == null)
            {
                return NotFound();
            }

            var @class = await _context.Class
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@class == null)
            {
                return NotFound();
            }

            return View(@class);
        }

        // GET: Classes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Classes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClassName,Timetable")] Class @class)
        {
            if (Request.Form.Files.Count > 0)
            {
                var file = Request.Form.Files.FirstOrDefault();
                using (var datastream = new MemoryStream())
                {
                    await file.CopyToAsync(datastream);
                    @class.Timetable = datastream.ToArray();
                }
            }
            /*if (ModelState.IsValid)
            {
                return View(@class);

            }*/
            _context.Add(@class);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Classes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Class == null)
            {
                return NotFound();
            }

            var @class = await _context.Class.FindAsync(id);
            if (@class == null)
            {
                return NotFound();
            }
            return View(@class);
        }

        // POST: Classes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClassName,Timetable")] Class @class)
        {
            if (id != @class.Id)
            {
                return NotFound();
            }

            if (Request.Form.Files.Count > 0)
            {
                var file = Request.Form.Files.FirstOrDefault();
                using (var datastream = new MemoryStream())
                {
                    await file.CopyToAsync(datastream);
                    @class.Timetable = datastream.ToArray();
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@class);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassExists(@class.Id))
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
            return View(@class);
        }

        // GET: Classes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Class == null)
            {
                return NotFound();
            }

            var @class = await _context.Class
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@class == null)
            {
                return NotFound();
            }

            return View(@class);
        }

        // POST: Classes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Class == null)
            {
                return Problem("Entity set 'EnisContext.Class'  is null.");
            }
            var @class = await _context.Class.FindAsync(id);
            if (@class != null)
            {
                _context.Class.Remove(@class);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClassExists(int id)
        {
          return _context.Class.Any(e => e.Id == id);
        }
    }
}
