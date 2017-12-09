using System.Threading.Tasks;
using System.Web.Mvc;
using Spot.Data;
using Spot.Repositories;

namespace Spot.Controllers
{
    [Route("{action=Index}")]
    public class HomeController : Controller
    {
        private readonly PostRepository PostRepository;

        public HomeController()
        {
            PostRepository = new PostRepository(new SpotContext());
        }

        [AllowAnonymous]
        public async Task<ActionResult> Index()
        {
            var model = await PostRepository.GetAllAsync();

            ViewBag.Title = "Home Page";
            return View(model);
        }

        [Route("About")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [Route("Contact")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            PostRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}
