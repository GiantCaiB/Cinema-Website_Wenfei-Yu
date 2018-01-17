using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieCineplex.Models;
using Microsoft.AspNetCore.Http;


namespace MovieCineplex.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly ABCDataContext _context;

        public ReservationsController(ABCDataContext context)
        {
            _context = context;    
        }

        // GET: Reservations
        public async Task<IActionResult> UserResInfo()
        {
            String userName = HttpContext.Session.GetString("userName");
            Enquiry enquiry = await _context.Enquiry.SingleOrDefaultAsync(m => m.Email == userName);
            if (enquiry == null)
            {
                Enquiry en = new Enquiry();
                en.Email = userName;
                en.Message = "";
                _context.Add(en);
                await _context.SaveChangesAsync();
                enquiry = await _context.Enquiry.SingleOrDefaultAsync(m => m.Email == userName);
            }
            ViewData["userid"] = enquiry.EnquiryId;

            var aBCDataContext = _context.Reservation.Where(e => e.EnquiryId == enquiry.EnquiryId).Include(r => r.Session)
                .ThenInclude(s => s.Movie).Include(r => r.Session).ThenInclude(s => s.Cineplex);
            return View(await aBCDataContext.ToListAsync());
        }

        public async Task<IActionResult> Index()
        {
            var aBCDataContext = _context.Reservation.Where(e => e.EnquiryId == 3).Include(r => r.Session)
                .ThenInclude(s=>s.Movie).Include(r => r.Session).ThenInclude(s =>s.Cineplex);
            List<Reservation> res = await aBCDataContext.Where(e => e.EnquiryId == 3).ToListAsync();
            ViewData["itemnumber"]=res.Count;
            return View(await aBCDataContext.ToListAsync());
        }

        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation.SingleOrDefaultAsync(m => m.ReservationId == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: Reservations/Create
        public IActionResult Create()
        {
            ViewData["EnquiryId"] = new SelectList(_context.Enquiry, "EnquiryId", "Email");
            ViewData["SessionId"] = new SelectList(_context.SessionTime, "SessionId", "SessionId");
            return View();
        }

        public async Task<IActionResult> CreateNew(int? id)
        {
            ViewData["sessionid"] = id;
            /*
            String userName = HttpContext.Session.GetString("userName");
            Enquiry enquiry = await _context.Enquiry.SingleOrDefaultAsync(m => m.Email == userName);
            if (enquiry == null)
            {
                Enquiry en = new Enquiry();
                en.Email = userName;
                en.Message = "";
                _context.Add(en);
                await _context.SaveChangesAsync();
                enquiry = await _context.Enquiry.SingleOrDefaultAsync(m => m.Email == userName);
            }
            ViewData["userid"] = enquiry.EnquiryId;
            */
            ViewData["userid"] = 3;
            List<int> seats = new List<int>();
            for (int i=1;i<21;i++)
            {
                seats.Add(i);
            }
            
            var aBCDataContext = _context.Reservation.Where(e => e.SessionId == id);
            List<Reservation> res = await aBCDataContext.ToListAsync();
            if (res != null)
            {
                foreach(Reservation re in res)
                {
                    seats.Remove(re.SeatId);
                }
            }

            List<Reservation> cap = await _context.Reservation.Where(c=>c.EnquiryId==3).ToListAsync();
            ViewData["Cap"] = cap.Count;

            ViewData["SeatId"] = new SelectList(seats);
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReservationId,EnquiryId,SeatId,SessionId")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["EnquiryId"] = new SelectList(_context.Enquiry, "EnquiryId", "Email", reservation.EnquiryId);
            ViewData["SessionId"] = new SelectList(_context.SessionTime, "SessionId", "SessionId", reservation.SessionId);
            return View(reservation);
        }

        public async Task<IActionResult> AddToCart([Bind("ReservationId,EnquiryId,SeatId,SessionId")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                //Reservation re =HttpContext.Session("hk") as Reservation;
                return RedirectToAction("Index");
            }
            ViewData["EnquiryId"] = new SelectList(_context.Enquiry, "EnquiryId", "Email", reservation.EnquiryId);
            ViewData["SessionId"] = new SelectList(_context.SessionTime, "SessionId", "SessionId", reservation.SessionId);
            return View(reservation);
        }
        /**
        // GET: Reservations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation.SingleOrDefaultAsync(m => m.ReservationId == id);
            if (reservation == null)
            {
                return NotFound();
            }
            ViewData["EnquiryId"] = new SelectList(_context.Enquiry, "EnquiryId", "Email", reservation.EnquiryId);
            ViewData["SessionId"] = new SelectList(_context.SessionTime, "SessionId", "SessionId", reservation.SessionId);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit()
        {
            var aBCDataContext = _context.Reservation.Where(e => e.EnquiryId == 3);
            List<Reservation> res = await aBCDataContext.ToListAsync();

            String userName = HttpContext.Session.GetString("userName");
            Enquiry enquiry = await _context.Enquiry.SingleOrDefaultAsync(m => m.Email == userName);
            if (enquiry == null)
            {
                Enquiry en = new Enquiry();
                en.Email = userName;
                en.Message = "";
                _context.Add(en);
                await _context.SaveChangesAsync();
                enquiry = await _context.Enquiry.SingleOrDefaultAsync(m => m.Email == userName);
            }
            ViewData["userid"] = enquiry.EnquiryId;

            foreach (Reservation reservation in res) {
                reservation.EnquiryId= enquiry.EnquiryId;
                try
                {
                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.ReservationId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
                return RedirectToAction("UserResInfo");

        }

        // GET: Reservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation.SingleOrDefaultAsync(m => m.ReservationId == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await _context.Reservation.SingleOrDefaultAsync(m => m.ReservationId == id);
            _context.Reservation.Remove(reservation);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Checkout(int? tickettype, int? suncount)
        {
            if (HttpContext.Session.GetString("userName") == null)
                ViewData["logintest"] = "0";
            else
                ViewData["logintest"] = "1";

            if (tickettype == 1)
            {
                suncount = suncount * 45;
            }
            else
            {
                suncount = suncount * 20;
            }
            ViewData["pay"] = suncount;
            return View();
        }

        public async Task<IActionResult> UserCenter()
        {
            if (HttpContext.Session.GetString("userName") == null)
                ViewData["logintest"] = "0";
            else
                ViewData["logintest"] = "1";

            return View();
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservation.Any(e => e.ReservationId == id);
        }
    }
}
