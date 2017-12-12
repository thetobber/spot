using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Spot.Models.Generic.ViewModels;
using Spot.Models.Post;
using Spot.Models.Post.ViewModels;
using Spot.Repositories;

namespace Spot.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostRepository PostRepository;

        public PostController(IPostRepository postRepository)
        {
            PostRepository = postRepository;
        }

        [HttpGet]
        public ActionResult New() => Content("Create");

        [HttpPost]
        public ActionResult New(object model) => Content("New");

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Single(int id)
        {
            PostModel model;

            try {
                model = await PostRepository.GetAsync(id);
            }
            catch {
                return View("500");
            }

            if (model == null)
                return View("404");

            ViewBag.Title = model.Title;
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Paged(int pageIndex = 1)
        {
            if (pageIndex < 1)
                return View("404");

            PagedViewModel<PostExcerptViewModel> model;

            try {
                model = await PostRepository.GetPagedAsync(pageIndex, 2);
            }
            catch {
                return View("500");
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id) => Content("Create");

        [HttpPost]
        public ActionResult Edit(object model) => Content("Create");

        [HttpPost]
        public ActionResult Remove() => Content("Create");
    }
}