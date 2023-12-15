using Microsoft.AspNetCore.Mvc;
using SportsPro.Models;
using SportsPro.Models.DataAccess;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SportsPro.Controllers
{
    public class TechIncidentController : Controller
    {
        private const string TECH_Key = "techID";
        private Repository<Technician> technicianData {  get; set; }
        private Repository<Incident> incidentData { get; set; }

        public TechIncidentController(SportsProContext ctx)
        {
            technicianData = new Repository<Technician>(ctx);
            incidentData = new Repository<Incident>(ctx);
        }

        [Route("techincident")]
        public IActionResult Index()
        {
            ViewBag.Technicians = technicianData.List(new QueryOptions<Technician>
            {
                OrderBy = c => c.TechnicianID,
                Where = t => t.TechnicianID > -1
            }).ToList();

            var technician = new Technician();

            int? techID = HttpContext.Session.GetInt32(TECH_Key);

            if(techID.HasValue)
            {
                technician = technicianData.Get(techID.Value);
            }

            return View(technician);
        }

        [HttpPost]
        public IActionResult List(Technician technician)
        {
            if(technician.TechnicianID == 0)
            {
                TempData["message"] = "You must select a technician.";
                return RedirectToAction("Index");
            }
            else
            {
                HttpContext.Session.SetInt32(TECH_Key, technician.TechnicianID);
                return RedirectToAction("List", new {id = technician.TechnicianID});
            }
        }

        [HttpGet]
        public IActionResult List(int id)
        {
            var technician = technicianData.Get(id);
            if(technician == null)
            {
                TempData["message"] = "Technician not found. Please select a tech.";
                return RedirectToAction("Index");
            }
            else
            {
                var model = new TechnicianIncidentViewModel
                {
                    Technician = technician,
                    Incidents = incidentData.List(new QueryOptions<Incident>
                    {
                        Includes = "Customer,Product",
                        Where = i => i.TechnicianID == id && i.DateClosed == null
                    }).ToList()
                };
                return View(model);
            }
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            int? techId = HttpContext.Session.GetInt32(TECH_Key);
            if(!techId.HasValue)
            {
                TempData["message"] = "Technician not found. Please select a tech.";
                return RedirectToAction("Index");
            }

            var technician = technicianData.Get(techId.Value);
            if(technician == null)
            {
                TempData["message"] = "Technician not found. Please select a tech.";
                return RedirectToAction("Index");
            }
            else
            {
                var model = new TechnicianIncidentViewModel
                {
                    Technician = technician,
                    Incident = incidentData.List(new QueryOptions<Incident>
                    {
                        Includes = "Customer,Product"
                    }).FirstOrDefault(i => i.TechnicianID == id)!
                };
                return View(model);
            }
        }
        [HttpPost]
        public IActionResult Edit(IncidentViewModel model)
        {
            Incident? i = incidentData.Get(model.Incident.IncidentID);
            i.Description = model.Incident.Description;
            i.DateClosed = model.Incident.DateClosed;

            incidentData.Update(i);
            incidentData.Save();

            int? techID = HttpContext.Session.GetInt32(TECH_Key);
            return RedirectToAction("List", new {id = techID});
        }
    }
}
