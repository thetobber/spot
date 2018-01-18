using System;
using System.Data.Entity.Validation;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Spot.Models.Generic.ViewModels;
using Spot.Models.Post;
using Spot.Models.Post.ViewModels;
using Spot.Models.User;
using Spot.Repositories;

namespace Spot.Controllers
{
    [RoutePrefix("posts")]
    public class PostController : Controller
    {
        private readonly IPostRepository PostRepository;
        private readonly UserManager UserManager;
        private readonly Microsoft.AspNet.SignalR.IHubContext PostHub;

        public PostController(
            IPostRepository postRepository,
            UserManager userManager
        )
        {
            PostRepository = postRepository;
            UserManager = userManager;
            PostHub = Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<PostHub>();
        }

        [HttpGet]
        [Route("create")]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create() => View();

        [HttpPost]
        [Route("create")]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PostCreateViewModel newPost)
        {
            if (!ModelState.IsValid)
                return View(newPost);

            UserModel currentUser;

                currentUser = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            try {
            }
            catch {
                return View("500");
            }

            var model = new PostModel {
                Status = newPost.Status,
                Title = newPost.Title,
                Excerpt = newPost.Excerpt,
                Content = newPost.Content,
                Created = DateTime.Now,
                Modified = DateTime.Now,
                Published = DateTime.Now,
                Category = null,
                Author = currentUser.UserName
            };

            try {
                PostRepository.Add(model);
                var result = await PostRepository.SaveAsync();

                if (result > 0) {
                    var latest = await PostRepository.GetAsync(model.Id);

                    if (model != null)
                        PostHub.Clients.All.addNewMessageToPage(model);

                    return RedirectToAction("Edit", "Post", new { id = model.Id });
                }
            }
            catch (DbEntityValidationException e) {
                var a = e.EntityValidationErrors;
                throw e;
                //return View("500");
            }

            return View(model);
        }

        [HttpGet]
        [Route("single/{id:int}")]
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
        [Route("{pageIndex:int?}")]
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
        [Route("edit/{id:int}")]
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
        [Route("edit/{id:int}")]
        [Authorize(Roles = "Administrator,Editor")]
        public async Task<ActionResult> Edit([Bind(Exclude = "Modified,Published")]PostModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            model.Modified = DateTime.Now;
            model.Published = DateTime.Now;

            PostRepository.Update(model);

            var result = await PostRepository.SaveAsync();

            try {
                if (result > 0) {
                    ViewBag.Success = $"{model.Title} has been updated.";
                    return View(model);
                }
            }
            catch {
                return View("500");
            }

            return View(model);
        }

        //[HttpPost]
        [Route("remove/{id:int}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Remove(int id)
        {
            PostRepository.Remove(new PostModel { Id = id });

            var result = await PostRepository.SaveAsync();

            try {
                if (result > 0) {
                    ViewBag.Success = "Post was succesfully removed.";
                    return RedirectToAction("Paged");
                }
            }
            catch {
                return View("500");
            }

            return RedirectToAction("Edit", new { id });
        }
    }
}