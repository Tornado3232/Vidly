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

//using System.Web.Http;

//using Vidly.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Threading.Tasks;

namespace Vidly.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private IdentityModels _context;

        public readonly IMapper _mapper;

        public CustomersController(IMapper mapper)
        {
            _context = new IdentityModels();
            _mapper = mapper;
        }

        //GET /api/customers
        [HttpGet]
        public IActionResult GetCustomers()
        {
            var customerDtos = _context.Customers
                .Include(c => c.MembershipType)
                .ToList()
                .Select(_mapper.Map<Customer, CustomerDto>);
            return Ok(customerDtos);
        }

        //GET /api/customers/9
        [HttpGet("{id:int}")]
        public IActionResult GetCustomer(int id)
        {
            var customerDto = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerDto == null)
                return NotFound();

            return Ok(_mapper.Map<Customer, CustomerDto>(customerDto));
        }

        //POST /api/customers
        [HttpPost]
        public IActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();   

            var customer = _mapper.Map<CustomerDto, Customer>(customerDto);

            _context.Customers.Add(customer);   
            _context.SaveChanges();

            customerDto.Id= customer.Id;

            return Created(new Uri(Request.Host.Value + Request.Path.Value + "/" + customer.Id), customerDto);
        }   

        //PUT /api/customers/1
        [HttpPut("{id:int}")]
        public void UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                throw new System.Web.Http.HttpResponseException(HttpStatusCode.BadRequest);

            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == customerDto.Id);
            //var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if(customerInDb == null)
                throw new System.Web.Http.HttpResponseException(HttpStatusCode.NotFound);


            _mapper.Map(customerDto, customerInDb);
            //customerInDb.Name = customerDto.Name;
            //customerInDb.Birth date = customerDto.Birthdate;
            //customerInDb.IsSubscribedToNewsletter = customerDto.IsSubscribedToNewsletter;
            //customerInDb.MembershipTypeId = customerDto.MembershipTypeId;

            _context.SaveChanges();

        }

        //DELETE /api/customers/1
        [HttpDelete("{id:int}")]
        public void DeleteCustomer(int id)
        {
            var customerInDb = _context?.Customers.SingleOrDefault(c =>c.Id == id);

            if(customerInDb == null)
                throw new System.Web.Http.HttpResponseException(HttpStatusCode.NotFound);
            
            _context.Customers.Remove(customerInDb);
            _context.SaveChanges();

        }
    }
}
