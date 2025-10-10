using Company.BLL.Interfaces;
using Company.BLL.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Campany.Joe.PL.Controllers
{
    public class DepartmentController : Controller
    {
        IDepartmentRepository _repository;
        public DepartmentController(IDepartmentRepository repository)
        {
            _repository = repository;
        }
        public IActionResult Index()
        {
            
           var department= _repository.GetAll();
            return View(department);
        }

        public IActionResult Create()
        {

           return View();   
        }
    }
}
