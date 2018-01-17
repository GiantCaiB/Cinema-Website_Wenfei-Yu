using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieCineplex.Models;

namespace MovieCineplex.Controllers
{
    public class EventsInfoesController : Controller
    {
        private readonly ABCDataContext _context;

        public EventsInfoesController(ABCDataContext context)
        {
            _context = context;    
        }

        // GET: EventsInfoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.EventsInfo.ToListAsync());
        }

        // GET: EventsInfoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventsInfo = await _context.EventsInfo.SingleOrDefaultAsync(m => m.EventsId == id);
            if (eventsInfo == null)
            {
                return NotFound();
            }

            return View(eventsInfo);
        }

        // GET: EventsInfoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EventsInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventsId,Information")] EventsInfo eventsInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eventsInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(eventsInfo);
        }

        // GET: EventsInfoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventsInfo = await _context.EventsInfo.SingleOrDefaultAsync(m => m.EventsId == id);
            if (eventsInfo == null)
            {
                return NotFound();
            }
            return View(eventsInfo);
        }

        // POST: EventsInfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventsId,Information")] EventsInfo eventsInfo)
        {
            if (id != eventsInfo.EventsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eventsInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventsInfoExists(eventsInfo.EventsId))
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
            return View(eventsInfo);
        }

        // GET: EventsInfoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventsInfo = await _context.EventsInfo.SingleOrDefaultAsync(m => m.EventsId == id);
            if (eventsInfo == null)
            {
                return NotFound();
            }

            return View(eventsInfo);
        }

        // POST: EventsInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eventsInfo = await _context.EventsInfo.SingleOrDefaultAsync(m => m.EventsId == id);
            _context.EventsInfo.Remove(eventsInfo);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool EventsInfoExists(int id)
        {
            return _context.EventsInfo.Any(e => e.EventsId == id);
        }
    }
}
