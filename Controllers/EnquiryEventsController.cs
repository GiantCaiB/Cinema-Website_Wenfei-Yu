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
    public class EnquiryEventsController : Controller
    {
        private readonly ABCDataContext _context;

        public EnquiryEventsController(ABCDataContext context)
        {
            _context = context;    
        }

        // GET: EnquiryEvents
        public async Task<IActionResult> Index()
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
            
            var aBCDataContext = _context.EnquiryEvents.Include(e => e.Enquiry).Include(e => e.Events);
            return View(await aBCDataContext.Where(e =>e.EnquiryId== enquiry.EnquiryId).ToListAsync());
        }

        // GET: EnquiryEvents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enquiryEvents = await _context.EnquiryEvents.SingleOrDefaultAsync(m => m.EventsId == id);
            if (enquiryEvents == null)
            {
                return NotFound();
            }

            return View(enquiryEvents);
        }

        // GET: EnquiryEvents/Create
        public async Task<IActionResult> Create(int? id)
        {
            if(HttpContext.Session.GetString("userName")==null)
            ViewData["logintest"] = "0";
            else
                ViewData["logintest"] = "1";
            ViewData["event"] = id;

            EnquiryEvents enquiryEvent = await _context.EnquiryEvents.Include(e =>e.Events)
                .SingleOrDefaultAsync(m => m.EventsId == id);
            EventsInfo even = enquiryEvent.Events;
            ViewData["eventInfo"] = even.Information;

            return View();
        }

        // POST: EnquiryEvents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventsId,EnquiryId")] EnquiryEvents enquiryEvents)
        {
            if (ModelState.IsValid)
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
                enquiryEvents.EnquiryId = enquiry.EnquiryId;

                EnquiryEvents existEvent= await _context.EnquiryEvents.SingleOrDefaultAsync(e=>e.EnquiryId== enquiry.EnquiryId && e.EventsId== enquiryEvents.EventsId);
                if(existEvent!=null)
                    return RedirectToAction("Index");
                _context.Add(enquiryEvents);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["EnquiryId"] = new SelectList(_context.Enquiry, "EnquiryId", "Email", enquiryEvents.EnquiryId);
            ViewData["EventsId"] = new SelectList(_context.EventsInfo, "EventsId", "EventsId", enquiryEvents.EventsId);
            return View(enquiryEvents);
        }

        // GET: EnquiryEvents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enquiryEvents = await _context.EnquiryEvents.SingleOrDefaultAsync(m => m.EventsId == id);
            if (enquiryEvents == null)
            {
                return NotFound();
            }
            ViewData["EnquiryId"] = new SelectList(_context.Enquiry, "EnquiryId", "Email", enquiryEvents.EnquiryId);
            ViewData["EventsId"] = new SelectList(_context.EventsInfo, "EventsId", "EventsId", enquiryEvents.EventsId);
            return View(enquiryEvents);
        }

        // POST: EnquiryEvents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventsId,EnquiryId")] EnquiryEvents enquiryEvents)
        {
            if (id != enquiryEvents.EventsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enquiryEvents);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnquiryEventsExists(enquiryEvents.EventsId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["EnquiryId"] = new SelectList(_context.Enquiry, "EnquiryId", "Email", enquiryEvents.EnquiryId);
            ViewData["EventsId"] = new SelectList(_context.EventsInfo, "EventsId", "EventsId", enquiryEvents.EventsId);
            return View(enquiryEvents);
        }

        // GET: EnquiryEvents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enquiryEvents = await _context.EnquiryEvents.SingleOrDefaultAsync(m => m.EventsId == id);
            if (enquiryEvents == null)
            {
                return NotFound();
            }

            return View(enquiryEvents);
        }

        // POST: EnquiryEvents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var enquiryEvents = await _context.EnquiryEvents.SingleOrDefaultAsync(m => m.EventsId == id);
            _context.EnquiryEvents.Remove(enquiryEvents);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool EnquiryEventsExists(int id)
        {
            return _context.EnquiryEvents.Any(e => e.EventsId == id);
        }
    }
}
