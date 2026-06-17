using PhantasmalARTdemo.Models.DTO;
using PhantasmalARTdemo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PhantasmalARTdemo.Filters
{
    public class UserAuthFilter : Attribute, IAuthorizationFilter
    {
        IUserService _userService;

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string? username = context.HttpContext.Session.GetString("UserName");
            if (!string.IsNullOrWhiteSpace(username)) return;
            _userService = context.HttpContext.RequestServices.GetService<IUserService>();
            UserDTO? user = _userService.GetByUsername(username);
            if (user != null) return;

            context.Result = new RedirectToActionResult("LoginGet", "User", null);
        }
    }

    public class UserSpecificFilter : Attribute, IActionFilter
    {
        IUserService _userService;

        public void OnActionExecuted(ActionExecutedContext context)
        {
            return;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            string? username = context.HttpContext.Session.GetString("UserName");
            string? requestedUser = (string)context.HttpContext.GetRouteValue("username");

            Console.WriteLine($"{username} requests edit of {requestedUser}");

            if (!string.IsNullOrWhiteSpace(username) && username == requestedUser) return;

            _userService = context.HttpContext.RequestServices.GetService<IUserService>();
            UserDTO? user = _userService.GetByUsername(username);
            if (user != null && (user.Role == 113 || user.Role == 13)) return;

            context.Result = new RedirectToActionResult("Index", "Home", null);
        }
    }
}
