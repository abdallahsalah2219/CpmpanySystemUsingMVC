﻿using BLL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CompanySystem.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepo;

        public DepartmentController(IDepartmentRepository departmentRepo)
        {
            _departmentRepo = departmentRepo;
        }
        public IActionResult Index()
        {
            var departments = _departmentRepo.GetAll();

            return View(departments);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Department department)
        {
            if (ModelState.IsValid) // Server Side Validation
            {
              var count=  _departmentRepo.Add(department);
                if(count > 0)
                    return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        [HttpGet]
        public IActionResult Details(int? id , string viewName = "Details") 
        {
            if (!id.HasValue)
                return BadRequest();// 400

            var department = _departmentRepo.Get(id.Value);

            if (department is null)
                return NotFound();// 404
            return View( viewName, department);
        }

        public IActionResult Edit(int? id)
        {
            ///if(id is null)
            ///    return BadRequest();
            ///var department = _departmentRepo.Get(id.Value);
            ///if (department is null)
            ///    return NotFound();
            ///return View(department);
            ///

            return Details(id,"Edit");
        }
        [HttpPost]
        
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute]int id ,Department department)
        {
            if(id!= department.Id)
                return BadRequest();
            if (ModelState.IsValid) 
            {
                try
                {
                    _departmentRepo.Update(department);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // 1- Log Exception
                    // 2- Friendly Message
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(department);
        }

        public IActionResult Delete(int? id) 
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id, Department department)
        {
            if (id!= department.Id)
                return BadRequest();
            try
            {
                _departmentRepo.Delete(department);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
                return View(department);
            }
           
        }
    }
}
