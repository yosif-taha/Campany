using Campany.Joe.PL.Dtos;
using Company.BLL.Interfaces;
using Company.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Campany.Joe.PL.Controllers
{
    public class EmployeeController : Controller
    {
        IEmployeeRepository _repository;
        public EmployeeController(IEmployeeRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public IActionResult Index()
        {

            var employee = _repository.GetAll();
            return View(employee);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost] //Use when send data through form
        public IActionResult Create(CreateEmployeeDtos model)
        {
            if (ModelState.IsValid) //ServerSide Validation To Props Inside CreateEmployeeDtos Class
            {
                var employee = new Employee() //Casting Model From "CreateDepartmentDtos" class To "Department" class 
                {                                 //for save date of "model" in Our Database
                   Name = model.Name,
                   Age = model.Age,
                   Salary = model.Salary,
                   Address = model.Address,
                   Email = model.Email,
                   Phone = model.Phone,
                   HiringDate = model.HiringDate,
                   CreateAt = model.CreateAt,
                   IsActive = model.IsActive,
                   IsDeleted = model.IsDeleted,

                };

                var count = _repository.Add(employee);
                if (count > 0) //if saved data of "model" in our database, the "Add" fun will increase
                               //one, is returned to index 
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);

        }
        [HttpGet]
        public IActionResult Details(int? id, string ViewName)
        {
            if (id == null) return BadRequest("Invalid Id");
            var employee = _repository.Get(id.Value);
            if (employee is null) return NotFound(new { StatusCode = 400, Message = $"Department With Id {id} Is Not Found" });
            return View(ViewName, employee);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null) return BadRequest("Invalid Id");
            var employee = _repository.Get(id.Value);
            if (employee is null) return NotFound(new { StatusCode = 400, Message = $"Department With Id {id} Is Not Found" });
            var employeeDto = new CreateEmployeeDtos() //Casting Model From "CreateDepartmentDtos" class To "Department" class 
            {                                 //for save date of "model" in Our Database
               
                Name = employee.Name,
                Age = employee.Age,
                Salary = employee.Salary,
                Address = employee.Address,
                Email = employee.Email,
                Phone = employee.Phone,
                HiringDate = employee.HiringDate,
                CreateAt = employee.CreateAt,
                IsActive = employee.IsActive,
                IsDeleted = employee.IsDeleted,

            };
            return View(employeeDto);
        }

        [HttpPost] //Use when send data through form
        [ValidateAntiForgeryToken]//Use with any form, for prevent any external tool or app 
                                  //from accessing that form
        public IActionResult Edit([FromRoute] int id, CreateEmployeeDtos model)
        {
            if (ModelState.IsValid) //ServerSide Validation To Props Inside CreateDepartmentDtos Class
            {
                //if (id != model.Id) return BadRequest();//Error 400
                var employee = new Employee() //Casting Model From "CreateDepartmentDtos" class To "Department" class 
                {                                 //for save date of "model" in Our Database
                    Id = id,
                    Name = model.Name,
                    Age = model.Age,
                    Salary = model.Salary,
                    Address = model.Address,
                    Email = model.Email,
                    Phone = model.Phone,
                    HiringDate = model.HiringDate,
                    CreateAt = model.CreateAt,
                    IsActive = model.IsActive,
                    IsDeleted = model.IsDeleted,

                };
                var count = _repository.Update(employee);
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

            return Details(id, "Delete");
        }
        [HttpPost] //Use when send data through form
        [ValidateAntiForgeryToken]//Use with any form, for prevent any external tool or app 
                                  //from accessing that form
        public IActionResult Delete([FromRoute] int id, Employee model)
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
