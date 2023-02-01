using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Web;
using Vidly.Models;
using Vidly.Migrations;
using Vidly.Dtos;
using AutoMapper;

namespace Vidly.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private IdentityModels _context;

        public readonly IMapper _mapper;

        public MoviesController(IMapper mapper)
        {
            _context = new IdentityModels();
            _mapper = mapper;
        }

        //GET /api/movies
        [HttpGet]
        public IActionResult GetMovies() 
        {
            var movieDtos = _context.Movies
                    .Include(c => c.Genre)
                    .ToList()
                    .Select(_mapper.Map<Movie, MovieDto>);

            return Ok(movieDtos);
        }

        //GET /api/movies/9
        [HttpGet("{id:int}")]
        public IActionResult GetMovie(int id)
        {
            var movieDto = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (movieDto == null)
                return NotFound();

            return Ok(_mapper.Map<Movie, MovieDto>(movieDto));
        }


        //POST /api/movies
        [HttpPost]
        public IActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movie = _mapper.Map<MovieDto, Movie>(movieDto);

            _context.Movies.Add(movie);
            _context.SaveChanges();

            movieDto.Id =  movie.Id;

            return Created(new Uri(Request.Host.Value + Request.Path.Value + "/" + movie.Id), movieDto);
        }

        //PUT /api/movies/1
        [HttpPut("{id:int}")]
        public void UpdateMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                throw new System.Web.Http.HttpResponseException(HttpStatusCode.BadRequest);

            var movieInDb = _context.Movies.SingleOrDefault(c => c.Id == movieDto.Id);
            //var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (movieInDb == null)
                throw new System.Web.Http.HttpResponseException(HttpStatusCode.NotFound);


            _mapper.Map(movieDto, movieInDb);

            _context.SaveChanges();

        }

        //DELETE /api/movies/1
        [HttpDelete("{id:int}")]
        public void DeleteMovie(int id)
        {
            var movieInDb = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (movieInDb == null)
                throw new System.Web.Http.HttpResponseException(HttpStatusCode.NotFound);

            _context.Movies.Remove(movieInDb);
            _context.SaveChanges();

        }




    }
}
