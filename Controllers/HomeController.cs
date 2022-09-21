using Course2.Data;
using Course2.Models;
using Course2.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics;
using Course2.Repository;

namespace Course2.Controllers
{
    public class HomeController : Controller
    {

        private readonly DataContext _context;
        Employee logInUser;
        public int? employeeId = null;

        public HomeController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("EmployeeName,EmployeeEmail")] Employee LogInEmployee)
        {
            ViewBag.videos = null;
            var LogInEmpRequest = LogInEmployee;
            logInUser = LogInEmployee;
            var employeeName = "";

            try
            {
                if (!string.IsNullOrEmpty(LogInEmpRequest.EmployeeName) && !string.IsNullOrEmpty(LogInEmpRequest.EmployeeEmail))
                {
                    var existingEmployee = await _context.Employees.Select(x => x).FirstOrDefaultAsync(x => x.EmployeeEmail == LogInEmpRequest.EmployeeEmail & x.EmployeeName == LogInEmpRequest.EmployeeName);
                    if (existingEmployee is Employee)
                    {
                        employeeName = existingEmployee.EmployeeName;
                        var employeeId = existingEmployee.Id;
                        employeeId = existingEmployee.Id;
                        var professionalVideos = await _context.Videos.Select(x => x).Where(x => x.PositionId == existingEmployee.PositionId).ToListAsync();
                        var WatchVideo = await _context.LogWatchVideos.Select(x => x).Where(x => x.EmployeeId == existingEmployee.Id).ToListAsync();

                        var VideoList = createVideoWithDate(professionalVideos, WatchVideo, employeeId);
                        ViewBag.videos = VideoList; 
                        logInUser = existingEmployee;
                        ViewBag.SuccessLogInEmp = existingEmployee.EmployeeName;
                        ViewBag.SuccessLogInEmpId = existingEmployee.Id;
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

        private List<VideosWithWatchDate> createVideoWithDate(List<Video> professionalVideos, List<LogWatchVideo> logwatchVideo,int employeeId)
        {
            List<VideosWithWatchDate> list = new List<VideosWithWatchDate>();
            VideosWithWatchDate item;
            for (int i = 0; i < professionalVideos.Count; i++)
            {
                item = new VideosWithWatchDate();
                item.VideoId = professionalVideos[i].Id;
                item.VideoName = professionalVideos[i].VideoName;
                item.VideoURL = professionalVideos[i].VideoURL;
                item.EmployeeId = employeeId;
                var minDate = logwatchVideo.Where(x => x.VideoId == item.VideoId).Select(x => x.DateTime).OrderBy(x=>x).FirstOrDefault();
                item.WatchDateFirst = minDate.Year > 1 ? minDate : null;
                var maxDate = logwatchVideo.Where(x => x.VideoId == item.VideoId).Select(x => x.DateTime).OrderByDescending(x => x).FirstOrDefault();
                item.WatchDateLast = maxDate.Year > 1 ? maxDate : null;
                
                 

                list.Add(item);
            }

            return list;
        }


        [HttpPost]
        public async Task<IActionResult> CreateLog([FromBody] VideosWithWatchDate videosWithWatchDate )
        {
            LogWatchVideo logWatch = new LogWatchVideo();
            logWatch.VideoId = videosWithWatchDate.VideoId;
            logWatch.DateTime = DateTime.Now;
            logWatch.EmployeeId = (int)videosWithWatchDate.EmployeeId;

            _context.Add(logWatch);
            await _context.SaveChangesAsync();



            return Json("url");
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
                upLoadCreateEmployee.EmployeeName = upLoadEmployeeRequire.Employee.EmployeeName;
                upLoadCreateEmployee.EmployeeEmail = upLoadEmployeeRequire.Employee.EmployeeEmail;
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