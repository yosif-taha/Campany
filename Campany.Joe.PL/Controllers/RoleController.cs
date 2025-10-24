using Campany.Joe.PL.Dtos;
using Campany.Joe.PL.Helpers;
using Company.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Campany.Joe.PL.Controllers
{
    [Authorize]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string? SearchInput)
        {
            IEnumerable<RoleToReturnDto> roles;
            if (string.IsNullOrEmpty(SearchInput))
            {
                roles = _roleManager.Roles.Select(U => new RoleToReturnDto()
                {
                    Id = U.Id,
                    Name = U.Name
                });
            }
            else
            {

                roles =  _roleManager.Roles.Select(U => new RoleToReturnDto()
                {
                    Id = U.Id,
                    Name = U.Name
                }).Where(R => R.Name.ToLower().Contains(SearchInput.ToLower()));

                
            }
            return View(roles);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost] //Use when send data through form
        public async Task<IActionResult> Create(RoleToReturnDto model)
        {

            if (ModelState.IsValid) //ServerSide Validation To Props Inside CreateEmployeeDtos Class
            {
              var role= await _roleManager.FindByNameAsync(model.Name);
                if (role == null)
                {
                    role=new IdentityRole()
                    {
                        Name= model.Name
                    };
                   var result= await _roleManager.CreateAsync(role);

                    if(result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                }      
            }
            return View(model);
        }
        public async Task<IActionResult> Details(string? id, string ViewName)
        {
            if (id == null) return BadRequest("Invalid Id");
            var role = await _roleManager.FindByIdAsync(id);
            if (role is null) return NotFound(new { StatusCode = 400, Message = $"Role With Id {id} Is Not Found" });
            var Dto = new RoleToReturnDto()
            {
                Id = role.Id,
              Name=role.Name
            };
            return View(ViewName, Dto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {

            if (id == null) return BadRequest("Invalid Id");
            var role = await _roleManager.FindByIdAsync(id);
            if (role is null) return NotFound(new { StatusCode = 400, Message = $"User With Id {id} Is Not Found" });

            var Dto = new RoleToReturnDto()
            {
                Id = role.Id,
                Name= role.Name
            };



            return View(Dto);
        }

        [HttpPost] //Use when send data through form
        [ValidateAntiForgeryToken]//Use with any form, for prevent any external tool or app 
                                  //from accessing that form
        public async Task<IActionResult> Edit([FromRoute] string id, RoleToReturnDto model)
        {
            if (ModelState.IsValid) //ServerSide Validation To Props Inside CreateDepartmentDtos Class
            {

                if (id != model.Id) return BadRequest("invalid Operation !!");
                var role = await _roleManager.FindByIdAsync(id);
                if (role is null) return BadRequest("invalid operation !!");
                var roleResult= await _roleManager.FindByNameAsync(model.Name);
                
                if (roleResult is  null)
                {
                    role.Name = model.Name;
                    var result = await _roleManager.UpdateAsync(role);

                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                ModelState.AddModelError("","invalid operation");
               

            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {
            //if (id == null) return BadRequest("Invalid Id");
            //var department = _employeerepository.Get(id.Value);
            //if (department is null) return NotFound(new { StatusCode = 400, Message = $"Department With Id {id} Is Not Found" });

            return await Details(id, "Delete");
        }
        [HttpPost] //Use when send data through form
        [ValidateAntiForgeryToken]//Use with any form, for prevent any external tool or app 
                                  //from accessing that form
        public async Task<IActionResult> Delete([FromRoute] string id, RoleToReturnDto model)
        {
            if (ModelState.IsValid) //ServerSide Validation To Props Inside CreateDepartmentDtos Class
            {

                if (id != model.Id) return BadRequest("invalid id");
                var role = await _roleManager.FindByIdAsync(id);
                if (role is null) return BadRequest("invalid operation !");

                    role.Name = model.Name;
                    var result = await _roleManager.DeleteAsync(role);

                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(Index));
                    }
               
                ModelState.AddModelError("", "invalid operation");

            }
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> AddOrRemoveUsers(string roleId)
        {
          var role= await _roleManager.FindByIdAsync(roleId);
            if(role is null)
                return NotFound();

            ViewData["RoleId"] = roleId;

            var userInRole = new List<UserInRoleDto>();

            var users= await _userManager.Users.ToListAsync();

            foreach (var user in users)
            {
                var userInRoles = new UserInRoleDto()
                {
                    Id = user.Id,
                    UserName=user.UserName
                };
                if( await _userManager.IsInRoleAsync(user,role.Name))
                {
                    userInRoles.IsSelected = true;
                }
                else
                {
                    userInRoles.IsSelected = false;
                }
                userInRole.Add(userInRoles);
            }

            return View(userInRole);
        }
        [HttpPost]
        public async Task<IActionResult> AddOrRemoveUsers(string roleId,List<UserInRoleDto> users)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role is null)
                return NotFound();

            if(ModelState.IsValid)
            {

                foreach (var user in users)
                {

                    var appUser = await _userManager.FindByIdAsync(user.Id);
                    if (appUser is not null)
                    {
                        if (user.IsSelected && !await _userManager.IsInRoleAsync(appUser, role.Name))
                        {
                            await _userManager.AddToRoleAsync(appUser, role.Name);
                        }



                        else if (!user.IsSelected && await _userManager.IsInRoleAsync(appUser, role.Name))
                        {
                            await _userManager.RemoveFromRoleAsync(appUser, role.Name);
                        }
                    }

                }
                return RedirectToAction(nameof(Edit), new {id =roleId});
            }
           return View(users);
        }
    }
}
