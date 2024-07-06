using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GettingStarted.Data;
using GettingStarted.Models;
using Microsoft.AspNetCore.Mvc;

namespace GettingStarted.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CoursesController(ApplicationDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Get Section of Course Model
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            var courses = _context.Courses.ToList();
            return View(courses);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            
            var course = _context.Courses.Where(x => x.Id == id).FirstOrDefault();
            return View(course);

        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var course = _context.Courses.Where(x => x.Id == id).FirstOrDefault();
            return View(course);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var course = _context.Courses.Where(x => x.Id == id).FirstOrDefault();
            return View(course);
        }

        /// <summary>
        /// Post Section of Course Model
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(Course model)
        {
            _context.Courses.Add(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Edit(Course model)
        {
            _context.Courses.Update(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Delete(Course model)
        {
            _context.Courses.Remove(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}