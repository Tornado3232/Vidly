using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Controller;
using System.Data.Entity;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    [Authorize]
    public class CustomersController : Controller
    {
        private IdentityModels _context;

        public CustomersController()
        {
            _context = new IdentityModels();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();

            var viewModel = new CustomerFormViewModel 
            { 
               Customer = new Customer(),
               MembershipTypes = membershipTypes             
            };
            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer) 
         {
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };
                return View("CustomerForm", viewModel);
            }

            if (customer.Id == 0)
            {
                _context.Customers.Add(customer);
            }
            else
            {
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);

                customerInDb.Name = customer.Name;
                customerInDb.Birthdate = customer.Birthdate;
                customerInDb.MembershipTypeId= customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            
            }
            _context.SaveChanges();

            return RedirectToAction("Index", "Customers");
        }

        public IActionResult Index()
        {

            //using (_context = new IdentityModels())
            //{
            //    var customers = _context.Customers
            //    .Include(c => c.MembershipType)
            //    .ToList();

            //    return View(customers);
            //}
            return View();
        }

        public IActionResult Details(int id)
        {
            var customer = _context.Customers
                .Include(c => c.MembershipType)
                .SingleOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        public IActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return NotFound();

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };

            return View("CustomerForm", viewModel);
        }
    }
}
