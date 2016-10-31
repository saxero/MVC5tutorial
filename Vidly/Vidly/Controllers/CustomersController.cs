using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            return View(getCustomers());
            
        }

        public ActionResult Details(int? Id)
        {
            if (Id==null)
                return HttpNotFound();

            var Customer = getCustomers().SingleOrDefault(c => c.Id == Id);
            if (Customer == null)
                return HttpNotFound();
            else
                return View(Customer);
        }

        IEnumerable<Customer> getCustomers()
        {
            return new List<Customer>{
                new Customer{ Id=1, Name="Johh Smith"},
                new Customer{ Id=2, Name="Mary Williams"}
            };
            //return new List<Customer>();
        }

        

    }
}