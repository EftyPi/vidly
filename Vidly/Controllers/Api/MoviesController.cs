﻿using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;
using AutoMapper;

namespace Vidly.Controllers.Api
{
    public class MoviesController : ApiController
    {
        private ApplicationDbContext _context;
        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        // GET /api/movies
        public IHttpActionResult GetMovies()
        {
            IEnumerable<MovieDto> moviesDto = _context.Movies
                .Include(m => m.Genre)
                .ToList()
                .Select(Mapper.Map<Movie, MovieDto>);
            return Ok(moviesDto);
        } 


        // GET /api/movies/:id
        public IHttpActionResult GetMovie(int id)
        {
            Movie movie = _context.Movies.Single(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }
            return Ok(Mapper.Map<Movie, MovieDto>(movie));
        }

        // POST /api/movies
        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            Movie movie = Mapper.Map<MovieDto, Movie>(movieDto);
            _context.Movies.Add(movie);
            _context.SaveChanges();

            return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDto);
        }

        // PUT /api/movies/:id
        [HttpPut]
        public IHttpActionResult UpdateMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            // update
            Movie movieInDb = _context.Movies.Single(c => c.Id == id);
            if (movieInDb == null)
            {
                return NotFound();
            }
            Mapper.Map(movieDto, movieInDb);

            _context.SaveChanges();

            return Ok();
        }

        // DELETE /api/movies/:id
        [HttpDelete]
        public IHttpActionResult DeleteMovie(int id)
        {
            Movie movieInDb = _context.Movies.Single(c => c.Id == id);
            if (movieInDb == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movieInDb);
            _context.SaveChanges();
            return Ok();
        }
    }
}