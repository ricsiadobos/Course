using Course2.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Course2.Models;
using Microsoft.EntityFrameworkCore;

namespace Course2.Controllers
{
    public class EmloyeeController : Controller
    {
        private readonly DataContext _context;

        public EmloyeeController(DataContext context)
        {
            _context = context;
        }



        // GET: EmloyeeController
        public async Task<ActionResult> Index()
        {
            var users = await _context.Users.ToListAsync();


            return View();

            
        }

        // GET: EmloyeeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EmloyeeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmloyeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmloyeeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EmloyeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EmloyeeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EmloyeeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
