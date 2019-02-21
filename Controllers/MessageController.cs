using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wall.Models;

namespace Wall.Controllers
{
    public class MessageController : Controller
    {
        private WallContext dbContext;
        public MessageController(WallContext context)
        {
            dbContext = context;
        }

        [Route("message")]
        [HttpGet]
        public IActionResult Success()
        {
            int? id = HttpContext.Session.GetInt32("id");
            if (id != null)
            {
                ViewBag.allMessage = dbContext.Messages
                    .Include(y =>y.User)
                    .Include(u => u.Comments)
                    .ThenInclude(u => u.User)
                    .ToList();
                ViewBag.User = dbContext.Users.FirstOrDefault(d => d.userId == id);
                return View("Message");
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost("createmessage")]
        public IActionResult MessageProcess(Messages mess)
        {
            mess.userId = (int)HttpContext.Session.GetInt32("id");
            if(ModelState.IsValid)
            {
                dbContext.Add(mess);
                dbContext.SaveChanges();
                return RedirectToAction("Success");
            }
            int? id = HttpContext.Session.GetInt32("id");
                ViewBag.allMessage = dbContext.Messages
                    .Include(y =>y.User)
                    .Include(u => u.Comments)
                    .ThenInclude(u => u.User)
                    .ToList();
                ViewBag.User = dbContext.Users.FirstOrDefault(d => d.userId == id);
            return View("Message");
        }

        [HttpPost("createcomment")]
        public IActionResult CommentProcess(Comments com)
        {
            if(ModelState.IsValid)
            {
                dbContext.Add(com);
                dbContext.SaveChanges();
                return RedirectToAction("Success");
            }

                int? id = HttpContext.Session.GetInt32("id");
            
                ViewBag.allMessage = dbContext.Messages
                    .Include(y =>y.User)
                    .Include(u => u.Comments)
                    .ThenInclude(u => u.User)
                    .ToList();
                // Messageboard message = new Messageboard();
            
                // message.Messages = ViewBag.allMessage;
                ViewBag.User = dbContext.Users.FirstOrDefault(d => d.userId == id);
                return View("Message");          
        }

        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }

    
}