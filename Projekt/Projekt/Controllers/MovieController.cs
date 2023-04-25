using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projekt.Data;
using Projekt.Models;
using System.Data;

namespace Projekt.Controllers
{
    public class MovieController : Controller
    {

        private readonly ApplicationDbContext _context;

        public MovieController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Movie;
            return View(await appDbContext.ToListAsync());
        }

        public async Task<IActionResult> Info(int? id)
        {
            if (id == null)
            {
                return View(nameof(NotFound));
            }

            var movie = await _context.Movie.Include(m => m.Producer).FirstOrDefaultAsync(m => m.MovieId == id);

            if (movie == null)
            {
                return View(nameof(NotFound));
            }

            return View(movie);
        }

        public IActionResult Create()
        {
            ViewData["ProducerId"] = new SelectList(_context.Producer, "ProducerId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MovieId","Title","Director","Genre","YearOfProduction","ProducerId")] Movie movie) 
        {
            if(ModelState.IsValid)
            {
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProducerId"] = new SelectList(_context.Producer, "ProducerId", "Name", movie.ProducerId);
            return View();
            
        }

        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null) { return View(nameof(NotFound)); }

            var movie = await _context.Movie.FirstOrDefaultAsync(p => p.MovieId == id);
            if (movie == null) { return View(nameof(NotFound)); }

            ViewData["ProducerId"] = new SelectList(_context.Producer, "ProducerId", "Name");
            return View(movie);
        }

        [HttpPost, ActionName("Edit")]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            { return View(nameof(NotFound)); }


            var movieToUpdate = await _context.Movie.FirstOrDefaultAsync(m => m.MovieId == id);

            if (await TryUpdateModelAsync<Movie>(movieToUpdate,
                "",
                m => m.Title, m => m.Director, m => m.Genre, m => m.YearOfProduction, m => m.ProducerId))
            {
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Nie działa");
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProducerId"] = new SelectList(_context.Producer, "ProducerId", "Name");
            return View(movieToUpdate);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return View(nameof(NotFound));
            }

            var movie = await _context.Movie.FirstOrDefaultAsync(m => m.MovieId == id);
            if (movie == null)
            {
                return View(nameof(NotFound));
            }

            return View(Index());
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movie.FindAsync(id);
            if (movie == null)
            {
                return View(nameof(NotFound));
            }
            _context.Movie.Remove(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            
                return _context.Movie.Any(p => p.MovieId == id);
            
        }
    }
}
