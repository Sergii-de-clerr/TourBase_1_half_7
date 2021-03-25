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
    public class StagesController : Controller
    {
        private readonly TourBaseContext _context;

        public StagesController(TourBaseContext context)
        {
            _context = context;
        }

        // GET: Stages
        //public async Task<IActionResult> Index()
        //{
        //    var tourBaseContext = _context.Stages.Include(s => s.Hotel).Include(s => s.Tour).Include(s => s.Transport);
        //    return View(await tourBaseContext.ToListAsync());
        //}

        public async Task<IActionResult> Index(int? id, string? name)
        {
            if (id == null)
                return RedirectToAction("Index", "Tours");
            ViewBag.TourId = id;
            ViewBag.TourName = name;
            var tourBaseContext = _context.Stages.Where(s => s.TourId == id).Include(s => s.Hotel).Include(s => s.Tour).Include(s => s.Transport);
            return View(await tourBaseContext.ToListAsync());
        }

        // GET: Stages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stage = await _context.Stages
                .Include(s => s.Hotel)
                .Include(s => s.Tour)
                .Include(s => s.Transport)
                .FirstOrDefaultAsync(m => m.StageId == id);
            if (stage == null)
            {
                return NotFound();
            }

            return View(stage);
        }

        // GET: Stages/Create
        public IActionResult Create()
        {
            ViewData["HotelId"] = new SelectList(_context.Hotels, "HotelId", "HotelName");
            ViewData["TourId"] = new SelectList(_context.Tours, "TourId", "TourName");
            ViewData["TransportId"] = new SelectList(_context.Transports, "TransportId", "TransportName");
            return View();
        }

        // POST: Stages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StageId,StageNumber,TourId,TransportId,HotelId")] Stage stage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HotelId"] = new SelectList(_context.Hotels, "HotelId", "HotelId", stage.HotelId);
            ViewData["TourId"] = new SelectList(_context.Tours, "TourId", "TourName", stage.TourId);
            ViewData["TransportId"] = new SelectList(_context.Transports, "TransportId", "TransportId", stage.TransportId);
            return View(stage);
        }

        // GET: Stages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stage = await _context.Stages.FindAsync(id);
            if (stage == null)
            {
                return NotFound();
            }
            ViewData["HotelId"] = new SelectList(_context.Hotels, "HotelId", "HotelName", stage.HotelId);
            ViewData["TourId"] = new SelectList(_context.Tours, "TourId", "TourName", stage.TourId);
            ViewData["TransportId"] = new SelectList(_context.Transports, "TransportId", "TransportName", stage.TransportId);
            return View(stage);
        }

        // POST: Stages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StageId,StageNumber,TourId,TransportId,HotelId")] Stage stage)
        {
            if (id != stage.StageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StageExists(stage.StageId))
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
            ViewData["HotelId"] = new SelectList(_context.Hotels, "HotelId", "HotelId", stage.HotelId);
            ViewData["TourId"] = new SelectList(_context.Tours, "TourId", "TourName", stage.TourId);
            ViewData["TransportId"] = new SelectList(_context.Transports, "TransportId", "TransportId", stage.TransportId);
            return View(stage);
        }

        // GET: Stages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stage = await _context.Stages
                .Include(s => s.Hotel)
                .Include(s => s.Tour)
                .Include(s => s.Transport)
                .FirstOrDefaultAsync(m => m.StageId == id);
            if (stage == null)
            {
                return NotFound();
            }

            return View(stage);
        }

        // POST: Stages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stage = await _context.Stages.FindAsync(id);
            _context.Stages.Remove(stage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StageExists(int id)
        {
            return _context.Stages.Any(e => e.StageId == id);
        }
    }
}
