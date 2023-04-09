using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.ModelBinding;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class MoviesController : ApiController
    {
        private readonly ApplicationDbContext _context;
        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }
        // GET api/<controller>
        public IHttpActionResult GetMovies()
        {
            return Ok(_context.Movies.ToList().Select(Mapper.Map<MovieDto>));
        }

        // GET api/<controller>/5
        public IHttpActionResult GetMovie(int id)
        {
            var movie = _context.Movies.FirstOrDefault(x => x.Id == id);
            if (movie == null)
                return NotFound();
            return Ok(Mapper.Map<MovieDto>(movie));
        }

        // POST api/<controller>
        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            movieDto.DateAdded = DateTime.Now;
            var movie = Mapper.Map<Movie>(movieDto);
            _context.Movies.Add(movie);
            _context.SaveChanges();
            return Created(new Uri(Request.RequestUri + "/" + movie.Id),movie);
        }

        // PUT api/<controller>/5
        [HttpPut]
        public void UpdateMovie(int id, MovieDto movieDto)
        {
            if(!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            var movie = _context.Movies.FirstOrDefault(m => m.Id == id);
            if(movie == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            movieDto.DateAdded = DateTime.Now;
            Mapper.Map(movieDto, movie);
            _context.SaveChanges();
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        public void DeleteMovie(int id)
        {
            var movie = _context.Movies.FirstOrDefault(m => id == m.Id);
            if(movie == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            _context.Movies.Remove(movie);
            _context.SaveChanges();
        }
    }
}