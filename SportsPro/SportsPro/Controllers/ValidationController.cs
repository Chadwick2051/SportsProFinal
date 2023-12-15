using Microsoft.AspNetCore.Mvc;
using SportsPro.Models;
using SportsPro.Models.DataAccess;
using SportsPro.Models.Validation;

namespace SportsPro.Controllers
{
    public class ValidationController : Controller
    {
        private Repository<Customer> customerData { get; set; }

        public ValidationController(SportsProContext ctx)
        {
            customerData = new Repository<Customer>(ctx);
        }

        public IActionResult CheckEmail(string emailAddress, int customerId)
        {
            Customer customer = new Customer { Email = emailAddress, CustomerID = customerId };

            string msg = Validate.CheckEmail(customerData, customer);

            if(string.IsNullOrEmpty(msg))
            {
                return Json(true);
            }
            else
            {
                return Json(msg);
            }
            return View();
        }
    }
}
