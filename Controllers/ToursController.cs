using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TourBase_Stage_1_2;

namespace TourBase_Stage_1_2.Controllers
{
    public class ToursController : Controller
    {
        private readonly TourBaseContext _context;

        public ToursController(TourBaseContext context)
        {
            _context = context;
        }

        // GET: Tours
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tours.ToListAsync());
        }

        // GET: Tours/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tour = await _context.Tours
                .FirstOrDefaultAsync(m => m.TourId == id);
            if (tour == null)
            {
                return NotFound();
            }

            //return View(tour);
            return RedirectToAction("Index", "Stages", new { id = tour.TourId, name = tour.TourName });
        }

        // GET: Tours/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tours/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TourId,TourName,DurationInDays,Info")] Tour tour)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tour);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tour);
        }

        // GET: Tours/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tour = await _context.Tours.FindAsync(id);
            if (tour == null)
            {
                return NotFound();
            }
            return View(tour);
        }

        // POST: Tours/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TourId,TourName,DurationInDays,Info")] Tour tour)
        {
            if (id != tour.TourId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tour);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TourExists(tour.TourId))
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
            return View(tour);
        }

        // GET: Tours/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tour = await _context.Tours
                .FirstOrDefaultAsync(m => m.TourId == id);
            if (tour == null)
            {
                return NotFound();
            }

            return View(tour);
        }

        // POST: Tours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var tour = await _context.Tours.FindAsync(id);
        //    _context.Tours.Remove(tour);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tourBaseContext = _context.Stages.Where(s => s.TourId == id).Include(s => s.Hotel).Include(s => s.Tour).Include(s => s.Transport);
            var tourBaseContextV = _context.Vouchers.Where(v => v.TourId == id).Include(v => v.Tour).Include(v => v.Tourist);
            var cont = tourBaseContext.ToList();
            var contV = tourBaseContextV.ToList();
            if (cont.Count == 0 && contV.Count == 0)
            {
                var tour = await _context.Tours.FindAsync(id);
                _context.Tours.Remove(tour);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction("ToursDeleteError", "Home");
            }
        }

        public async Task<IActionResult> DeleteKask(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tour = await _context.Tours
                .FirstOrDefaultAsync(m => m.TourId == id);
            if (tour == null)
            {
                return NotFound();
            }

            return View(tour);
        }

        // POST: Tours/Delete/5
        [HttpPost, ActionName("DeleteKask")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DeleteKaskConfirmed(int id)
        {
            var tourBaseContext = _context.Stages.Where(s => s.TourId == id).Include(s => s.Hotel).Include(s => s.Tour).Include(s => s.Transport);
            var tourBaseContextV = _context.Vouchers.Where(v => v.TourId == id).Include(v => v.Tour).Include(v => v.Tourist);
            var cont = tourBaseContext.ToList();
            var contV = tourBaseContextV.ToList();
            var tour = await _context.Tours.FindAsync(id);
            _context.Tours.Remove(tour);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TourExists(int id)
        {
            return _context.Tours.Any(e => e.TourId == id);
        }
    }
}
