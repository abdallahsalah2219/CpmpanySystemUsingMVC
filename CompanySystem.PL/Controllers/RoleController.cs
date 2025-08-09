using CompanySystem.PL.ViewModels;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace CompanySystem.PL.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public RoleController(RoleManager<IdentityRole> roleManager ,
            IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                var roles = await _roleManager.Roles.Select(R => new RoleViewModel()
                {
                    Id = R.Id,
                    RoleName = R.Name
                    

                }).ToListAsync();
                return View(roles);
            }


            else
            {
                var role = await _roleManager.FindByNameAsync(name);

                if (role == null)
                {
                    // ممكن ترجع صفحة NotFound أو تعرض رسالة فاضية
                    return View(new List<RoleViewModel>());
                }

                var mappedRole = new RoleViewModel()
                {
                    Id = role.Id,
                    RoleName = role.Name
                };

                return View(new List<RoleViewModel> { mappedRole });
            }


        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(RoleViewModel model) 
        {
            if (ModelState.IsValid) 
            {
                var mappedRole = _mapper.Map<RoleViewModel,IdentityRole>(model);
               await _roleManager.CreateAsync(mappedRole);
                return RedirectToAction(nameof(Index));
                
            }
            return View(model);
            
        }


        public async Task<IActionResult> Details(string id, string viewName = "Details")
        {
            if (id is null)
                return BadRequest();// 400

            var role = await _roleManager.FindByIdAsync(id);
            if (role is null)
                return NotFound();

            var mappedRole = _mapper.Map<IdentityRole, RoleViewModel>(role);


            return View(viewName, mappedRole);
        }
        public async Task<IActionResult> Edit(string id)
        {


            return await Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, RoleViewModel updatedRole)
        {
            if (id != updatedRole.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    // 1. جيب الـ User من الـ DB
                    var role = await _roleManager.FindByIdAsync(id);
                    if (role == null)
                        return NotFound();

                    // 2. عدل القيم اللي جاية من الـ ViewModel
                    role.Name = updatedRole.RoleName;
                    

                    // 3. احفظ التعديلات
                    var result = await _roleManager.UpdateAsync(role);
                    if (result.Succeeded)
                        return RedirectToAction(nameof(Index));

                    // لو فيه Errors من Identity
                    foreach (var error in result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            return View(updatedRole);
        }

        public async Task<IActionResult> Delete(string id)
        {
            return await Details(id, "Delete");
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Delete([FromRoute] string id, UserViewModel deletedUser)
        //{
        //    if (id != deletedUser.Id)
        //        return BadRequest();
        //    try
        //    {
        //        var mappedUser = _mapper.Map<UserViewModel,ApplicationUser>(deletedUser);
        //        await _userManager.DeleteAsync(mappedUser);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch (Exception ex)
        //    {

        //        ModelState.AddModelError(string.Empty, ex.Message);
        //        return View(deletedUser);
        //    }

        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] string id, RoleViewModel deletedRole)
        {
            if (id != deletedRole.Id)
                return BadRequest();

            try
            {
                // 1. جيب اليوزر من قاعدة البيانات
                var role = await _roleManager.FindByIdAsync(id);
                if (role == null)
                    return NotFound();

                // 2. نفذ الحذف
                var result = await _roleManager.DeleteAsync(role);

                // 3. تحقق من النتيجة
                if (result.Succeeded)
                    return RedirectToAction(nameof(Index));

                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            // لو فيه خطأ، رجع نفس البيانات عشان تتعرض في الـ View
            return View(deletedRole);
        }
    }
}
