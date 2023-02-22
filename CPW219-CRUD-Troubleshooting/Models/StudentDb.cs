namespace CPW219_CRUD_Troubleshooting.Models
{
    public static class StudentDb
    {
        public static Student Add(Student p, SchoolContext db)
        {
            //Add student to context
            db.Students.Add(p);
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
