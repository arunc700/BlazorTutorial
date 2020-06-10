using LearningModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningWebApi.DBContext
{

    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetDepartment();
        Task<Department> GetDepartmentById(int id);
    }

    public class DepartmentRepository : IDepartmentRepository
    {
        public readonly BlazorDBContext dbContext;
        public DepartmentRepository(BlazorDBContext _context)
        {
            dbContext = _context;
        }

        public async Task<IEnumerable<Department>> GetDepartment()
        {
            return await dbContext.Departments.ToListAsync();
        }

        public async Task<Department> GetDepartmentById(int id)
        {
            return await dbContext.Departments.FirstOrDefaultAsync(x => x.DepartmentId == id);
        }
    }
}
