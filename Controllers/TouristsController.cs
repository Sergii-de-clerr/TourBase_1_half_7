using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TourBase_Stage_1_2;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using System.IO;
using ClosedXML.Excel;

namespace TourBase_Stage_1_2.Controllers
{
    public class TouristsController : Controller
    {
        //bool Contains(string s, string value)
        //{
        //    if(s.Length > value.Length)
        //        return false;
        //    int dist = (value.Length - s.Length);
        //    string val;
        //    for (int i = 0; i < dist; i++)
        //    {
        //        for (int j = 0; j < s.Length)
        //    }
        //    return false;
        //}

        private readonly TourBaseContext _context;

        public TouristsController(TourBaseContext context)
        {
            _context = context;
        }

        // GET: Tourists
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tourists.ToListAsync());
        }

        // GET: Tourists/Details/5
        public async Task<IActionResult> Details(int? id, string? name)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourist = await _context.Tourists
                .FirstOrDefaultAsync(m => m.TouristId == id);
            if (tourist == null)
            {
                return NotFound();
            }

            //return View(tourist);
            return RedirectToAction("Index", "Vouchers", new { id = tourist.TouristId, name = tourist.TouristName });
        }

        // GET: Tourists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tourists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TouristId,TouristName,BirthDate,EmailAdress")] Tourist tourist)
        {
            if (string.IsNullOrEmpty(tourist.EmailAdress))
            {
                ModelState.AddModelError("EmailAdress", "Це поле повинно бути заповнене");
            }
            else
            {
                Regex regex = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", RegexOptions.CultureInvariant | RegexOptions.Singleline);
                bool isValidEmail = regex.IsMatch(tourist.EmailAdress);
                if (!isValidEmail)
                {
                    ModelState.AddModelError("EmailAdress", "Невірний запис e-mail адреси");
                }
            }

            DateTime t = Convert.ToDateTime("01.01.1920");
            if (tourist.BirthDate < t)
            {
                ModelState.AddModelError("BirthDate", "Некоректний вік");
            }

            if (ModelState.IsValid)
            {
                _context.Add(tourist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tourist);
        }

        // GET: Tourists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourist = await _context.Tourists.FindAsync(id);
            if (tourist == null)
            {
                return NotFound();
            }
            return View(tourist);
        }

        // POST: Tourists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TouristId,TouristName,BirthDate,EmailAdress")] Tourist tourist)
        {
            if (string.IsNullOrEmpty(tourist.EmailAdress))
            {
                ModelState.AddModelError("EmailAdress", "Це поле повинно бути заповнене");
            }
            else
            {
                Regex regex = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", RegexOptions.CultureInvariant | RegexOptions.Singleline);
                bool isValidEmail = regex.IsMatch(tourist.EmailAdress);
                if (!isValidEmail)
                {
                    ModelState.AddModelError("EmailAdress", "Невірний запис e-mail адреси");
                }
            }

            if (id != tourist.TouristId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tourist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TouristExists(tourist.TouristId))
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
            return View(tourist);
        }

        // GET: Tourists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourist = await _context.Tourists
                .FirstOrDefaultAsync(m => m.TouristId == id);
            if (tourist == null)
            {
                return NotFound();
            }

            return View(tourist);
        }

        // POST: Tourists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var tourist = await _context.Tourists.FindAsync(id);
        //    _context.Tourists.Remove(tourist);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tourBaseContext = _context.Vouchers.Where(v => v.TouristId == id).Include(v => v.Tour).Include(v => v.Tourist);
            var cont = tourBaseContext.ToList();
            if (cont.Count == 0)
            {
                var tourist = await _context.Tourists.FindAsync(id);
                _context.Tourists.Remove(tourist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else 
            {
                return RedirectToAction("TouristsDeleteError", "Home");
            }
        }

        // GET: Tourists/Delete/5
        public async Task<IActionResult> DeleteKask(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourist = await _context.Tourists
                .FirstOrDefaultAsync(m => m.TouristId == id);
            if (tourist == null)
            {
                return NotFound();
            }

            return View(tourist);
        }

        // POST: Tourists/Delete/5
        [HttpPost, ActionName("DeleteKask")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DeleteKaskConfirmed(int id)
        {
            var tourBaseContext = _context.Vouchers.Where(v => v.TouristId == id).Include(v => v.Tour).Include(v => v.Tourist);
            var cont = tourBaseContext.ToList();
            var tourist = await _context.Tourists.FindAsync(id);
            _context.Tourists.Remove(tourist);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TouristExists(int id)
        {
            return _context.Tourists.Any(e => e.TouristId == id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Import(IFormFile fileExcel)
        {
            if (ModelState.IsValid)
            {
                if (fileExcel != null)
                {
                    using (var stream = new FileStream(fileExcel.FileName, FileMode.Create))
                    {
                        var tourBaseContextTourist = _context.Tourists.ToList();
                        foreach (var t in tourBaseContextTourist)
                        {
                            await DeleteKask(t.TouristId);
                        }
                        await fileExcel.CopyToAsync(stream);
                        using (XLWorkbook workBook = new XLWorkbook(stream, XLEventTracking.Disabled))
                        {
                            //перегляд усіх листів (в даному випадку категорій)
                            foreach (IXLWorksheet worksheet in workBook.Worksheets)
                            {
                                //worksheet.Name - назва категорії. Пробуємо знайти в БД, якщо відсутня, то створюємо нову
                                Tourist newcat;
                                var c = (from cat in _context.Tourists
                                         where cat.TouristName.Contains(worksheet.Name)
                                         select cat).ToList();
                                if (c.Count > 0)
                                {
                                    newcat = c[0];
                                }
                                else
                                {
                                    newcat = new Tourist();
                                    newcat.TouristName = worksheet.Cell(1, 1).Value.ToString();
                                    newcat.BirthDate = Convert.ToDateTime(worksheet.Cell(1, 2).Value);
                                    newcat.EmailAdress = worksheet.Cell(1, 3).Value.ToString();
                                    //додати в контекст
                                    _context.Tourists.Add(newcat);
                                }
                                //перегляд усіх рядків                    
                                foreach (IXLRow row in worksheet.RowsUsed().Skip(1))
                                {
                                    try
                                    {
                                        Voucher voucher = new Voucher();
                                        voucher.TakeOffDate = Convert.ToDateTime(row.Cell(2).Value);
                                        var tourBaseContextT = _context.Tours.Where(v => v.TourName == row.Cell(3).Value.ToString()).ToList();
                                        voucher.TourId = tourBaseContextT[0].TourId;
                                        voucher.TouristId = newcat.TouristId;
                                        voucher.Tour = tourBaseContextT[0];
                                        voucher.Tourist = newcat;
                                        _context.Vouchers.Add(voucher);
                                        //у разі наявності автора знайти його, у разі відсутності - додати
                                        //for (int i = 2; i <= 5; i++)
                                        //{
                                        //    if (row.Cell(i).Value.ToString().Length > 0)
                                        //    {
                                        //        Author author;

                                        //        var a = (from aut in _context.Authors
                                        //                 where aut.Name.Contains(row.Cell(i).Value.ToString())
                                        //                 select aut).ToList();
                                        //        if (a.Count > 0)
                                        //        {
                                        //            author = a[0];
                                        //        }
                                        //        else
                                        //        {
                                        //            author = new Author();
                                        //            author.Name = row.Cell(i).Value.ToString();
                                        //            author.Info = "from EXCEL";
                                        //            //додати в контекст
                                        //            _context.Add(author);
                                        //        }
                                        //        AuthorsBooks ab = new AuthorsBooks();
                                        //        ab.Book = book;
                                        //        ab.Author = author;
                                        //        _context.AuthorsBooks.Add(ab);
                                        //    }
                                        //}
                                    }
                                    catch (Exception e)
                                    {
                                        //logging самостійно :)

                                    }
                                }
                            }
                        }
                    }
                }

                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Export()
        {
            using (XLWorkbook workbook = new XLWorkbook(XLEventTracking.Disabled))
            {
                var tourists = _context.Tourists.Include("Vouchers").ToList();
                //тут, для прикладу ми пишемо усі книжки з БД, в своїх проектах ТАК НЕ РОБИТИ (писати лише вибрані)
                foreach (var c in tourists)
                {
                    var worksheet = workbook.Worksheets.Add(c.TouristName);

                    worksheet.Cell("A1").Value = "ПІБ туриста";
                    worksheet.Cell("B1").Value = "Дата закінчення дії ваучера";
                    worksheet.Cell("C1").Value = "Назва туру";
                    worksheet.Row(1).Style.Font.Bold = true;
                    var vouchers = c.Vouchers.ToList();

                    var t = vouchers.Count();
                    //нумерація рядків/стовпчиків починається з індекса 1 (не 0)
                    for (int i = 0; i < vouchers.Count(); i++)
                    {
                        worksheet.Cell(i + 2, 1).Value = vouchers[i].TouristId;
                        string takeOffDate = vouchers[i].TakeOffDate.ToString();
                        worksheet.Cell(i + 2, 2).Value = takeOffDate;
                        worksheet.Cell(i + 2, 3).Value = vouchers[i].TourId;

                        var tourists_v = _context.Tourists.Where(a => a.TouristId == vouchers[i].TouristId).ToList();
                        var tours_v = _context.Tours.Where(a => a.TourId == vouchers[i].TourId).ToList();
                        //більше 4 - ох нікуди писати

                        worksheet.Cell(i + 2, 1).Value = tourists_v[0].TouristName;

                        worksheet.Cell(i + 2, 3).Value = tours_v[0].TourName;
                    }
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    stream.Flush();

                    return new FileContentResult(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        FileDownloadName = $"library_{DateTime.UtcNow.ToShortDateString()}.xlsx"
                    };
                }
            }
        }

    }
}
