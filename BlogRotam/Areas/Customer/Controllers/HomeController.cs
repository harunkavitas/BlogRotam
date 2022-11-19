using BlogRotam.Data;
using BlogRotam.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using X.PagedList;
using X.PagedList.Mvc;
using X.PagedList.Mvc.Core;

namespace BlogRotam.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index(int sayi=1)
        {
            var blog = _db.Blogs.Include(m => m.Category).Select(i => new Blog()
            {
                Id = i.Id,
                Header = i.Header,
                Shows = i.Shows,
                ApplicationUserId = i.ApplicationUserId,
                Date = i.Date,
                Image = i.Image,
                comment = i.comment,
                Article=i.Article.Length>450 ? i.Article.Substring(0,480) + ("....") : i.Article,
            }).ToList().ToPagedList(sayi,4);
            return View(blog);
        }
        public IActionResult Detail(int id)
          { 
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
        
            var blog = _db.Blogs.Include(e => e.Category).ToList().FirstOrDefault(i=>i.Id== id);
            ViewBag.blog = blog;
            var cmt = _db.Comments.Where(i=>i.BlogId==id).Include(e => e.Blog).Include(d=>d.ApplicationUser).ToList();
            ViewBag.cmt = cmt;
            var number = _db.Blogs.ToList().Find(m => m.Id == id);
            number.Shows += 1;
            _db.SaveChanges();
            Comment comment = new Comment()
            {
                Blog = blog,
                BlogId = blog.Id,
            };

            return View(comment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GoComment(Comment comment)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            comment.ApplicationUserId = claim.Value;
            comment.CommentDate = DateTime.Now;
            _db.Comments.Add(comment);
            _db.SaveChanges();
            return RedirectToAction(nameof(Detail));
        }

       


        public IActionResult Search(string q)
        {
            if (!String.IsNullOrEmpty(q))
            {
                var search = _db.Blogs.Where(i => i.Header.Contains(q) || i.Article.Contains(q));
                return View(search);
            }
            return View();
        }
        public PartialViewResult Latest()
        {
            
            var late = _db.Blogs.OrderByDescending(x => x.Date).Take(4).ToList();
            return PartialView(late);
            
        }
        public IActionResult CategoryDetails(int? id)
        {
            var blog = _db.Blogs.Where(i => i.CategoryId == id).ToList();
            ViewBag.Category = id;
            return View(blog);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
