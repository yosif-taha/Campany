using Campany.Joe.PL.Dtos;
using Company.BLL.Interfaces;
using Company.BLL.Repositories;
using Company.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Campany.Joe.PL.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            
           var department= await _unitOfWork.DepartmentRepository.GetAllAsync();
            return View(department);
        }

        [HttpGet]
        public IActionResult Create()
        {
           return View();   
        }
        [HttpPost] //Use when send data through form
        public async Task<IActionResult> Create(CreateDepartmentDtos model)
        {
            if(ModelState.IsValid) //ServerSide Validation To Props Inside CreateDepartmentDtos Class
            {
                var department = new Department() //Casting Model From "CreateDepartmentDtos" class To "Department" class 
                {                                 //for save date of "model" in Our Database
                Code = model.Code,
                Name = model.Name,
                CreateAt=model.CreateAt

                };

               await _unitOfWork.DepartmentRepository.Add(department);
                var count = await _unitOfWork.CompleteAsync();

                if (count>0) //if saved data of "model" in our database, the "Add" fun will increase
                            //one, is returned to index 
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
   
        }
        [HttpGet]
        public async Task<IActionResult> Details(int? id,string ViewName)
        {
            if (id == null) return BadRequest("Invalid Id");
           var department= await _unitOfWork.DepartmentRepository.GetAsync(id.Value);
            if (department is null) return NotFound(new {StatusCode=400,Message=$"Department With Id {id} Is Not Found" });
            return View(ViewName,department);
        }

        [HttpGet]
        public async Task<IActionResult> Edit( int? id)
        {
            if (id == null) return BadRequest("Invalid Id");
            var department = await _unitOfWork.DepartmentRepository.GetAsync(id.Value);
            if (department is null) return NotFound(new { StatusCode = 400, Message = $"Department With Id {id} Is Not Found" });
            var departmentDto = new CreateDepartmentDtos() //Casting Model From "CreateDepartmentDtos" class To "Department" class 
            {                                 //for save date of "model" in Our Database
                Code = department.Code,
                Name = department.Name,
                CreateAt = department.CreateAt

            };
            return View(departmentDto);     
        }

        [HttpPost] //Use when send data through form
        [ValidateAntiForgeryToken]//Use with any form, for prevent any external tool or app 
                                  //from accessing that form
        public async Task<IActionResult> Edit([FromRoute] int id,CreateDepartmentDtos model)
        {
            if (ModelState.IsValid) //ServerSide Validation To Props Inside CreateDepartmentDtos Class
            {
                var department = new Department() //Casting Model From "CreateDepartmentDtos" class To "Department" class 
                {                                 //for save date of "model" in Our Database
                    Id = id,
                    Code = model.Code,
                    Name = model.Name,
                    CreateAt = model.CreateAt
                };
                //if (id!=model.Id) return BadRequest();//Error 400
               _unitOfWork.DepartmentRepository.Update(department);
                var count = await _unitOfWork.CompleteAsync();

                if (count > 0) //if saved data of "model" in our database, the "Add" fun will increase
                               //one, is returned to index 
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }
        [HttpGet]
        public Task<IActionResult> Delete(int? id)
        {
            //if (id == null) return BadRequest("Invalid Id");
            //var department = _repository.Get(id.Value);
            //if (department is null) return NotFound(new { StatusCode = 400, Message = $"Department With Id {id} Is Not Found" });

            return Details(id,"Delete");
        }
        [HttpPost] //Use when send data through form
        [ValidateAntiForgeryToken]//Use with any form, for prevent any external tool or app 
                                  //from accessing that form
        public async Task<IActionResult> Delete([FromRoute] int id, Department model)
        {
            if (ModelState.IsValid) //ServerSide Validation To Props Inside CreateDepartmentDtos Class
            {
                if (id != model.Id) return BadRequest();//Error 400
                model.Id = id;  
                _unitOfWork.DepartmentRepository.Delete(model);
                var count = await _unitOfWork.CompleteAsync();

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
