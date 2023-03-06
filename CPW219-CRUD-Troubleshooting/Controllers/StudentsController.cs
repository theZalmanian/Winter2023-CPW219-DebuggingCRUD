using CPW219_CRUD_Troubleshooting.Models;
using Microsoft.AspNetCore.Mvc;

namespace CPW219_CRUD_Troubleshooting.Controllers
{
    public class StudentsController : Controller
    {
        private readonly SchoolContext context;

        public StudentsController(SchoolContext dbContext)
        {
            context = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            // Get all Students from the db
            List<Student> products = await StudentDb.GetStudents(context);

            // Display them on the page
            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Student currStudent)
        {
            if (ModelState.IsValid)
            {
                // Add the Student to the db
                await StudentDb.Add(currStudent, context);

                // Display success message
                ViewData["Message"] = $"{currStudent.Name} was added!";
            }

            //Show web page with errors
            return View(currStudent);
        }

        public IActionResult Edit(int id)
        {
            //get the product by id
            Student p = StudentDb.GetStudent(context, id);

            //show it on web page
            return View();
        }

        [HttpPost]
        public IActionResult Edit(Student p)
        {
            if (ModelState.IsValid)
            {
                StudentDb.Update(context, p);
                ViewData["Message"] = "Product Updated!";
                return View(p);
            }
            //return view with errors
            return View(p);
        }

        public IActionResult Delete(int id)
        {
            Student p = StudentDb.GetStudent(context, id);
            return View(p);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            //Get Product from database
            Student p = StudentDb.GetStudent(context, id);

            StudentDb.Delete(context, p);

            return RedirectToAction("Index");
        }
    }
}
