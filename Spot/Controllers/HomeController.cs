using System.Threading.Tasks;
using System.Web.Mvc;
using Spot.Repositories;

namespace Spot.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly IPostRepository PostRepository;

        public HomeController(IPostRepository postRepository)
        {
            PostRepository = postRepository;
        }

        public async Task<ActionResult> Index()
        {
            var model = await PostRepository.GetAllAsync();

            ViewBag.Title = "Home Page";
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult NotFound() => View("404");

        public ActionResult InternalServerError() => View("500");
    }
}
