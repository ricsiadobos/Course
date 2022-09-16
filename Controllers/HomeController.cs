﻿using Course2.Data;
using Course2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Course2.Controllers
{
    public class HomeController : Controller
    {

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
            ViewBag.videos = false;
           var LogInEmpRequest = LogInEmployee;

            if (!string.IsNullOrEmpty(LogInEmpRequest.EmloyeeName) && !string.IsNullOrEmpty(LogInEmpRequest.EmloyeeEmail)) 
            {
                var existingEmployee = await _context.Users.Select(x => x).FirstOrDefaultAsync(x => x.EmloyeeEmail == LogInEmpRequest.EmloyeeEmail & x.EmloyeeName == LogInEmpRequest.EmloyeeName);
                var professionalVideos = await _context.Videos.Select(x => x).Where(x => x.PositionId == existingEmployee.PositionId).ToListAsync();
                ViewBag.videos = professionalVideos;

                if (existingEmployee is Employee)
                {
                    logInUser =  existingEmployee;
                    ViewBag.SuccessLogInEmp = existingEmployee.EmloyeeName;
                return View();
                }

            }

            return View("Sikertelen bejelentkezés");
        }

              public IActionResult CreateEmp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmp([Bind("EmloyeeName,EmloyeeEmail,PositionId")] Employee employee)
        {

            var megerkezett = employee;

            if (ModelState.IsValid)
            {
                _context.Add(megerkezett);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(CreateEmp));
            }

            return View();
        }


        public async Task<IActionResult> CreateVideo()
        {

            /// /// /// ///
            List<SelectListItem> mySkills = new List<SelectListItem>() {
        new SelectListItem {
            Text = "ASP.NET MVC", Value = "1"
        },
        new SelectListItem {
            Text = "ASP.NET WEB API", Value = "2"
        }, };


            /// /// /// ///
            var PositionList = await _context.Positions.Select(x => x).ToListAsync();
            ViewData["PositionList"] = PositionList;

            ViewData["MySkills"] = mySkills;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVideo([Bind("VideoName,videoURL,PositionId")] Video uploadVideoRequest)
        {
            ViewBag.SuccessUploadVideo = false;
            var uploadVideo = uploadVideoRequest;

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