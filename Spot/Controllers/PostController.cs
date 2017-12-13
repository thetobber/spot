using System;
using System.Threading.Tasks;
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
        public ActionResult New() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> New([Bind(Exclude = "Id,Created,Modified,Published")]PostModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            
            model.Created = DateTime.Now;
            model.Modified = DateTime.Now;
            model.Published = DateTime.Now;

            try {
                PostRepository.Add(model);
                var result = await PostRepository.SaveAsync();

                if (result > 0)
                    return RedirectToAction("Edit", "Post", new { id = model.Id });
            }
            catch {
                return View("500");
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Single(int id)
        {
            PostModel model;

            try {
                model = await PostRepository.GetAsync(id, PostStatus.Public);
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
                model = await PostRepository.GetPagedAsync(pageIndex, 9);
            }
            catch {
                return View("500");
            }

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator,Editor")]
        public async Task<ActionResult> Edit(int id)
        {
            PostModel model;

            try {
                model = await PostRepository.GetAsync(id, null);
            }
            catch {
                return View("500");
            }

            if (model == null)
                return View("404");

            ViewBag.Title = $"Editing - {model.Title}";
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator,Editor")]
        public async Task<ActionResult> Edit([Bind(Exclude = "Modified,Published")]PostModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            model.Modified = DateTime.Now;
            model.Published = DateTime.Now;

            PostRepository.Update(model);

            var result = await PostRepository.SaveAsync();

            if (result > 0) {
                ViewBag.Success = $"{model.Title} has been updated.";
                return View(model);
            }

            try {
            }
            catch {
                return View("500");
            }

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator,Editor")]
        public ActionResult Remove() => Content("Create");
    }
}