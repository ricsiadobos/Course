using Course2.Data;
using Course2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Course2.Controllers
{
    public class HomeController : Controller
    {

        private readonly DataContext _context;

        public HomeController(DataContext context)
        {
            _context = context;
        }

      

        public async Task<IActionResult> Index()
        {
            var logInData = new Employee();
            var logInUser = "";

            
            
            var users = await _context.Users.ToListAsync();
            var videos = await _context.Videos.ToListAsync();
            var positions = await _context.Positions.ToListAsync();
            var videosToUser = await _context.Videos.Select(x => x).Where(x => x.PositionId == 2).ToListAsync();

            ViewBag.videos = videosToUser;


            return View();


        }

        [HttpPost]
        public async Task<IActionResult> Index(string employeeName, string employeeEmail)
        {

            if (!string.IsNullOrEmpty(employeeName) && !string.IsNullOrEmpty(employeeEmail)) 
            {
                return Redirect("Sikertelen bejelentkezés");
            }


            var logInData = new Employee();
            var logInUser = "";


            var users = await _context.Users.ToListAsync();
            var videos = await _context.Videos.ToListAsync();
            var positions = await _context.Positions.ToListAsync();
            var videosToUser = await _context.Videos.Select(x => x).Where(x => x.PositionId == 2).ToListAsync();

            ViewBag.videos = videosToUser;


            return Redirect("Sikertelen bejelentkezés");


        }


        public IActionResult CreateEmp()
        {
            return View();
        }


        public async Task<IActionResult> CreateVideo()
        {
            
            var PositionList = await _context.Positions.Select(x => x.PositionName).ToListAsync();   
            ViewBag.PositionList = PositionList;
            return View();
        }


        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}