using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Controller;
using System.Data.Entity;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private IdentityModels _context;

        public MoviesController()
        {
            _context = new IdentityModels();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult New()
        {
            var genres = _context.Genres.ToList();

            var viewModel = new MovieFormViewModel
            {
                Genres = genres
            };
            return View("MovieForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {

            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel(movie)
                {
                    Genres = _context.Genres.ToList()
                };
                return View("MovieForm", viewModel);
            }

            if (movie.Id == 0)
            {
                movie.DateAdded= DateTime.Now;
                 _context.Movies.Add(movie);
            }
            else
            {
                var movieInDb = _context.Movies.Single(c => c.Id == movie.Id);

                movieInDb.Name = movie.Name;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.NumberInStock = movie.NumberInStock;

            }
            _context.SaveChanges();

            return RedirectToAction("Index", "Movies");
        }

        public IActionResult Index()
        {
            using (_context = new IdentityModels())
            {
                var movies = _context.Movies
                    .Include(c => c.Genre)
                    .ToList();

                return View(movies);
            }
        }

        [Route("movies/details/{id:int}")]
        public IActionResult Details(int id, string name)
        {
            //var movie = new Movie { Id = id, Name = name };
            var movie = _context.Movies
                .Include(c => c.Genre)
                .SingleOrDefault(c => c.Id == id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        [Route("movies/release/{year:min(2000):max(2020)}/{month:range(1,12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }


        public IActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
                return NotFound();

            var viewModel = new MovieFormViewModel(movie)
            {
                Genres = _context.Genres.ToList()
            };

            return View("MovieForm", viewModel);
        }
    } 
}
