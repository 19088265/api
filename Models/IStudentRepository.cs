namespace Architecture.Models
{
    public interface IStudentRepository
    {
        // Students
        Task<Student[]> GetAllStudentAsync();
        Task<Student> GetOneStudentAsync(Guid studentId);
        Task<Student> AddStudent(Student newStudent);
        Task<Student> EditStudent(Student editedStudent);
        bool DeleteStudent(Guid ID);
        Task<Student[]> SearchStudentsAsync(string query);


        // Student  Type
        Task<StudentType[]> GetStudentTypeAsync();
        Task<StudentType> GetOneStudentTypeAsync(Guid studentTypeId);
        Task<StudentType> AddStudentType(StudentType newStudentType);
        Task<StudentType> EditStudentType(StudentType editedStudentType);
        bool DeleteStudentType(Guid ID);
    }
}
