using Microsoft.AspNetCore.Mvc;

namespace PDPMvc.Controllers
{
	public class AccountController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
