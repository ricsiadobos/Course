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
            var users = await _context.Users.ToListAsync();
            var videos = await _context.Videos.ToListAsync();
            var positions = await _context.Positions.ToListAsync();
            var videosList = await _context.Videos.Select(x => x).Where(x => x.PositionId == 2).ToListAsync();

            ViewBag.videos = videos;



            return View();


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