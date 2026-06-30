using Microsoft.AspNetCore.Mvc;
using PhantasmalARTdemo.Models.Containers;
using PhantasmalARTdemo.Services;

namespace PhantasmalARTdemo.Controllers
{
    public class CommentController : Controller
    {
        private IArtCommentService _artCommentService;

        public CommentController(IArtCommentService artCommentService)
        {
            _artCommentService = artCommentService;
        }

        [HttpPost]
        [Route("comment/new")]
        public IActionResult AddArtComment(ArtDisplayView pageModel, Guid artID, string authorUsername)
        {
            pageModel.CommentForm.Entity.Author = authorUsername;
            // get comment from form and save it to db
            _artCommentService.NewComment(pageModel.CommentForm.Entity, artID);
            // return to art
            return RedirectToAction("ArtDisplay", "Art", new { username = authorUsername, artID });
        }
    }
}
