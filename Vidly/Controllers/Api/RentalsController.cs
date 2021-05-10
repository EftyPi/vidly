using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class RentalsController : ApiController
    {
        private ApplicationDbContext _context;
        public RentalsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult CreateRental(RentalDto newRental)
        {
            // get the customer details
            Customer customer = _context.Customers.Single(
                c => c.Id == newRental.CustomerId);
            
            // find all the movies details that will be rented
            List<Movie> movies = _context.Movies.Where(
                m => newRental.MovieIds.Contains(m.Id)).ToList();

            // check if the movies are avalaible
            foreach (Movie movie in movies)
            {
                if (movie.NumberAvailable == 0)
                    return BadRequest("Movie is not available.");

                movie.NumberAvailable--;

                // create and add the new rental
                Rental rental = new Rental
                {
                    Customer = customer,
                    Movie = movie,
                    DateRented = DateTime.Now
                };

                _context.Rentals.Add(rental);
            }
            // add it to the db
            _context.SaveChanges();
            return Ok();
        }
    }
}