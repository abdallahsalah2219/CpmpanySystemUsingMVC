using BLL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CompanySystem.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepo;

        public EmployeeController(IEmployeeRepository employeeRepo)
        {
            _employeeRepo = employeeRepo;
        }
        public IActionResult Index()
        {
            var employees = _employeeRepo.GetAll();

            return View(employees);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid) // Server Side Validation
            { 
                var count = _employeeRepo.Add(employee);
                if (count > 0)
                    return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        [HttpGet]
        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (!id.HasValue)
                return BadRequest();// 400

            var Employee = _employeeRepo.Get(id.Value);

            if (Employee is null)
                return NotFound();// 404
            return View(viewName, Employee);
        }

        public IActionResult Edit(int? id)
        {
            ///if(id is null)
            ///    return BadRequest();
            ///var Employee = _employeeRepo.Get(id.Value);
            ///if (Employee is null)
            ///    return NotFound();
            ///return View(Employee);
            ///

            return Details(id, "Edit");
        }
        [HttpPost]

        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, Employee Employee)
        {
            if (id != Employee.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    _employeeRepo.Update(Employee);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // 1- Log Exception
                    // 2- Friendly Message
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(Employee);
        }

        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id, Employee Employee)
        {
            if (id != Employee.Id)
                return BadRequest();
            try
            {
                _employeeRepo.Delete(Employee);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
                return View(Employee);
            }

        }
    }
}
