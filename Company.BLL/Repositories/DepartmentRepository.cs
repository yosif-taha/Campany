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
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        //readonly: for connect with DB Only Once
        public DepartmentRepository(CampanyDbContext context) : base(context)
        {
        }
    }
}
