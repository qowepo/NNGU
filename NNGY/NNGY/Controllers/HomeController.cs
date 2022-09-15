using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NNGY.Models;
using System.Diagnostics;

namespace NNGY.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        AppDBContext db = new AppDBContext();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            AppDBContext appDBContext = new AppDBContext();

             
            return View();
        }
         
        public IActionResult AccessDenied()
        {
            
            return View();
        }
       
        public async Task<IActionResult> Privacy()
        {
            var all_student = await db.student.ToListAsync();
            return View(all_student);
        }
        
        public async Task<IActionResult> Student()
        {
            var all_student = await db.student.ToListAsync();
            return View(all_student);
        }
         
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}