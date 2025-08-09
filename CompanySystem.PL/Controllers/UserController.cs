using AutoMapper;
using CompanySystem.PL.Helpers;
using CompanySystem.PL.ViewModels;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanySystem.PL.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;

        public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager
            ,IMapper mapper )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                var users = await _userManager.Users.Select(U => new UserViewModel()
                {
                    Id = U.Id,
                    FName =U.FName,
                    LName =U.LName,
                    Email =U.Email,
                    PhoneNumber =U.PhoneNumber,
                    Roles=_userManager.GetRolesAsync(U).Result

                }).ToListAsync();
                return View(users);
            }


            else 
            { 
                var user = await _userManager.FindByEmailAsync(email);

                var mappedUser = new UserViewModel()
                {
                    Id = user.Id,
                    FName = user.FName,
                    LName = user.LName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Roles = _userManager.GetRolesAsync(user).Result

                };  
                return View(new List <UserViewModel>() { mappedUser });

            }

        }
        public async Task<IActionResult> Details(string id, string viewName = "Details")
        {
            if (id is null)
                return BadRequest();// 400

            var user = await _userManager.FindByIdAsync (id);
            if (user is null)
                return NotFound();

            var mappedUser = _mapper.Map<ApplicationUser, UserViewModel>(user);

            
            return View(viewName, mappedUser);
        }
        public async Task<IActionResult> Edit(string id)
        {
            

            return await Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, UserViewModel userVM)
        {
            if (id != userVM.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    // 1. جيب الـ User من الـ DB
                    var user = await _userManager.FindByIdAsync(id);
                    if (user == null)
                        return NotFound();

                    // 2. عدل القيم اللي جاية من الـ ViewModel
                    user.FName = userVM.FName;
                    user.LName = userVM.LName;
                    user.PhoneNumber = userVM.PhoneNumber;
                    // ممكن تضيف أي خصائص أخرى هنا لو محتاج

                    // 3. احفظ التعديلات
                    var result = await _userManager.UpdateAsync(user);
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

            return View(userVM);
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
        public async Task<IActionResult> Delete([FromRoute] string id, UserViewModel deletedUser)
        {
            if (id != deletedUser.Id)
                return BadRequest();

            try
            {
                // 1. جيب اليوزر من قاعدة البيانات
                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                    return NotFound();

                // 2. نفذ الحذف
                var result = await _userManager.DeleteAsync(user);

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
            return View(deletedUser);
        }

    }
}
