using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projekt.Data;
using Projekt.Models;
using System.Data;

namespace Projekt.Controllers
{
    public class ProducerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProducerController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Producer;
            return View(await appDbContext.ToListAsync());
        }

        public async Task<IActionResult> Info(int? id)
        {
            if(id == null)
            {
                return View(nameof(NotFound));
            }

            var producer = await _context.Producer.FirstOrDefaultAsync(p => p.ProducerId== id);

            if (producer == null)
            {
                return View(nameof(NotFound));
            }

            return View(producer);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("ProducerId","Name","Country","YearOfFoundation")] Producer producer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(producer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }


        public async Task<IActionResult> Edit(int? id)
        {
            
            if (id == null) { return View(nameof(NotFound)); }

            var producer = await _context.Producer.FirstOrDefaultAsync(p => p.ProducerId == id);
            if (producer == null) { return View(nameof(NotFound)); }

            return View(producer);
        }

        [HttpPost, ActionName("Edit")]
        public async Task<IActionResult> EditPost(int? id)
        {
            if(id == null)
            { return View(nameof(NotFound)); }


            var producerToUpdate = await _context.Producer.FirstOrDefaultAsync(p => p.ProducerId == id);

            if (await TryUpdateModelAsync<Producer>(producerToUpdate,
                "",
                p => p.Name, p => p.Country, p => p.YearOfFoundation))
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

            return View(producerToUpdate);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return View();
            }

            var producer = await _context.Producer.FirstOrDefaultAsync(p => p.ProducerId == id);
            if (producer == null) {
                return View();
            }

            return View(Index());
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producer = await _context.Producer.FindAsync(id);
            _context.Producer.Remove(producer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        private bool ProducerExists(int id)
        {
            return _context.Producer.Any(p => p.ProducerId == id);
        }

    }
}
