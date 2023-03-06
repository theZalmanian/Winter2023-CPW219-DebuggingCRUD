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

        public static List<Student> GetStudents(SchoolContext context)
        {
            return (from s in context.Students
                    select s).ToList();
        }

        public static Student GetStudent(SchoolContext context, int id)
        {
            Student p2 = context
                            .Students
                            .Where(s => s.StudentId == id)
                            .Single();
            return p2;
        }

        public static void Delete(SchoolContext context, Student p)
        {
            context.Students.Update(p);
        }

        public static void Update(SchoolContext context, Student p)
        {
            //Mark the object as deleted
            context.Students.Remove(p);

            //Send delete query to database
            context.SaveChanges();
        }
    }
}
