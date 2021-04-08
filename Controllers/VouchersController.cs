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
    public class VouchersController : Controller
    {
        private readonly TourBaseContext _context;

        public VouchersController(TourBaseContext context)
        {
            _context = context;
        }

        //GET: Vouchers
        //public async Task<IActionResult> Index()
        //{
        //    var tourBaseContext = _context.Vouchers.Include(v => v.Tour).Include(v => v.Tourist);
        //    return View(await tourBaseContext.ToListAsync());
        //}

        public async Task<IActionResult> Index(int? id, string? name)
        {
            if (id == null)
                return RedirectToAction("Index", "Tourists");
            ViewBag.TouristId = id;
            ViewBag.TouristName = name;
            var tourBaseContext = _context.Vouchers.Where(v => v.TouristId == id).Include(v => v.Tour).Include(v => v.Tourist);
            return View(await tourBaseContext.ToListAsync());
        }

        // GET: Vouchers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voucher = await _context.Vouchers
                .Include(v => v.Tour)
                .Include(v => v.Tourist)
                .FirstOrDefaultAsync(m => m.VoucherId == id);
            if (voucher == null)
            {
                return NotFound();
            }

            return View(voucher);
        }

        // GET: Vouchers/Create
        public IActionResult Create(int touristid)
        {
            ViewData["TourId"] = new SelectList(_context.Tours, "TourId", "TourName");
            ViewData["TouristId"] = new SelectList(_context.Tourists, "TouristId", "TouristName");
            return View();
        }

        // POST: Vouchers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int touristid, [Bind("VoucherId,TakeOffDate,TouristId,TourId")] Voucher voucher)
        {
            var tourBaseContextV = _context.Vouchers.Include(v => v.Tour).Include(v => v.Tourist).ToList();
            var tourBaseContextT = _context.Tours.ToList();
            Dictionary<int, int> TOURS = new Dictionary<int, int>();
            foreach (var a in tourBaseContextT)
            {
                TOURS.Add(a.TourId, (int)a.DurationInDays);
            }
            DateTime t;
            foreach (var a in tourBaseContextV)
            {
                t = Convert.ToDateTime(value: a.TakeOffDate);
                t = t.AddDays(TOURS[(int)voucher.TourId]);
                if ((voucher.TakeOffDate > a.TakeOffDate) && (voucher.TakeOffDate < t))
                {
                    ModelState.AddModelError("TakeOffDate", "Ви в турі в цей час");
                }
            }

            voucher.TouristId = touristid;
            if (ModelState.IsValid)
            {
                _context.Add(voucher);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "Vouchers", new { id = touristid });
            }
            ViewData["TourId"] = new SelectList(_context.Tours, "TourName", "TourName", voucher.TourId);
            ViewData["TouristId"] = new SelectList(_context.Tourists, "TouristName", "TouristName", voucher.TouristId);
            return View(voucher);
            //return RedirectToAction("Create", "Vouchers", new { id = touristid });
        }

        // GET: Vouchers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voucher = await _context.Vouchers.FindAsync(id);
            if (voucher == null)
            {
                return NotFound();
            }
            ViewData["TourId"] = new SelectList(_context.Tours, "TourId", "TourId", voucher.TourId);
            ViewData["TouristId"] = new SelectList(_context.Tourists, "TouristId", "TouristId", voucher.TouristId);
            return View(voucher);
        }

        // POST: Vouchers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VoucherId,TakeOffDate,TouristId,TourId")] Voucher voucher)
        {
            if (id != voucher.VoucherId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(voucher);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VoucherExists(voucher.VoucherId))
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
            ViewData["TourId"] = new SelectList(_context.Tours, "TourId", "TourName", voucher.TourId);
            ViewData["TouristId"] = new SelectList(_context.Tourists, "TouristId", "TouristName", voucher.TouristId);
            return View(voucher);
        }

        // GET: Vouchers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var voucher = await _context.Vouchers
                .Include(v => v.Tour)
                .Include(v => v.Tourist)
                .FirstOrDefaultAsync(m => m.VoucherId == id);
            if (voucher == null)
            {
                return NotFound();
            }

            return View(voucher);
        }

        // POST: Vouchers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var voucher = await _context.Vouchers.FindAsync(id);
            _context.Vouchers.Remove(voucher);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VoucherExists(int id)
        {
            return _context.Vouchers.Any(e => e.VoucherId == id);
        }
    }
}
