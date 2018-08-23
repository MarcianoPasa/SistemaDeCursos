using System.Web.Mvc;

namespace SistemaDeCursos.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}