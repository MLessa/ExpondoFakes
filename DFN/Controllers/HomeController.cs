using DFN.Services;
using System.Web.Mvc;

namespace DFN.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			if(TempData["posted"] == null)
				ViewBag.Posted = "";
			else
				ViewBag.Posted = TempData["posted"];

			return View(new DatabaseService().GetDenounceData());
		}
	}
}
