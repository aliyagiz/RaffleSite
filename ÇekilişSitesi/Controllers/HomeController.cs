using ÇekilişSitesi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ÇekilişSitesi.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		const int oneMegabyte = 1048576;
		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View("Raffle");
		}

		public IActionResult Privacy()
		{
			return View();
		}

		public IActionResult TestDesign()
		{
			return View();
		}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		//This redirects to page
		public IActionResult Raffle()
		{
			RaffleEntity pageEntity = new RaffleEntity();

            return View(pageEntity);
		}
	}
}