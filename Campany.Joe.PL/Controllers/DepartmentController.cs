using Campany.Joe.PL.Dtos;
using Company.BLL.Interfaces;
using Company.BLL.Repositories;
using Company.DAL.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Campany.Joe.PL.Controllers
{
    public class DepartmentController : Controller
    {
        IDepartmentRepository _repository;
        public DepartmentController(IDepartmentRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public IActionResult Index()
        {
            
           var department= _repository.GetAll();
            return View(department);
        }

        [HttpGet]
        public IActionResult Create()
        {
          

           return View();   
        }
        [HttpPost]
        public IActionResult Create(CreateDepartmentDtos model)
        {
            if(ModelState.IsValid) //ServerSide Validation To Props Inside CreateDepartmentDtos Class
            {
                var department = new Department() //Casting Model From "CreateDepartmentDtos" class To "Department" class 
                {                                 //for save date of "model" in Our Database
                Code = model.Code,
                Name = model.Name,
                CreateAt=model.CreateAt

                };

              var count=  _repository.Add(department);
                if(count>0) //if saved data of "model" in our database, the "Add" fun will increase
                            //one, is returned to index 
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);

        }
    }
}
