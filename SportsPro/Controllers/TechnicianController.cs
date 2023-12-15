using Microsoft.AspNetCore.Mvc;
using SportsPro.Models;
using SportsPro.Models.DataAccess;

namespace SportsPro.Controllers
{
	public class TechnicianController : Controller
	{
		private Repository<Technician> technicianData {  get; set; }
		public TechnicianController(SportsProContext ctx)
		{
			technicianData = new Repository<Technician>(ctx);
		}

        [Route("[controller]s")]
        public IActionResult List()
		{
            List<Technician> technicians = technicianData.List(new QueryOptions<Technician>
            {
                OrderBy = t => t.Name,
				Where = t => t.TechnicianID > 0
            }).ToList();

            return View(technicians);
		}

		public IActionResult Add() 
		{
			ViewBag.Action = "Add";
			return View("AddEdit", new Technician());
		}

		public IActionResult Edit(int id)
		{
			ViewBag.Aciton = "Edit";
			var tech = technicianData.Get(id)!;
			return View("AddEdit",tech);
		}
		[HttpPost]
		public IActionResult Save(Technician tech)
		{
			if (ModelState.IsValid)
			{
				if (tech.TechnicianID == 0)
				{
					technicianData.Insert(tech);
				}
				else
				{
					technicianData.Update(tech);
				}
				technicianData.Save();
				return RedirectToAction("List");
			}
			else
			{
				ViewBag.Action = (tech.TechnicianID == 0) ? "Add" : "Edit";
				return View(tech);
			}
		}
		[HttpGet]
		public IActionResult Delete(int id)
		{
			Technician technician = technicianData.Get(id)!;
			return View(technician);
		}
		[HttpPost]
		public IActionResult Delete(Technician tech)
		{
			technicianData.Delete(tech);
			technicianData.Save();
			return RedirectToAction("List", "Technician");
		}
	}
}
