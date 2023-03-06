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
            List<Student> allStudents = await StudentDb.GetStudents(context);

            // Display them on the page
            return View(allStudents);
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

            // Show web page with errors
            return View(currStudent);
        }

        public IActionResult Edit(int id)
        {
            // Get the Student by their id
            Student currStudent = StudentDb.GetStudent(context, id);

            // If the Student was not found, display error
            if (currStudent == null)
            {
                return NotFound();
            }

            // Display their info on the page
            return View(currStudent);
        }

        [HttpPost]
        public IActionResult Edit(Student currStudent)
        {
            if (ModelState.IsValid)
            {
                // Update the given student
                StudentDb.Update(context, currStudent);

                // Display success message
                ViewData["Message"] = "Product Updated!";
            }

            // Show view with errors
            return View(currStudent);
        }

        public IActionResult Delete(int id)
        {
            // Get the Student by their id
            Student currStudent = StudentDb.GetStudent(context, id);

            // Display their info on the page
            return View(currStudent);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            // Get Student from database
            Student studentToDelete = StudentDb.GetStudent(context, id);

            // Delete the given student
            StudentDb.Delete(context, studentToDelete);

            // Prepare success message
            TempData["Message"] = $"{studentToDelete.Name} was deleted successfully";

            // Send the user back to the Roster
            return RedirectToAction("Index");
        }
    }
}
