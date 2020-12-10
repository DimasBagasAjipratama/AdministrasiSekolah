using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdministrasiSekolah.Models;

namespace AdministrasiSekolah.Controllers
{
    public class PageStudentsController : Controller
    {
        private readonly SekolahDBContext _context;

        public PageStudentsController(SekolahDBContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            var sekolahDBContext = _context.Student.Include(s => s.IdParentNavigation).Include(s => s.IdUserNavigation);
            return View(await sekolahDBContext.ToListAsync());
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .Include(s => s.IdParentNavigation)
                .Include(s => s.IdUserNavigation)
                .FirstOrDefaultAsync(m => m.Nis == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            ViewData["IdParent"] = new SelectList(_context.Parent, "IdParent", "NamaAyah");
            ViewData["IdUser"] = new SelectList(_context.AccountUser, "IdUser", "Username");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nis,NamaStudent,Kelas,Angkatan,Gender,Alamat,Password,IdUser,IdParent")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdParent"] = new SelectList(_context.Parent, "IdParent", "IdParent", student.IdParent);
            ViewData["IdUser"] = new SelectList(_context.AccountUser, "IdUser", "IdUser", student.IdUser);
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            ViewData["IdParent"] = new SelectList(_context.Parent, "IdParent", "IdParent", student.IdParent);
            ViewData["IdUser"] = new SelectList(_context.AccountUser, "IdUser", "IdUser", student.IdUser);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Nis,NamaStudent,Kelas,Angkatan,Gender,Alamat,Password,IdUser,IdParent")] Student student)
        {
            if (id != student.Nis)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Nis))
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
            ViewData["IdParent"] = new SelectList(_context.Parent, "IdParent", "IdParent", student.IdParent);
            ViewData["IdUser"] = new SelectList(_context.AccountUser, "IdUser", "IdUser", student.IdUser);
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .Include(s => s.IdParentNavigation)
                .Include(s => s.IdUserNavigation)
                .FirstOrDefaultAsync(m => m.Nis == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var student = await _context.Student.FindAsync(id);
            _context.Student.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(string id)
        {
            return _context.Student.Any(e => e.Nis == id);
        }
    }
}
