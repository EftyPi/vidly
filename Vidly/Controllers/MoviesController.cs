using System;
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
        // GET: Movies
        public ActionResult Index()
        {
            return View(GetMovies());
        }


        public ActionResult Random()
        {
            Movie movie = new Movie() { Name = "Shrek!" };
            // not a go way because it is dirty code and introduces magic strings
            // ViewData["Movie"] = movie;
            //  ViewBag.Movie = movie;

            List<Customer> customers = new List<Customer>()
            {
                new Customer { Name = "Customer 1" },
                new Customer { Name = "Customer 2" },
            };

            RandomMovieViewModel viewModel = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customers
            };

            // if we want to send the model
            // return View(movie);
            // if we want to send the view model
            return View(viewModel);
        }

        public ActionResult Edit(int id)
        {
            return Content("id=" + id);
        }




        [Route("movies/released/{year}/{month:regex(\\d{2}:range(1,12))}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }

        private IEnumerable<Movie> GetMovies()
        {
            return new List<Movie>()
            {
                new Movie { Id = 1, Name = "Lost" },
                new Movie { Id = 2, Name = "Shrek!" },
            };
        }
    }
}