using Microsoft.AspNetCore.Mvc;
using Projekt.Data;
using Projekt.Models;
using System.Diagnostics;

namespace Projekt.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.Producer = _context.Producer.Count();
            ViewBag.Movie = _context.Movie.Count();
            ViewBag.Actor = _context.Actor.Count();
            return View();
        }

        public IActionResult Informations() 
        { 
            return View(); 
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public ActionResult YoungProducers()
        {
            var producers = _context.Producer.ToList();
            IEnumerable<Producer> ProducersYoungest =
                (from prod in producers
                 where prod.YearOfFoundation > 2000
                 orderby prod.YearOfFoundation descending
                 select prod
            );

            return PartialView("YoungProducers", ProducersYoungest.ToList());
        }

        public ActionResult NolanMovies() 
        {
            var movies = _context.Movie.ToList();
            IEnumerable<Movie> MoviesOfNolan =
                (from movie in movies
                 where movie.Director.Equals("Christopher Nolan")
                 orderby movie.YearOfProduction descending
                 select movie
                 );
            return PartialView("NolanMovies", MoviesOfNolan.ToList());
        }
    }
}