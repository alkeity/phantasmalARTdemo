using Microsoft.AspNetCore.Mvc;

namespace PhantasmalARTdemo.Controllers
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
