using Microsoft.EntityFrameworkCore;

namespace Architecture.Models
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _appDbContext;

        public EmployeeRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        // Employee
        public async Task<Employee[]> SearchEmployeesAsync(string query)
        {
            // Implement the search logic, e.g., using Entity Framework
            // Sample implementation assuming a context with Employee DbSet:
            return await _appDbContext.Employee
                .Where(e => e.EmployeeName.Contains(query))
                .ToArrayAsync();
        }

        public async Task<Employee[]> GetAllEmployeeAsync()
        {
            IQueryable<Employee> query = _appDbContext.Employee.Include(e => e.EmployeeType);
            return await query.ToArrayAsync();
        }

        //Get one record
        public async Task<Employee> GetEmployeeAsync(Guid employeeId)
        {
            IQueryable<Employee> query = _appDbContext.Employee.Where(c => c.EmployeeId == employeeId).Include(e => e.EmployeeType);
            return await query.FirstOrDefaultAsync();
        }

        //Add Employee
        public async Task<Employee> AddEmployee(Employee newEmployee)
        {
            _appDbContext.Employee.Add(newEmployee);
            await _appDbContext.SaveChangesAsync();
            return newEmployee;
        }

        //Update Employee
        public async Task<Employee> EditEmployee(Employee editedEmployee)
        {
            _appDbContext.Entry(editedEmployee).State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();
            return editedEmployee;
        }

        public bool DeleteEmployee(Guid ID)
        {
            var employee = _appDbContext.Employee.FirstOrDefault(p => p.EmployeeId == ID);
            if (employee != null)
            {
                _appDbContext.Employee.Remove(employee);
                _appDbContext.SaveChanges();
                return true;
            }
            return false; // Employee with the given ID not found.
        }

        //////////////////////////////////////////////////////////////////////////////
        // Employee type

        public async Task<EmployeeType[]> GetEmployeeTypesAsync()
        {
            IQueryable<EmployeeType> query = _appDbContext.EmployeeType;
            return await query.ToArrayAsync();
        }

        //Get one record
        public async Task<EmployeeType> GetOneEmployeeTypeAsync(Guid employeeTypeId)
        {
            IQueryable<EmployeeType> query = _appDbContext.EmployeeType.Where(c => c.EmployeeTypeId == employeeTypeId);
            return await query.FirstOrDefaultAsync();
        }


        //Add employee type
        public async Task<EmployeeType> AddEmployeeType(EmployeeType newEmployeeType)
        {
            _appDbContext.EmployeeType.Add(newEmployeeType);
            await _appDbContext.SaveChangesAsync();
            return newEmployeeType;
        }

        //Update employee type
        public async Task<EmployeeType> EditEmployeeType(EmployeeType editedEmployeeType)
        {
            _appDbContext.Entry(editedEmployeeType).State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();
            return editedEmployeeType;
        }

        public bool DeleteEmployeeType(Guid ID)
        {
            var employeeType = _appDbContext.EmployeeType.FirstOrDefault(p => p.EmployeeTypeId == ID);

            if (employeeType != null)
            {
                // Check if the application type is referenced in the employee table
                var isReferencedInEmployee = _appDbContext.Employee.Any(e => e.EmployeeTypeId == ID);

                if (isReferencedInEmployee)
                {
                    // If referenced, do not delete and return false
                    return false;
                }
                else
                {
                    _appDbContext.EmployeeType.Remove(employeeType);
                    _appDbContext.SaveChanges();
                    return true;
                }
            }
            else
            {
                return false;
            }
        }


    }
}
