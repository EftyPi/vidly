using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
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

        // GET: Movies
        public ActionResult Index()
        {
            IEnumerable<Movie> movies = _context.Movies.Include(m => m.Genre).ToList();
            return View(movies);
        }


        public ActionResult Details(int id)
        {
            Movie movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(c => c.Id == id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        //public ActionResult Random()
        //{
        //    Movie movie = new Movie() { Name = "Shrek!" };
        //    // not a go way because it is dirty code and introduces magic strings
        //    // ViewData["Movie"] = movie;
        //    //  ViewBag.Movie = movie;

        //    List<Customer> customers = new List<Customer>()
        //    {
        //        new Customer { Name = "Customer 1" },
        //        new Customer { Name = "Customer 2" },
        //    };

        //    RandomMovieViewModel viewModel = new RandomMovieViewModel
        //    {
        //        Movie = movie,
        //        Customers = customers
        //    };

        //    // if we want to send the model
        //    // return View(movie);
        //    // if we want to send the view model
        //    return View(viewModel);
        //}

        //public ActionResult Edit(int id)
        //{
        //    return Content("id=" + id);
        //}

        //[Route("movies/released/{year}/{month:regex(\\d{2}:range(1,12))}")]
        //public ActionResult ByReleaseDate(int year, int month)
        //{
        //    return Content(year + "/" + month);
        //}

    }
}