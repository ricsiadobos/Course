using Course2.Data;
using Course2.Models;
using Course2.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Course2.Controllers
{
    public class HomeController : Controller
    {

        //https://www.youtube.com/watch?v=B94TmwVhYsE&t=457s&ab_channel=CodeS

        private readonly DataContext _context;
        Employee logInUser;

        public HomeController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("EmloyeeName,EmloyeeEmail")] Employee LogInEmployee)
        {
            ViewBag.videos = null;
            var LogInEmpRequest = LogInEmployee;
            var employeeName = "";

            try
            {
                if (!string.IsNullOrEmpty(LogInEmpRequest.EmloyeeName) && !string.IsNullOrEmpty(LogInEmpRequest.EmloyeeEmail))
                {
                    var existingEmployee = await _context.Users.Select(x => x).FirstOrDefaultAsync(x => x.EmloyeeEmail == LogInEmpRequest.EmloyeeEmail & x.EmloyeeName == LogInEmpRequest.EmloyeeName);
                    if (existingEmployee is Employee)
                    {
                        employeeName = existingEmployee.EmloyeeName;
                        var professionalVideos = await _context.Videos.Select(x => x).Where(x => x.PositionId == existingEmployee.PositionId).ToListAsync();
                        ViewBag.videos = professionalVideos;
                        logInUser = existingEmployee;
                        ViewBag.SuccessLogInEmp = existingEmployee.EmloyeeName;
                        ViewBag.FailedLogIn = false;
                        return View();
                    }

                }

            }
            catch (Exception)
            {
                ViewBag.FailedLogIn = true;
                ViewBag.FailedMessage = "Sikertelen bejelentkezés";
                return View();
            }
            ViewBag.FailedLogIn = true;
            ViewBag.FailedMessage = "Sikertelen bejelentkezés";
            return View();
        }

        public IActionResult CreateEmp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmp([Bind("EmloyeeName,EmloyeeEmail,PositionId")] Employee createEmployeeRequire)
        {

            var createEmployee = createEmployeeRequire;

            if (ModelState.IsValid)
            {
                _context.Add(createEmployee);
                await _context.SaveChangesAsync();
                return View();
            }

            return RedirectToAction(nameof(CreateEmp));
        }


        public async Task<IActionResult> CreateVideo()
        {

            UpLoadVideo upLoadVideo = new UpLoadVideo();
            upLoadVideo.Video = new Video();

            upLoadVideo.Positions = _context.Positions
                    .ToList()
                    .Select(x => new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = x.PositionName,
                        Selected = (x.Id == 2)

                    });

            int DefaultId = 2;
            var dropdownPositionList = _context.Positions
                    .ToList()
                    .Select(x => new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = x.PositionName,
                        Selected = (x.Id == DefaultId)

                    });

            ViewBag.pos = dropdownPositionList;
            /*
            /// /// /// ///
            var PositionList = await _context.Positions.Select(x => x).ToListAsync();
            var PositionNameList =  PositionList.Select(x => x.PositionName).ToList();
            List<SelectListItem> sdf = new List<SelectListItem>((IEnumerable<SelectListItem>)PositionNameList);
            ViewData["PositionList"] = sdf;
            ViewBag.PositionList = sdf;
            ViewData["MySkills"] = mySkills;
            */
            var PositionNameList = _context.Positions.Select(x => x.PositionName).ToList();
            ViewBag.PositionList = PositionNameList;

            return View(upLoadVideo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVideo(UpLoadVideo upLoadVideoRequired)
        {

            ViewBag.SuccessUploadVideo = false;
            var uploadVideo = upLoadVideoRequired;

            if (ModelState.IsValid)
            {
                _context.Add(uploadVideo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(CreateVideo));
            }

            return View();
        }


        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}