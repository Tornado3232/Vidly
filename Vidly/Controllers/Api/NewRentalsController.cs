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
        //POST /api/rentals
        [HttpPost]
        public IActionResult CreateNewRentals(NewRentalDto newRental)
        {
            throw new NotImplementedException();
        }
    }
}
