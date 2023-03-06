using Microsoft.EntityFrameworkCore;

namespace CPW219_CRUD_Troubleshooting.Models
{
    public static class StudentDb
    {
        /// <summary>
        /// Adds the given student to the database
        /// </summary>
        /// <param name="p">The student being added to the database</param>
        /// <param name="db">The database context to be used</param>
        /// <returns>The student being added to the database</returns>
        public static async Task<Student> Add(Student p, SchoolContext db)
        {
            // Prepare INSERT Statement
            db.Students.Add(p);

            // Execute query asynchronously
            await db.SaveChangesAsync();

            return p;
        }

        /// <summary>
        /// Gets all Students present in the database and returns them in a list
        /// </summary>
        /// <param name="context">The database context being used</param>
        /// <returns>A list containing all students present in the db</returns>
        public static async Task<List<Student>> GetStudents(SchoolContext context)
        {
            return await context.Students.ToListAsync();
        }

        /// <summary>
        /// Searches the database for a Student with the given ID, 
        /// returns the student if found; otherwise returns null
        /// </summary>
        /// <param name="context">The database context being used</param>
        /// <param name="studentID">The ID of the Student to be found</param>
        /// <returns>The Student if found; otherwise null</returns>
        public static Student GetStudent(SchoolContext context, int studentID)
        {
            // Get the Student from the db w/ the given ID
            Student currStudent = context.Students
                                         .Where(student => student.StudentId == studentID)
                                         .Single();

            // Return the student
            return currStudent;
        }

        /// <summary>
        /// Updates the given Student in the db
        /// </summary>
        /// <param name="context">The database context being used</param>
        /// <param name="currStudent">The student being updated</param>
        public static void Update(SchoolContext context, Student currStudent)
        {
            // Prepare UPDATE Statement
            context.Students.Update(currStudent);

            // Execute query
            context.SaveChanges();
        }

        public static void Delete(SchoolContext context, Student p)
        {
            //Mark the object as deleted
            context.Students.Remove(p);

            //Send delete query to database
            context.SaveChanges();
        }
    }
}
