using BlogRotam.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BlogRotam.Areas.Admin.Controllers_Api
{
    [Route("api/[controller]")]
        [ApiController]
    public class UserApiController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        

        [HttpGet]
        public IActionResult User()
        {
            var c = _context.Users.ToList();

            return Ok(c);
        }
    }
}
