using Microsoft.AspNetCore.Mvc;

namespace phantasmalARTdemo.Controllers
{
    public class ErrorController : Controller
    {
        [Route("not-found")]
        public IActionResult NotFoundPage()
        {
            Response.StatusCode = 404;
            return View("NotFound");
        }
    }
}
