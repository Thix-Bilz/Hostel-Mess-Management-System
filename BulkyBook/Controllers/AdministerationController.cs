using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
 
    public class AdministerationController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _db;

        public AdministerationController(RoleManager<IdentityRole> roleManager,UserManager<IdentityUser> userManager,ApplicationDbContext applicationDbContext)
        {
            this._roleManager = roleManager;
            this._userManager = userManager;
            this._db = applicationDbContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult createRole()
        {
            return View();
        }
        [HttpPost]
        
        public async Task<ActionResult> createRole(CreateRoleViewModel createRoleViewModel)
        {
          
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole() { Name = createRoleViewModel.roleName };
                IdentityResult result = await _roleManager.CreateAsync(identityRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListOfRoles", "Administeration");
                }
                foreach (var e in result.Errors)
                {
                    ModelState.AddModelError("", e.Description);
                }
            }
            return View(createRoleViewModel);

        }
        [HttpGet]
        public IActionResult ListOfRoles()
        {
            var roles=_roleManager.Roles;
            return View(roles);
        }
        [HttpGet]
        public async Task<IActionResult> editRole(string id)
        {
           var role=await _roleManager.FindByIdAsync(id);
            EditRoleViewModel editRoleViewModel = new EditRoleViewModel()
            {
                Id = role.Id,
                RoleName = role.Name

            };
           
            foreach(var user in _userManager.Users.ToList())
            {
                
                if(await _userManager.IsInRoleAsync(user,role.Name))
                {
                    editRoleViewModel.users.Add(user.UserName);

                }
            }
            return View(editRoleViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> editRole(EditRoleViewModel Model)
        {
            var role = await _roleManager.FindByIdAsync(Convert.ToString(Model.Id));
            role.Name = Model.RoleName;
            var result=await _roleManager.UpdateAsync(role);
            if(result.Succeeded)
            {
               
                return RedirectToAction("ListOfRoles");
            }
            foreach(var e in result.Errors)
            {
                ModelState.AddModelError("", e.Description);
            }

            return View(Model);
        }
        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string Id)
        {
            ViewBag.RoleId = Id;
            var role = await _roleManager.FindByIdAsync(Id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with id={Id} cannot be found";
                return View("NotFound");
            }
            var editUsersInRole = new List<EditUsersInRole>();
            foreach (var user in _userManager.Users.ToList())
            {
                
               
                    EditUsersInRole editUsersInRole1 = new EditUsersInRole
                    {
                        UserId =Convert.ToString(user.Id),
                        UserName = user.UserName,
                     
                       
                    };
                    if(await _userManager.IsInRoleAsync(user,role.Name))
                    {
                        editUsersInRole1.IsSelected = true;
                    }
                    else
                    {
                        editUsersInRole1.IsSelected = false;
                    }
                    editUsersInRole.Add(editUsersInRole1);

                

            }
            return View(editUsersInRole);
        }
       [HttpPost]
       public async Task<IActionResult> EditUsersInRole(List<EditUsersInRole> editUsersInRoles,string id)
        {
            ViewBag.RoleId = id;
            
            var role = await _roleManager.FindByIdAsync(id);
        if(role==null)
            {
                ViewBag.ErrorMessage = $"Role with id={id} cannot be found";
                return View("NotFound");
            }
            for(int i=0;i<editUsersInRoles.Count;i++)
            {

                var user = await _userManager.FindByIdAsync(editUsersInRoles[i].UserId);
                IdentityResult result=null;
                if (editUsersInRoles[i].IsSelected && !await _userManager.IsInRoleAsync(user,role.Name))
                    {
                         result=await _userManager.AddToRoleAsync(user, role.Name);
                    }
                    else if(!editUsersInRoles[i].IsSelected && await _userManager.IsInRoleAsync(user, role.Name))
                    {
                         result=await _userManager.RemoveFromRoleAsync(user,role.Name);
                    }
                    else
                    {
                        continue;
                    }
                if (result.Succeeded)
                {
                    if (i < (editUsersInRoles.Count - 1))
                        continue;
                    else
                        return RedirectToAction("EditRole", new { Id = id });
                }
            }

            return RedirectToAction("EditRole", new { Id = id });

        }
        [HttpGet]
        public IActionResult Configuration()
        {
            return View();
        }
       
  
       
    }

    
}

