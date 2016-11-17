using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        // GET: Customer
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }


        public ActionResult New()
        {
            var membershipTypes = _context.MemberShipTypes.ToList();
            var viewmodel = new CustomerFormViewModel { 
                MemberShipTypes=membershipTypes
            };
            return View("CustomerForm", viewmodel);
        }

        [HttpPost]
        public ActionResult Save(Customer customer)
        {
            if (customer.Id==0)
                _context.Customers.Add(customer);
            else {
                var customerInDb = _context.Customers.Single(m => m.Id == customer.Id);
                customerInDb.Name = customer.Name;
                customerInDb.Birthdate = customer.Birthdate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;

                // Mapper.map(customer, customerInDb);
            }
            _context.SaveChanges();


            return RedirectToAction("Index", "Customers");
        }


        public ActionResult Edit(int id)
        {
            var costumer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (costumer == null)
                return HttpNotFound();
            
            var viewmodel = new CustomerFormViewModel
            {
                Customer = costumer,
                MemberShipTypes = _context.MemberShipTypes.ToList()
            };
            return View("CustomerForm", viewmodel );
        }



        //public ActionResult Create(NewCustomerViewModel viewmodel)
        //{
            
        //    return View();
        //}
        public ActionResult Action()
        {
            return View();
        }

        public ViewResult Index()
        {
            
            
            //return View(getCustomers());
            
            // Eager Loading -> it includes the related objects
            var customers = _context.Customers.Include(c => c.MemberShipType ).ToList();
            return View(customers);
            
        }

        public ActionResult Details(int? Id)
        {
            if (Id==null)
                return HttpNotFound();

            //var Customer = getCustomers().SingleOrDefault(c => c.Id == Id);
            var Customer = _context.Customers.Include(c=> c.MemberShipType ).SingleOrDefault(c => c.Id == Id);
            if (Customer == null)
                return HttpNotFound();
            else
                return View(Customer);
        }

        //IEnumerable<Customer> getCustomers()
        //{
        //    return new List<Customer>{
        //        new Customer{ Id=1, Name="Johh Smith"},
        //        new Customer{ Id=2, Name="Mary Williams"}
        //    };
        //    //return new List<Customer>();
        //}

        

    }
}