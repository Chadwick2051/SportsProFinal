using Microsoft.AspNetCore.Mvc;
using SportsPro.Models;
using SportsPro.Models.DataAccess;
using System.Diagnostics;

namespace SportsPro.Controllers
{
	public class HomeController : Controller
	{
		private SportsProContext context {  get; set; }

		public HomeController(SportsProContext ctx) => context = ctx;

		public IActionResult Index()
		{
			return View();
		}

		[Route("about")]
		public IActionResult About ()
		{
			return View();
		}
	}
}