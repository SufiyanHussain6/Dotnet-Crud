using CrudApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CrudApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly  ApplicationDbContext _dbcontext;

        public HomeController(ApplicationDbContext dbContext)
        {
            _dbcontext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]

        public IActionResult AddStudent()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult AddStudent(Student student)
        {
            if (ModelState.IsValid)
            {
                _dbcontext.Students.Add(student);
                _dbcontext.SaveChanges();
                TempData["Success"] = "Student Registered Successfully";
                return RedirectToAction("ViewStudent", "Home");
            }
            return View();
        }
        [HttpGet]

        public IActionResult ViewStudent()
        {
            var student = _dbcontext.Students.ToList();
            return View(student);
        }

        public IActionResult Details(int id)
        {
            var students = _dbcontext.Students.FirstOrDefault(x=>x.Id==id);
            return View(students);


        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var students = _dbcontext.Students.Find(id);
            return View(students);


        }

        public IActionResult Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                _dbcontext.Students.Update(student);
                _dbcontext.SaveChanges();
                TempData["Update"] = "Student Update Successfully";
                return RedirectToAction("ViewStudent", "Home");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var students = _dbcontext.Students.Find(id);
            return View(students);


        }
        public IActionResult Delete(Student student)
        
           
            {
                _dbcontext.Students.Remove(student);
                _dbcontext.SaveChanges();
                TempData["Delete"] = "Student Delete Successfully";
                return RedirectToAction("ViewStudent", "Home");
            }
          
        

            public  IActionResult Privacy()
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
