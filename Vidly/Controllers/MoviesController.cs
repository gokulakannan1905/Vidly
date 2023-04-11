using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using System.Data.Entity;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;
        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        //
        // GET: /Movies/
        public ActionResult Index()
        {
            return View();
        }

        [Route("movies/about/{id}")]
        public ActionResult About(int id)
        {
            var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);
            return View(movie);
        }

        public ActionResult New()
        {
            var movieModel = new MovieFormViewModel()
            {
                Genres = _context.Genres.ToList(),
            };
            return View("MovieForm",movieModel);
        }

        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);

            if(movie == null)
                return HttpNotFound();
            var movieModel = new MovieFormViewModel(movie)
            {
                Genres = _context.Genres.ToList(),
            };
            return View("MovieForm", movieModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie) //model binding view --> movie parameter
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel(movie)
                {
                    Genres = _context.Genres.ToList()
                };
                return View("MovieForm", viewModel);
            }
            if(movie.Id == 0)
            {
                _context.Movies.Add(movie);
            }
            else
            {
                var movieInDb = _context.Movies.Single(m=> m.Id == movie.Id);
                movieInDb.Name = movie.Name;
                movieInDb.Stocks = movie.Stocks;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.GenreId = movie.GenreId;
            }
            movie.DateAdded = DateTime.Now;
            _context.SaveChanges();
            return RedirectToAction("Index","Movies");
        }
	}
}