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
    // all of the routes of this controller require the user to be logged in to access them
    // [Authorize]
    [Authorize(Roles = RoleName.CAN_MANAGE)]
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Customers
        public ActionResult Index()
        {
            // we get the list from ajax
            // to execute the database query must convert it ToList()
            // IEnumerable<Customer> customers = _context.Customers.Include(c => c.MembershipType).ToList();
            return View();
        }

        // requires the user to be logged in to access that route
        // [Authorize]
        public ActionResult New()
        {
            // get the membership types from the database
            IEnumerable<MembershipType> membershipTypes = _context.MembershipTypes.ToList();
            CustomerFormViewModel viewModel = new CustomerFormViewModel
            {
                MembershipTypes = membershipTypes,
                Customer = new Customer()
            };
            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            // check if the form is valid
            if (!ModelState.IsValid)
            {
                CustomerFormViewModel viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };
                return View("CustomerForm", viewModel);
            }
            // check if we need to create or update
            if (customer.Id== 0)
            {
                // create
                // add to context before adding to the database
                _context.Customers.Add(customer);
            }
            else
            {
                // update
                Customer customerInDb = _context.Customers.Single(c => c.Id == customer.Id);
                // option 1: not a good option because it is prone to exposing the database
                // TryValidateModel(customerInDb);
                // better option 
                customerInDb.Name = customer.Name;
                customerInDb.BirthDate = customer.BirthDate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            }
            
            // add to db
            _context.SaveChanges();
            return RedirectToAction("Index", "Customers");
        }

        public ActionResult Edit(int id)
        {
            Customer customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            CustomerFormViewModel viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes
            };
            
            // specify name otherwise it will look for a view with a name 'Edit'
            return View("CustomerForm", viewModel);
        }

        public ActionResult Details(int id)
        {
            Customer customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

    }
}