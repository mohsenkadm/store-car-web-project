using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using store_car_web_project.Models;
using X.PagedList;

namespace store_car_web_project.Controllers
{
    public class UsercontrolController : MasterController
    {

        public UsercontrolController(PblogsContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> usersview(string searching, int? page)
        {
            var pageNumber = page ?? 1; // if no page is specified, default to the first page (1)
            int pageSize = 4; // Get 25 students for each requested page.
            var onePageOfStudents = _context.Users.Where(x => x.UserName.StartsWith(searching) || searching == null).ToList().ToPagedList(pageNumber, pageSize);
            return View(onePageOfStudents);
        }
        [HttpGet]
        public async Task<IActionResult> postsview(string searching, int? page)
        {
            var pageNumber = page ?? 1; // if no page is specified, default to the first page (1)
            int pageSize = 4; // Get 25 students for each requested page.
            var onePageOfStudents = _context.Posts.Where(x => x.title.StartsWith(searching) || searching == null).ToList().ToPagedList(pageNumber, pageSize);
           
            return View(onePageOfStudents);
        }
        public async Task<IActionResult> DeletePost(int? id)
        {
            var t = _context.Posts.Find(id);
            _context.Posts.Remove(t);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(postsview));
        }
        public async Task<IActionResult> DeleteUser(int? id)
        {
            var t = _context.Users.Find(id);
            _context.Users.Remove(t);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(usersview));
        }
    }
}
