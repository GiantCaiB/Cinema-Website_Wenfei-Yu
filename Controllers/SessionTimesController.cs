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
    public class SessionTimesController : Controller
    {
        private readonly ABCDataContext _context;

        public SessionTimesController(ABCDataContext context)
        {
            _context = context;    
        }

        // GET: SessionTimes
        public async Task<IActionResult> Index(int? movie_id, int? cineplex_id)
        {
            var aBCDataContext = _context.SessionTime.Include(s => s.Cineplex).Include(s => s.Movie);
            return View(await aBCDataContext.Where(c =>c.CineplexId== cineplex_id&&c.MovieId== movie_id)
                .ToListAsync());
        }

        // GET: SessionTimes/Details/5
        public async Task<IActionResult> Details(int? movie_id,int? cineplex_id)
        {
            if (movie_id == null|| cineplex_id==null)
            {
                return NotFound();
            }

            var sessionTime = await _context.SessionTime
                .Include(m => m.Movie).Include(c =>c.Cineplex)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.MovieId == movie_id && m.CineplexId == cineplex_id);
            if (sessionTime == null)
            {
                return NotFound();
            }

            return View(sessionTime);
        }

        // GET: SessionTimes/Create
        public IActionResult Create()
        {
            ViewData["CineplexId"] = new SelectList(_context.Cineplex, "CineplexId", "Location");
            ViewData["MovieId"] = new SelectList(_context.Movie, "MovieId", "LongDescription");
            return View();
        }

        public IActionResult CreateNew(int? movie, int? cineplex,DateTime time)
        {

            ViewData["CineplexId"] = cineplex;
            ViewData["MovieId"] = movie;
            ViewData["Time"] = time;

            return View();
        }

        // POST: SessionTimes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SessionId,CineplexId,MovieId,MovieTime")] SessionTime sessionTime)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sessionTime);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["CineplexId"] = new SelectList(_context.Cineplex, "CineplexId", "Location", sessionTime.CineplexId);
            ViewData["MovieId"] = new SelectList(_context.Movie, "MovieId", "LongDescription", sessionTime.MovieId);
            return View(sessionTime);
        }

        // GET: SessionTimes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sessionTime = await _context.SessionTime.SingleOrDefaultAsync(m => m.SessionId == id);
            if (sessionTime == null)
            {
                return NotFound();
            }
            ViewData["CineplexId"] = new SelectList(_context.Cineplex, "CineplexId", "Location", sessionTime.CineplexId);
            ViewData["MovieId"] = new SelectList(_context.Movie, "MovieId", "LongDescription", sessionTime.MovieId);
            return View(sessionTime);
        }

        // POST: SessionTimes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SessionId,CineplexId,MovieId,MovieTime")] SessionTime sessionTime)
        {
            if (id != sessionTime.SessionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sessionTime);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SessionTimeExists(sessionTime.SessionId))
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
            ViewData["CineplexId"] = new SelectList(_context.Cineplex, "CineplexId", "Location", sessionTime.CineplexId);
            ViewData["MovieId"] = new SelectList(_context.Movie, "MovieId", "LongDescription", sessionTime.MovieId);
            return View(sessionTime);
        }

        // GET: SessionTimes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sessionTime = await _context.SessionTime.SingleOrDefaultAsync(m => m.SessionId == id);
            if (sessionTime == null)
            {
                return NotFound();
            }

            return View(sessionTime);
        }

        // POST: SessionTimes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sessionTime = await _context.SessionTime.SingleOrDefaultAsync(m => m.SessionId == id);
            _context.SessionTime.Remove(sessionTime);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool SessionTimeExists(int id)
        {
            return _context.SessionTime.Any(e => e.SessionId == id);
        }
    }
}
