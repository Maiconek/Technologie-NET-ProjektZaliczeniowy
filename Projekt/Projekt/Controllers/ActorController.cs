using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projekt.Data;
using Projekt.Models;

namespace Projekt.Controllers
{
    public class ActorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ActorController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Actor.ToList();
            IEnumerable<Actor> actors =
                (from actor in appDbContext
                 orderby actor.YearOfBirth descending
                 select actor);
            return View(actors);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ActorId","FirstName","LastName","YearOfBirth")] Actor actor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(actor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();

        }

        public async Task<IActionResult> Info(int? id)
        {
            if (id == null)
            {
                return View(nameof(NotFound));
            }

            var actor = await _context.Actor.FirstOrDefaultAsync(p => p.ActorId == id);

            if (actor == null)
            {
                return View(nameof(NotFound));
            }

            return View(actor);

        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return View(nameof(NotFound));
            }

            var actor = await _context.Actor.FirstOrDefaultAsync(a => a.ActorId == id);
            if (actor == null)
            {
                return View(nameof(NotFound));
            }

            return View(Index());
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var actor = await _context.Actor.FindAsync(id);
            if (actor == null)
            {
                return View(nameof(NotFound));
            }
            _context.Actor.Remove(actor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
