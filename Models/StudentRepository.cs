using Microsoft.EntityFrameworkCore;

namespace Architecture.Models
{
    public class StudentRepository: IStudentRepository
    {
        private readonly AppDbContext _appDbContext;

        public StudentRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Student[]> SearchStudentsAsync(string query)
        {
            // Implement the search logic, e.g., using Entity Framework
            // Sample implementation assuming a context with Employee DbSet:
            return await _appDbContext.Student
                .Where(e => e.StudentName.Contains(query))
                .ToArrayAsync();
        }

        //View students
        public async Task<Student[]> GetAllStudentAsync()
        {
            IQueryable<Student> query = _appDbContext.Student.Include(e => e.StudentType);
            return await query.ToArrayAsync();
        }

        //Get one record
        public async Task<Student> GetOneStudentAsync(Guid studentId)
        {
            IQueryable<Student> query = _appDbContext.Student.Where(c => c.StudentId == studentId).Include(e => e.StudentType);
            return await query.FirstOrDefaultAsync();
        }

        //Add student
        public async Task<Student> AddStudent(Student newStudent)
        {
            _appDbContext.Student.Add(newStudent);
            await _appDbContext.SaveChangesAsync();
            return newStudent;
        }

        //Update student
        public async Task<Student> EditStudent(Student editedStudent)
        {
            _appDbContext.Entry(editedStudent).State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();
            return editedStudent;
        }

        //Delete student
        public bool DeleteStudent(Guid ID)
        {
            bool result = false;
            var student = _appDbContext.Student.FirstOrDefault(p => p.StudentId == ID);
            if (student != null)
            {
                _appDbContext.Student.Remove(student);
                _appDbContext.SaveChanges();
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        //////////////////////////////////////////////////////////////////////////////
        //View Student types
        public async Task<StudentType[]> GetStudentTypeAsync()
        {
            IQueryable<StudentType> query = _appDbContext.StudentType;
            return await query.ToArrayAsync();
        }

        //Get one record
        public async Task<StudentType> GetOneStudentTypeAsync(Guid studentTypeId)
        {
            IQueryable<StudentType> query = _appDbContext.StudentType.Where(c => c.StudentTypeId == studentTypeId);
            return await query.FirstOrDefaultAsync();
        }


        //Add student type
        public async Task<StudentType> AddStudentType(StudentType newStudentType)
        {
            _appDbContext.StudentType.Add(newStudentType);
            await _appDbContext.SaveChangesAsync();
            return newStudentType;
        }

        //Update student type
        public async Task<StudentType> EditStudentType(StudentType editedStudentType)
        {
            _appDbContext.Entry(editedStudentType).State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();
            return editedStudentType;
        }

        //Delete student
        public bool DeleteStudentType(Guid ID)
        {
            bool result = false;
            var studentType = _appDbContext.StudentType.FirstOrDefault(p => p.StudentTypeId == ID);
            if (studentType != null)
            {
                // Check if the type is referenced in the other table
                var isReferencedInStudent = _appDbContext.Student.Any(e => e.StudentTypeId == ID);

                if (isReferencedInStudent)
                {
                    // If referenced, do not delete and return false
                    return false;
                }
                else
                {
                    _appDbContext.StudentType.Remove(studentType);
                    _appDbContext.SaveChanges();
                    return true;
                }
            }
            else
            {
                result = false;
            }
            return result;
        }
    }
}
