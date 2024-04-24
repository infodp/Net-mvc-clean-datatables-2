using Microsoft.AspNetCore.Mvc;
using Mvc_curso.Domain.Entities;
using Mvc_curso.Infrastructure.Context;

namespace Mvc_curso.Web.Controllers
{
    public class StudentController : Controller
    {
        private readonly AppDbContext _db;
        public StudentController(AppDbContext db)
        {
            _db = db;
        }
        //Procedimiento para mostrar todos los registros en DT2
        public IActionResult Index()
        {
            var students = _db.Students.ToList();
            return View(students);
        }
        //Procedimiento para mostrar vista del form Create
        public IActionResult Create()
        {
            return View();
        }

        //Procedimiento para guardar el registro en la DB
        [HttpPost]
        public IActionResult Store(Student student)
        {
            if (ModelState.IsValid)
            {
                _db.Students.Add(student);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Create");
        }
        
        //Procedimiento para mostrar la vista de form Edit
        public IActionResult Edit(int id)
        {
            Student? student = _db.Students.SingleOrDefault(s => s.Id == id);
            //Student? student = _db.Students.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        //Procedimiento para actualizar el registro en la DB
        [HttpPost]
        public IActionResult Update(Student student)
        {
            if (ModelState.IsValid && student.Id > 0)
            {
                _db.Students.Update(student);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Edit");
        }
        //Procedimiento para mostrar vista de form Delete

        public IActionResult Delete(int id)
        {
            //Student? student = _db.Students.SingleOrDefault(s => s.Id == id);
            Student? student = _db.Students.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpPost, ActionName("Destroy")]
        public IActionResult Destroy(Student student)
        {
            if (ModelState.IsValid && student.Id > 0)
            {
                _db.Students.Remove(student);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Delete");
        }

    }
}
