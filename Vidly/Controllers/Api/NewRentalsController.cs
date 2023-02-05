using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Web;
//using System.Data.Entity;
using Vidly.Models;
//using System.Web.Http;
using Vidly.Migrations;
using Vidly.Dtos;
using AutoMapper;
using System.Data.Entity;


namespace Vidly.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewRentalsController : ControllerBase
    {
        private IdentityModels _context;

        public readonly IMapper _mapper;

        public NewRentalsController(IMapper mapper)
        {
            _context = new IdentityModels();
            _mapper = mapper;
        }


        //POST /api/rentals
        [HttpPost]
        public IActionResult CreateNewRentals(NewRentalDto newRental)
        {
            //if(newRental.MovieIds.Count == 0)
            //{
            //    return BadRequest("No Movie Ids have been giv");
            //}

            var customer = _context.Customers.Single(
                c => c.Id == newRental.CustomerId);

            //if (customer == null)
            //{
            //    return BadRequest("Customer is not valid");
            //}

            var movies= _context.Movies.Where(
                m => newRental.MovieIds.Contains(m.Id)).ToList();

            //if(movies.Count != newRental.MovieIds.Count)
            //{
            //    return BadRequest("One or more Movie Ids are invalid.");
            //}

            foreach(var movie in movies)
            {

                if(movie.NumberAvailable == 0)
                {
                    return BadRequest("Movie is not available.");
                }

                movie.NumberAvailable--;

                var rental = new Rental
                {
                    Customer = customer,
                    Movie = movie,
                    DateRented= DateTime.Now

                };
                _context.Rentals.Add(rental);
            }
            _context.SaveChanges();
            return Ok();
        }
    }
}
