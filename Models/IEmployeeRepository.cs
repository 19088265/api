namespace Architecture.Models
{
    public interface IEmployeeRepository
    {
        // Employee
        Task<Employee[]> GetAllEmployeeAsync();
        Task<Employee> GetEmployeeAsync(Guid employeeId);
        Task<Employee> AddEmployee(Employee newEmployee);
        Task<Employee> EditEmployee(Employee editedEmployee);
        bool DeleteEmployee(Guid id);
        Task<Employee[]> SearchEmployeesAsync(string query);




        // Employee Type
        Task<EmployeeType[]> GetEmployeeTypesAsync();
        Task<EmployeeType> GetOneEmployeeTypeAsync(Guid employeeTypeId);
        Task<EmployeeType> AddEmployeeType(EmployeeType newEmployeeType);
        Task<EmployeeType> EditEmployeeType(EmployeeType editedEmployeeType);
        bool DeleteEmployeeType(Guid id);
    }
}
