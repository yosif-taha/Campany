using Company.BLL.Interfaces;
using Company.DAL.Data.Contexts;
using Company.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        CampanyDbContext _context;
        public EmployeeRepository(CampanyDbContext context) : base(context) //ASK CLR to create Object From CampanyDbContext 
        {
            _context = context;
        }

        public async Task<List<Employee>> GetNameAsync(string name)
        {
            return await _context.Employees.Include(E => E.Department).Where(E => E.Name.ToLower().Contains(name.ToLower())).ToListAsync() ;
        }
    }
}
