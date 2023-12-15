using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SportsPro.Models;
using SportsPro.Models.DataAccess;

namespace SportsPro.Controllers
{
    [Authorize(Roles = "Admin")]
    public class IncidentController : Controller
    {
        private Repository<Incident> incidentData {  get; set; }
        private Repository<Customer> customerData { get; set; }
        private Repository<Product> productData { get; set; }
        private Repository<Technician> technicianData { get; set; }

        public IncidentController(SportsProContext ctx)
        {
            incidentData = new Repository<Incident>(ctx);
            customerData = new Repository<Customer>(ctx);
            productData = new Repository<Product>(ctx);
            technicianData = new Repository<Technician>(ctx);
        }

        [Route("incidents/{id?}")]
        public IActionResult List(string id)
        {
            var vm = new IncidentsViewModel();
            if(!id.IsNullOrEmpty())
            {
                vm.IncidentFilter = id;
            }

            List<Incident> incidents = incidentData.List(new QueryOptions<Incident>
            {
                Includes = "Customer,Product"
            }).ToList();

            if (vm.IncidentFilter == "unassigned")
            {
                incidents = incidents.Where(i => i.TechnicianID == -1 || i.TechnicianID == null).ToList();
            }

            else if (vm.IncidentFilter == "open")
            {
                incidents = incidents.Where(i => i.DateClosed == null).ToList();
            }

            vm.Incidents = incidents.OrderBy(i => i.Title).ToList();

            return View(vm);
        }

/*

        public IActionResult Filter(string id)
        {
            return RedirectToAction("List", new { Filter = id });
        }
*/
        [HttpGet]
        public ViewResult Add()
        {
            var vm = new IncidentAddEditViewModel();
            vm.Action = "Add";

            vm.Customers = customerData.List(new QueryOptions<Customer>
            {
                OrderBy = c => c.LastName
            }).ToList();

            vm.Products = productData.List(new QueryOptions<Product>
            {
                OrderBy = p => p.Name
            }).ToList();

            vm.Technicians = technicianData.List(new QueryOptions<Technician>
            {
                OrderBy = p => p.Name,
                Where = t => t.TechnicianID > 0
            }).ToList();

            return View("Edit", vm);
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            var vm = new IncidentAddEditViewModel();
            vm.Action = "Edit";

            vm.Customers = customerData.List(new QueryOptions<Customer>
            {
                OrderBy = c => c.LastName
            }).ToList();

            vm.Products = productData.List(new QueryOptions<Product>
            {
                OrderBy = p => p.Name
            }).ToList();

            vm.Technicians = technicianData.List(new QueryOptions<Technician>
            {
                OrderBy = p => p.Name,
                Where = t => t.TechnicianID > 0
            }).ToList();

            vm.Incident = incidentData.Get(id)!;
            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(IncidentAddEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                if (vm.Incident.IncidentID == 0)
                {
                    incidentData.Insert(vm.Incident);
                }
                else
                {
                    incidentData.Update(vm.Incident);
                }
                incidentData.Save();
                return RedirectToAction("List", "Incident");
            }
            else
            {
                vm.Action = (vm.Incident.IncidentID == 0) ? "Add" : "Edit";
                vm.Customers = customerData.List(new QueryOptions<Customer>
                {
                    OrderBy = c => c.LastName
                }).ToList();

                vm.Products = productData.List(new QueryOptions<Product>
                {
                    OrderBy = p => p.Name
                }).ToList();

                vm.Technicians = technicianData.List(new QueryOptions<Technician>
                {
                    OrderBy = p => p.Name,
                    Where = t => t.TechnicianID > 0
                }).ToList();


                return View(vm);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var incident = incidentData.Get(id);
            return View(incident);
        }

        [HttpPost]
        public IActionResult Delete(Incident incident)
        {
            incidentData.Delete(incident);
            incidentData.Save();
            return RedirectToAction("List","Incident");
        }

    }
}