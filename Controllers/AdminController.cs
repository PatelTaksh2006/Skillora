using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Skillora.Models.Auth;
using Skillora.Models.Entities;
using Skillora.Models.ViewModels;
using Skillora.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skillora.Controllers
{
    public class AdminController : Controller
    {
        private UserManager<AppUser> _userManager { get; set; }
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;

        public AdminController(UserManager<AppUser> userManager, ICompanyService companyService,IMapper mapper)
        {
            _userManager = userManager;
            _companyService = companyService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CompanyApproval()
        {
            var user = await _userManager.GetUserAsync(User);
            var companies =await _userManager.Users.Where(u => u.Role == "Company" && !u.status).AsNoTracking().ToListAsync();
            List<CompanyViewModel> companyViewModels = new List<CompanyViewModel>();
           
            foreach (var item in companies)
            {
                var company = _companyService.Get(item.CompanyId);
                var tempModel=_mapper.Map<CompanyViewModel>(company);
                tempModel.userId = item.Id; 
                companyViewModels.Add(tempModel);
            }

            
            return View(companyViewModels);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveCompany(string userId)
        {
            Console.WriteLine(userId);
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null && user.Role == "Company")
            {
                user.status = true;
                await _userManager.UpdateAsync(user);
            }
            return RedirectToAction("CompanyApproval");
        }

        [HttpPost]
        public async Task<IActionResult> RejectCompany(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            string company = user.CompanyId;
            _companyService.Delete(company);
            return RedirectToAction("CompanyApproval");

        }


        
    }
}
