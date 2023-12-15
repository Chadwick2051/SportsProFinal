using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SportsPro.Models;
using SportsPro.Models.DataAccess;
using Microsoft.AspNetCore.Authorization;

namespace SportsPro.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RegistrationController : Controller
    {
        private Repository<Customer> customerData {  get; set; }
        private Repository<Product> productData { get; set; }

        public RegistrationController(SportsProContext ctx)
        {
            customerData = new Repository<Customer>(ctx);
            productData = new Repository<Product>(ctx);
        }

        [HttpGet]
        public IActionResult Index()
        {
            RegistrationViewModel vm = new RegistrationViewModel();

            vm.Customers = customerData.List(new QueryOptions<Customer>
            {
                OrderBy = c => c.LastName
            }).ToList();

            return View(vm);
        }

        [HttpPost]
        public IActionResult List(RegistrationViewModel vm)
        {
            if (vm.Customer.CustomerID == 0)
            {
                TempData["message"] = "You must select a customer.";

                vm.Customers = customerData.List(new QueryOptions<Customer>
                {
                    OrderBy = c => c.LastName
                }).ToList();

                return RedirectToAction("Index", vm);
            }
            else
            {
                return RedirectToAction("List", new { id = vm.Customer.CustomerID });
            }
        }

        [HttpGet]
        public IActionResult List(int id)
        {
            RegistrationViewModel vm = new RegistrationViewModel();
            vm.Customer = customerData.Get(new QueryOptions<Customer>
            {
                Includes = "Products",
                OrderBy = c => c.CustomerID == id
            })!;
            vm.Products = productData.List(new QueryOptions<Product>
            {
                OrderBy = p => p.Name
            }).ToList();

            vm.Customer.Products = vm.Customer.Products.OrderBy(p => p.Name).ToList();

            return View(vm);
        }

        [HttpPost]
        public IActionResult Delete(RegistrationViewModel vm, int id)
        {
            var customerId = vm.Customer.CustomerID;
            var productId = id;

            Product product = productData.Get(productId)!;

            Customer customer = customerData.Get(new QueryOptions<Customer>
            {
                Includes = "Products",
                Where = c => c.CustomerID == customerId
            })!;

            customer.Products.Remove(product);
            customerData.Save();

            vm.Products = productData.List(new QueryOptions<Product>
            {
                OrderBy = p => p.Name
            });

            vm.Customer = customer;

            return RedirectToAction("List", new {id = customerId});
        }

        [HttpPost]
        public IActionResult Add(RegistrationViewModel vm, int id)
        {
            var customerId = vm.Customer.CustomerID;
            var productId = id;

            var customer = customerData.Get(new QueryOptions<Customer>
            {
                Includes = "Products",
                Where = c => c.CustomerID == customerId
            })!;

            if(productId == 0)
            {
                TempData["message"] = $"Please select a product to add.";
                return RedirectToAction("List", new { id = customerId });
            }
            else
            {
                var product = productData.Get(productId);

                var customerWithProduct = customerData.Get(new QueryOptions<Customer>
                {
                    Includes = "Products",
                    Where = c => c.CustomerID == customerId
                })!;

                if (customerWithProduct.Products.Any(p => p.ProductID == productId))
                {
                    TempData["message"] = $"{customer.FullName} already has {product?.Name} Registered";
                    return RedirectToAction("List", new { id = productId });
                }

                customer.Products.Add(product!);
                customerData.Save();

                return RedirectToAction("List", new { id = customerId });
            }
        }
    }
}
