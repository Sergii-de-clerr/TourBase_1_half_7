using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace TourBase_Stage_1_2.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ChartsController : ControllerBase
    {
        private readonly TourBaseContext _context;

        public ChartsController(TourBaseContext context)
        {
            _context = context;
        }

        [HttpGet("JsonTours")]
        public JsonResult JsonTours()
        {
            var tourists = _context.Tours.Include(b => b.Vouchers).ToList();

            List<object> catBook = new List<object>();

            catBook.Add(new[] { "Тур", "Кількість ваучерів" });

            foreach (var c in tourists)
            {
                catBook.Add(new object[] { c.TourName, c.Vouchers.Count() });
            }
            return new JsonResult(catBook);
        }

        [HttpGet("JsonTourists")]
        public JsonResult JsonTourists()
        {
            var tourists = _context.Tourists.Include(b => b.Vouchers).ToList();

            List<object> catBook = new List<object>();

            catBook.Add(new[] { "Тур", "Кількість ваучерів" });

            foreach (var c in tourists)
            {
                catBook.Add(new object[] { c.TouristName, c.Vouchers.Count() });
            }
            return new JsonResult(catBook);
        }
    }
}
