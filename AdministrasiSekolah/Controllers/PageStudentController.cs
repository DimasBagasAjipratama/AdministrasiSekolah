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
    ///<summary>
    ///class ini digunakan sebagai controller untuk bagian student
    ///</summary>
    public class PageStudentsController : Controller
    {
        /// <summary>
        /// inisiasi dari skolah dbcontext
        /// </summary>
        private readonly SekolahDBContext _context;
        /// <summary>
        /// method student contrlloller
        /// </summary>
        /// <param name="context">parameter database yang digunkan</param>

        public PageStudentsController(SekolahDBContext context)
        {
            _context = context;
        }

        // GET: Students
        /// <summary>
        /// method ini digunakan untuk mendapatkan data student dari database
        /// </summary>
        /// <returns> method ini akan mengebalikan list student yang telah tercatat di dalam database</returns>
        public async Task<IActionResult> Index(string ktsd, string searchString)
        {
            var ktsdList = new List<string>();
            var ktsdQuery = from d in _context.Student select d.NamaStudent;

            ktsdList.AddRange(ktsdQuery.Distinct());

            ViewBag.ktsd = new SelectList(ktsdList);

            var menu = from m in _context.Student.Include(k => k.NamaStudent) select m;

            if (!string.IsNullOrEmpty(ktsd))
			{
                menu = menu.Where(x => x.NamaStudent == ktsd);
			}

            if (!string.IsNullOrEmpty(searchString))
			{
                menu = menu.Where(s => s.NamaStudent.Contains(searchString));
			}

            var sekolahDBContext = _context.Student.Include(s => s.IdParentNavigation).Include(s => s.IdUserNavigation);
            return View(await sekolahDBContext.ToListAsync());




           
        }

        // GET: Students/Details/5
        /// <summary>
        /// method ini digunakan untuk mendapatkan data detail dari student
        /// </summary>
        /// <param name="id"> parameter id digunakan untuk memilih id student yang akan dilihat secara detail</param>
        /// <returns> mengembalikan data parent yang telah dipilih</returns>
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
        /// <summary>
        /// method ini digunakan untuk mendapatkan 
        /// </summary>
        /// <returns>mengembalikan view</returns>
        public IActionResult Create()
        {
            ViewData["IdParent"] = new SelectList(_context.Parent, "IdParent", "NamaAyah");
            ViewData["IdUser"] = new SelectList(_context.AccountUser, "IdUser", "Username");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
         /// <summary>
        /// method ini digunakan untuk menahkan data student ke dalam data siswa
        /// </summary>
        /// <param name="parent">parameter student merupakan nama dari siswa tersebut</param>
        /// <returns>mengembalikan view student</returns>
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
         /// <summary>
        /// method untuk mendapatkan data student dari database untuk dilakukan perubahan
        /// </summary>
        /// <param name="id"> parameter id merupakan parameter dari id user</param>
        /// <returns>mengembalikan data student</returns>
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
        /// <summary>
        /// method ini untuk menambahkan hasil perubahan data ke dalam database khususnya pada student
        /// </summary>
        /// <param name="id">merupakan id user yang akan dirubah datanya</param>
        /// <param name="parent">parameter parent merupakan data student yang akan dirubah</param>
        /// <returns>akan megembalikan data</returns>
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
        // <summary>
        /// method ini digunakan untuk mengambil data student yang akan dihapus dari database
        /// </summary>
        /// <param name="id"> parameter id digunakan untuk memilih data student mana yang akan dihapus</param>
        /// <returns>mengembalikan data student</returns>
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
        /// <summary>
        /// method ini digunakan untuk mengambahkan atau mengesekusi data student yang akan dihapus dari database
        /// </summary>
        /// <param name="id">parameter id digunakan untuk memilih data student mana yang akan dihapus dari database</param>
        /// <returns>akan mengembalikan data student</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var student = await _context.Student.FindAsync(id);
            _context.Student.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        /// <summary>
        /// method ini untuk mengetahui ada atau tidaknya data student dalam database
        /// </summary>
        /// <param name="id">parameter id digunakan untuk mencek data dalam database</param>
        /// <returns>true or false</returns>
        private bool StudentExists(string id)
        {
            return _context.Student.Any(e => e.Nis == id);
        }
    }
}
