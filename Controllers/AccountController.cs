using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Net.Http.Headers;
using Skillora.Models.Auth;
using Skillora.Models.ViewModels;
using System.Threading.Tasks;

namespace Skillora.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager; // Updated type to match the constructor parameter
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }


        [HttpGet]
        public IActionResult GetRegistrationFields(string role)
        {
            return ViewComponent("RegistrationFields", new { role });
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user= new AppUser { UserName = model.UserName, Role = model.Role };
                var result = await _userManager.CreateAsync(user,model.Password);
                if(result.Succeeded)
                {
                    if (model.Role == "Student")
                    {
                        if (!await _roleManager.RoleExistsAsync("Student"))
                        {
                            await _roleManager.CreateAsync(new IdentityRole("Student"));
                        }
                        var roleResult = await _userManager.AddToRoleAsync(user, "Student");
                        if (roleResult.Succeeded)
                        {
                            await _signInManager.SignInAsync(user, isPersistent: false);
                            
                            return RedirectToAction("Create", "Student");
                        }
                        foreach (var item in roleResult.Errors)
                        {
                            ModelState.AddModelError(string.Empty, item.Description);
                        }
                    }
                    else if(model.Role=="Company")
                    {
                        if (!await _roleManager.RoleExistsAsync("Company"))
                        {
                            await _roleManager.CreateAsync(new IdentityRole("Company"));
                        }
                        var roleResult = await _userManager.AddToRoleAsync(user, "Company");
                        if (roleResult.Succeeded)
                        {
                            await _signInManager.SignInAsync(user, isPersistent: false);
                            return RedirectToAction("Create", "Company");
                        }
                        foreach (var item in roleResult.Errors)
                        {
                            ModelState.AddModelError(string.Empty, item.Description);
                        }
                    }
                    
                    
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            // Returns the login form view.
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {

                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(model.UserName);

                    await _signInManager.RefreshSignInAsync(user);

                    if (user.Role=="Student")
                    {
                        if (user.StudentId != null)
                            return RedirectToAction("Index", "Student");
                        else
                            return RedirectToAction("Create", "Student");
                        
                    }
                    else if (user.Role=="Company")
                    {
                        if(user.status==false)
                        {
                            return RedirectToAction("AdminApprove", "Company");
                        }
                        else
                        {
                            if (user.CompanyId != null)
                                return RedirectToAction("Index", "Company");
                            else
                                return RedirectToAction("Create", "Company");

                        }
                    }
                    else if(user.Role=="Admin")
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    
                }
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}
