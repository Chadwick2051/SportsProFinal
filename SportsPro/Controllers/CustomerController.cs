using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SportsPro.Models;
using SportsPro.Models.DataAccess;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SportsPro.Controllers
{
	public class CustomerController : Controller
	{
		private Repository<Customer> custData {  get; set; }
		private Repository<Country> countryData { get; set; }

		public CustomerController(SportsProContext ctx)
		{
			custData = new Repository<Customer>(ctx);
			countryData = new Repository<Country>(ctx);
        }

		[Route("customers")]
		[HttpGet]
		public IActionResult List()
		{
			var customers = custData.List(new QueryOptions<Customer>
			{
				OrderBy = c => c.LastName
			});
			return View(customers);
		}

		[HttpGet]
		public IActionResult Add()
		{
			ViewBag.Action = "Add";
			ViewBag.Countries = countryData.List(new QueryOptions<Country>
			{
				OrderBy = c => c.Name
			});
			return View("Edit", new Customer());
		}

        [HttpGet]
        public IActionResult Edit(int id)
		{
			ViewBag.Aciton = "Edit";
            ViewBag.Countries = countryData.List(new QueryOptions<Country>
            {
                OrderBy = c => c.Name
            });
            var customer = custData.Get(id);
			return View(customer);
		}
        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                if (customer.CustomerID == 0)
                {
                    custData.Insert(customer);
                }
                else
                {
                    custData.Update(customer);
                }
                custData.Save();
                return RedirectToAction("List","Customer");
            }
            else
            {
				ViewBag.Action = (customer.CustomerID == 0) ? "Add" : "Edit";
				ViewBag.Countries = countryData.List(new QueryOptions<Country>
				{
					OrderBy = c => c.Name
				});
                return View(customer);
            }
        }
		[HttpGet]
		public IActionResult Delete(int id)
		{
			var customer = custData.Get(id);
			return View(customer);
		}
		[HttpPost]
		public IActionResult Delete(Customer customer)
		{
			custData.Delete(customer);
			custData.Save();
			return RedirectToAction("List", "Customer");
		}
	}
}

