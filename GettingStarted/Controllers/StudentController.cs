using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GettingStarted.Data;
using GettingStarted.Models;
using GettingStarted.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace GettingStarted.Controllers
{
    //[Authorize(Roles = "Student, Admin, Administrator")]
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        //[Authorize(Roles = "Administrator")]
        // GET: Student
        [Route("student/Index")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Students.ToListAsync());
        }

        // GET: Student/Details/5
        [Route("student/Details/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.Include(x=>x.Enrollment).ThenInclude(y=>y.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Student/Create
        public IActionResult Create()
        {
            var courses = _context.Courses.Select(x => new SelectListItem()
            {
                Text = x.Title,
                Value = x.Id.ToString(),
            }).ToList();
            CreateStudentViewModel vm = new CreateStudentViewModel();
            vm.Courses = courses;
            return View(vm);
        }

        // POST: Student/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateStudentViewModel vm)
        {
            var student = new Student
            {
                Name = vm.Name,
                Enrolled = vm.Enrolled
            };
            var selectedCourses = vm.Courses.Where(x => x.Selected).Select(y => y.Value).ToList();
            foreach (var item in selectedCourses)
            {
                student.Enrollment.Add(new StudentCourse()
                {
                    CourseId = int.Parse(item)
                });
            }
            _context.Students.Add(student);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Student/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.Include(x => x.Enrollment).Where(y => y.Id == id).FirstOrDefaultAsync();
            var selectedIds = student.Enrollment.Select(x => x.CourseId).ToList();
            var items = _context.Courses.Select(x => new SelectListItem()
            {
                Text = x.Title,
                Value = x.Id.ToString(),
                Selected = selectedIds.Contains(x.Id)
            }).ToList();
            CreateStudentViewModel vm = new CreateStudentViewModel();
            vm.Name = student.Name;
            vm.Enrolled = student.Enrolled;
            vm.Courses = items;
            return View(vm);
        }

        // POST: Student/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CreateStudentViewModel vm)
        {
            var student = _context.Students.Find(vm.Id);
            student.Name = vm.Name;
            student.Enrolled = vm.Enrolled;
            var studentByIds = _context.Students.Include(x => x.Enrollment).FirstOrDefault(y => y.Id == vm.Id);
            var existingIds = studentByIds.Enrollment.Select(x => x.CourseId).ToList();
            var selectedIds = vm.Courses.Where(x => x.Selected).Select(y => y.Value).Select(int.Parse).ToList();
            var toAdd = selectedIds.Except(existingIds);
            var toRemove = existingIds.Except(selectedIds);
            student.Enrollment = student.Enrollment.Where(x => !toRemove.Contains(x.CourseId)).ToList();

           foreach (var item in toAdd)
            {
                student.Enrollment.Add(new StudentCourse()
                {
                   CourseId = item

                });
            }
            _context.Students.Update(student);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: Student/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }
    }
}
