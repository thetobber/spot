using System.Threading.Tasks;
using System.Web.Mvc;
using Spot.Repositories;

namespace Spot.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostRepository PostRepository;

        public HomeController(IPostRepository postRepository)
        {
            PostRepository = postRepository;
        }

        [AllowAnonymous]
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
    }
}
