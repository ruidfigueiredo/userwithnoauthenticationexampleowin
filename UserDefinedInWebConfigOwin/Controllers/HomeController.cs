using System.Web.Mvc;

namespace UserDefinedInWebConfig.Controllers
{
    public class HomeController : Controller
    {        
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult UserInformation()
        {
            return View();
        }
    }
}