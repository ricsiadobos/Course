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

        public async Task<IActionResult> CreateEmp()
        {
            UpLoadEmployee upLoadEmployee = new UpLoadEmployee();
            upLoadEmployee.Employee = new Employee();
            upLoadEmployee.Positions = await _context.Positions.ToListAsync();

            return View(upLoadEmployee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEmp(UpLoadEmployee upLoadEmployeeRequire)
        {

            Employee upLoadCreateEmployee = new Employee();
            {
                upLoadCreateEmployee.EmloyeeName = upLoadEmployeeRequire.Employee.EmloyeeName;
                upLoadCreateEmployee.EmloyeeEmail = upLoadEmployeeRequire.Employee.EmloyeeEmail;
                upLoadCreateEmployee.PositionId = upLoadEmployeeRequire.PositionSelectedId;
            }


            if (upLoadCreateEmployee is Employee)
            {
                _context.Add(upLoadCreateEmployee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(CreateEmp));
            }

            return RedirectToAction(nameof(upLoadEmployeeRequire));
        }


        public async Task<IActionResult> CreateVideo()
        {

            UpLoadVideo upLoadVideo = new UpLoadVideo();
            upLoadVideo.Video = new Video();

            upLoadVideo.Positions = _context.Positions.ToList();

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
            Video upLoadCreateVideo = new Video();
            upLoadCreateVideo = upLoadVideoRequired.Video;
            upLoadCreateVideo.PositionId = upLoadVideoRequired.PositionSelectedId;


            _context.Add(upLoadCreateVideo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(CreateVideo));


            upLoadVideoRequired.Positions = _context.Positions.ToList();

            return View();
        }


        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}