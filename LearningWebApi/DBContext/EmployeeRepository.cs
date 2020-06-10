using LearningModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningWebApi.DBContext
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetEmployee();
        Task<Employee> GetEmployeeByID(int id);
        Task<Employee> GetEmployeeByEmail(string email);
        Task<Employee> AddEmployee(Employee employee);
        Task<Employee> UpdateEmployee(Employee employee);
        Task<Employee> DeleteEmployee(int id);

        Task<IEnumerable<Employee>> Search(string name, Gender? gender);

    }
    public class EmployeeRepository : IEmployeeRepository
    {
        public readonly BlazorDBContext dbContext;
        public EmployeeRepository(BlazorDBContext _context)
        {
            dbContext = _context;
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            var result = await dbContext.AddAsync(employee);
            await dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Employee> DeleteEmployee(int id)
        {
            var result = await dbContext.Employee.FirstOrDefaultAsync(x => x.EmployeeId == id);
            if (result != null)
            {
                dbContext.Employee.Remove(result);
                await dbContext.SaveChangesAsync();
                return result;
            }

            return null;
        }

        public async Task<IEnumerable<Employee>> GetEmployee()
        {
            return await dbContext.Employee.Include("Department").ToListAsync();
        }

        public async Task<Employee> GetEmployeeByEmail(string email)
        {
            return await dbContext.Employee.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<Employee> GetEmployeeByID(int id)
        {
            return await dbContext.Employee
                .Include(x => x.Department)
                .FirstOrDefaultAsync(x => x.EmployeeId == id);
        }

        public async Task<IEnumerable<Employee>> Search(string name, Gender? gender)
        {
            IQueryable<Employee> query = dbContext.Employee;
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(x => x.FirstName.Contains(name) || x.LastName.Contains(name));
            }

            if (gender != null)
            {
                query = query.Where(x => x.Gender == gender);
            }

            return await query.ToListAsync();
        }

        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            var result = await dbContext.Employee.FirstOrDefaultAsync(x => x.EmployeeId == employee.EmployeeId);
            if (result != null)
            {
                result.FirstName = employee.FirstName;
                result.LastName = employee.LastName;
                result.DateOfBirth = employee.DateOfBirth;
                result.DepartmentId = employee.DepartmentId;
                result.Email = employee.Email;
                result.Gender = employee.Gender;
                result.PhotoPath = employee.PhotoPath;

                await dbContext.SaveChangesAsync();

                return result;
            }

            return null;
        }
    }
}
