using Company.BLL.Interfaces;
using Company.DAL.Models;
using Company.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.DAL.Data.Contexts;

namespace Company.BLL.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        //readonly: for connect with DB Only Once
       private readonly CampanyDbContext  _context;
        public DepartmentRepository(CampanyDbContext context)
        {
            _context = context;  
        }

        public IEnumerable<Department> GetAll()
        {
            
            return _context.Departments.ToList();
        }

        public Department? Get(int id)
        {
            
            return _context.Departments.Find(id);
        }

        public int Add(Department model)
        {
         
             _context.Departments.Add(model);
            return _context.SaveChanges();
        }

        public int Update(Department model)
        {
            _context.Departments.Update(model);
            return _context.SaveChanges();
        }

        public int Delete(Department model)
        {
            _context.Departments.Remove(model);
            return _context.SaveChanges();
        }



       
    }
}
