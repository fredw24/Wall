using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Wall.Models;

namespace Wall.Controllers
{
    public class HomeController : Controller
    {
        private WallContext dbContext;
        public HomeController(WallContext context)
        {
            dbContext = context;
        }

        [Route("")]
        [HttpGet]
        public IActionResult Index()
        {
            return View("LoginAndReg");
        }

        [HttpPost("process")]
        public IActionResult Registration(Users user)
        {
             // Check initial ModelState
            if(ModelState.IsValid)
            {
                if(dbContext.Users.Any(u => u.Email == user.Email))
                {
                    ModelState.AddModelError("Email", "Email already in use!");
                    return View("LoginAndReg");
                }
                dbContext.createUser(HttpContext, user);
                return RedirectToAction("Success", "Message");
            }
            return View("LoginAndReg");
        }


        [HttpPost("getuser")]
        public IActionResult Login(LoginUsers userSubmission)
        {
            if(ModelState.IsValid)
            {
                var userInDb = dbContext.Users
                    .FirstOrDefault(u => u.Email == userSubmission.LogEmail);
                if(userInDb is null)
                {
                    ModelState.AddModelError("Email", "Invalid Email");
                    return View("LoginAndReg");
                }
                var hasher = new PasswordHasher<LoginUsers>();
                var result = hasher.VerifyHashedPassword(
                    userSubmission,
                    userInDb.Password,
                    userSubmission.LogPassword)
                    ;
                var one = hasher;
                if(result == 0)
                {
                    ModelState.AddModelError("LogPassword", "Wrong Password");
                    return View("LoginAndReg");
                }
                HttpContext.Session.SetInt32("id", userInDb.userId);
                return RedirectToAction("Success", "Message");
            }
            return View("LoginAndReg");
        }
    }


}
