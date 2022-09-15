using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace NNGY.Controllers
{
    public class AccountController : Controller
    {

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            //if (!ModelState.IsValid)
            //     return View(model);


                  // да, пока так
                  
            if (model.Password != "admin_777" && model.Password != "student_777")
            {
                ModelState.AddModelError("", "User not found!");
                return View(model);
            }
            else if (model.UserName != "admin_777" && model.UserName != "student_777")
            {
                ModelState.AddModelError("", "User not found!");
                return View(model);
            }

            var claims = new List<Claim>();

            if (model.UserName == "admin_777" && model.Password == "admin_777")
                claims.Add(new Claim( ClaimTypes.Role, "Administrator"));
               
            
            if (model.UserName == "student_777" && model.Password == "student_777")
                claims.Add(new Claim(ClaimTypes.Role, "User"));

            var claimIdentity = new ClaimsIdentity(claims, "Cookie");
            var claimPrincipal = new ClaimsPrincipal(claimIdentity);
            await HttpContext.SignInAsync("Cookie", claimPrincipal);

            return Redirect("/Home/Index/");
        }

        [Authorize]
        public async Task<ActionResult> LogOff()
        {
            await HttpContext.SignOutAsync("Cookie");
            return View("Login");
        }

    }

    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

    }

}

