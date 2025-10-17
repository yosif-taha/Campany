using AutoMapper;
using Campany.Joe.PL.Dtos;
using Campany.Joe.PL.Helpers;
using Company.BLL.Interfaces;
using Company.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Threading.Tasks;

namespace Campany.Joe.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        //IEmployeeRepository _employeerepository;
        //IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public EmployeeController(
            //IEmployeeRepository repository,
            //  IDepartmentRepository departmentRepository,
            IUnitOfWork unitOfWork,
              IMapper mapper
            )
        {
            _unitOfWork = unitOfWork;
            //_employeerepository = repository;
            //_departmentRepository = departmentRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string? SearchInput)
        {
            IEnumerable<Employee> employee;
            if (string.IsNullOrEmpty(SearchInput))
            {
                employee = await _unitOfWork.EmployeeRepository.GetAllAsync();
            }
            else
            {
                employee = await _unitOfWork.EmployeeRepository.GetNameAsync(SearchInput);
            }
            return View(employee);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var department = await _unitOfWork.DepartmentRepository.GetAllAsync();
            ViewData["department"] = department;
            return View();
        }
        [HttpPost] //Use when send data through form
        public async Task<IActionResult> Create(CreateEmployeeDtos model)
        {

            if (ModelState.IsValid) //ServerSide Validation To Props Inside CreateEmployeeDtos Class
            {
                if (model.Image is not null)
                {
                    model.ImageName = DeocumentSettings.UploadFile(model.Image, "images");
                }

                var employee = _mapper.Map<Employee>(model); //Casting Model From "CreateDepartmentDtos" class To "Department" class 
                                                             //for save date of "model" in Our Database
                await _unitOfWork.EmployeeRepository.Add(employee);
                var count = await _unitOfWork.CompleteAsync();
                if (count > 0) //if saved data of "model" in our database, the "Add" fun will increase
                               //one, is returned to index 
                {
                    TempData["Message"] = "Employee Is Created !!";
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);

        }
        [HttpGet]
        public async Task<IActionResult> Details(int? id, string ViewName)
        {
            if (id == null) return BadRequest("Invalid Id");
            var employee = await _unitOfWork.EmployeeRepository.GetAsync(id.Value);
            if (employee is null) return NotFound(new { StatusCode = 400, Message = $"Department With Id {id} Is Not Found" });
            return View(ViewName, employee);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            var department = await _unitOfWork.DepartmentRepository.GetAllAsync();
            ViewData["department"] = department;
            if (id == null) return BadRequest("Invalid Id");
            var employee = await _unitOfWork.EmployeeRepository.GetAsync(id.Value);
            if (employee is null) return NotFound(new { StatusCode = 400, Message = $"Department With Id {id} Is Not Found" });

            var employeeDto = _mapper.Map<CreateEmployeeDtos>(employee); //Casting Model From "CreateDepartmentDtos" class To "Department" class 
                                                                         //for save date of "model" in Our Database, USing auto Mapper.

            return View(employeeDto);
        }

        [HttpPost] //Use when send data through form
        [ValidateAntiForgeryToken]//Use with any form, for prevent any external tool or app 
                                  //from accessing that form
        public async Task<IActionResult> Edit([FromRoute] int id, CreateEmployeeDtos model)
        {
            if (ModelState.IsValid) //ServerSide Validation To Props Inside CreateDepartmentDtos Class
            {
                if (model.Image is not null && model.ImageName is not null)
                {
                    DeocumentSettings.DeleteFile(model.ImageName, "images");
                }
                if (model.Image is not null )
                {
                  model.ImageName=  DeocumentSettings.UploadFile(model.Image, "images");
                }


                var employee = _mapper.Map<Employee>(model);  //Casting Model From "CreateDepartmentDtos" class To "Department" class 
                                                      //for save date of "model" in Our Database
                 employee.Id = id;
                _unitOfWork.EmployeeRepository.Update(employee);
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
            //var department = _employeerepository.Get(id.Value);
            //if (department is null) return NotFound(new { StatusCode = 400, Message = $"Department With Id {id} Is Not Found" });

            return Details(id, "Delete");
        }
        [HttpPost] //Use when send data through form
        [ValidateAntiForgeryToken]//Use with any form, for prevent any external tool or app 
                                  //from accessing that form
        public async Task<IActionResult> Delete([FromRoute] int id, CreateEmployeeDtos model)
        {
            if (ModelState.IsValid) //ServerSide Validation To Props Inside CreateDepartmentDtos Class
            {
               var employee = _mapper.Map<Employee>(model);
                employee.Id = id;
                _unitOfWork.EmployeeRepository.Delete(employee);
                var count = await _unitOfWork.CompleteAsync();

                if (count > 0) //if saved data of "model" in our database, the "Add" fun will increase
                               //one, is returned to index 
                {
                    if(model.ImageName is not null)
                    {
                        DeocumentSettings.DeleteFile(model.ImageName,"images");
                    }
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }
    }
}
