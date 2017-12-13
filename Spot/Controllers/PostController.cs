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
        public ActionResult New() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> New(PostNewViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var post = new PostModel {
                Status = model.Status,
                Title = model.Title,
                Content = model.Content,
                Excerpt = model.Excerpt,
                Created = DateTime.Now,
                Modified = DateTime.Now
            };

            if (model.Status == PostStatus.Public)
                post.Published = DateTime.Now;

            try {
                PostRepository.Add(post);
                var result = await PostRepository.SaveAsync();

                if (result > 0)
                    return RedirectToAction("Edit", "Post", new { id = post.Id });
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

                model = await PostRepository.GetPagedAsync(pageIndex);
            try {
            }
            catch {
                return View("500");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            PostEditViewModel model;

            try {
                model = await PostRepository.GetEditAsync(id);
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
        public async Task<ActionResult> Edit(PostEditViewModel model)
        {
            try {
                var post = await PostRepository.GetAsync(model.Id);

                if (post == null)
                    return View("404"); // Return post has been removed while editing

                if (model.Status == PostStatus.Public && post.Published == null)
                    post.Published = DateTime.Now;
                else if (model.Status != PostStatus.Public)
                    post.Published = null;

                post.Status = model.Status;
                post.Title = model.Title;
                post.Excerpt = model.Excerpt;
                post.Content = model.Content;
                post.Modified = DateTime.Now;

                PostRepository.Update(post);

                var result = await PostRepository.SaveAsync();

                if (result > 0) {
                    ViewBag.Success = $"{post.Title} has been updated.";
                    return View(model);
                }
            }
            catch {
                return View("500");
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Remove() => Content("Create");
    }
}