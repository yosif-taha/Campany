using Campany.Joe.PL.Dtos;
using Company.BLL.Interfaces;
using Company.BLL.Repositories;
using Company.DAL.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;

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
        [HttpPost] //Use when send data through form
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
        [HttpGet]
        public IActionResult Details(int? id,string ViewName)
        {
            if (id == null) return BadRequest("Invalid Id");
           var department= _repository.Get(id.Value);
            if (department is null) return NotFound(new {StatusCode=400,Message=$"Department With Id {id} Is Not Found" });
            return View(ViewName,department);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            //if (id == null) return BadRequest("Invalid Id");
            //var department = _repository.Get(id.Value);
            //if (department is null) return NotFound(new { StatusCode = 400, Message = $"Department With Id {id} Is Not Found" });

            return Details(id,"Edit");       
        }

        [HttpPost] //Use when send data through form
        [ValidateAntiForgeryToken]//Use with any form, for prevent any external tool or app 
                                  //from accessing that form
        public IActionResult Edit([FromRoute] int id,Department model)
        {
            if (ModelState.IsValid) //ServerSide Validation To Props Inside CreateDepartmentDtos Class
            {
                if(id!=model.Id) return BadRequest();//Error 400
                var count = _repository.Update(model);
                if (count > 0) //if saved data of "model" in our database, the "Add" fun will increase
                               //one, is returned to index 
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            //if (id == null) return BadRequest("Invalid Id");
            //var department = _repository.Get(id.Value);
            //if (department is null) return NotFound(new { StatusCode = 400, Message = $"Department With Id {id} Is Not Found" });

            return Details(id,"Delete");
        }
        [HttpPost] //Use when send data through form
        [ValidateAntiForgeryToken]//Use with any form, for prevent any external tool or app 
                                  //from accessing that form
        public IActionResult Delete([FromRoute] int id, Department model)
        {
            if (ModelState.IsValid) //ServerSide Validation To Props Inside CreateDepartmentDtos Class
            {
                if (id != model.Id) return BadRequest();//Error 400
                var count = _repository.Delete(model);
                if (count > 0) //if saved data of "model" in our database, the "Add" fun will increase
                               //one, is returned to index 
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }

    }
}
