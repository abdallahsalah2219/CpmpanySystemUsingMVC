using AutoMapper;
using BLL.Interfaces;
using CompanySystem.PL.ViewModels;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CompanySystem.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeRepository _employeeRepo;
        //private readonly IDepartmentRepository _departmentRepo;
        public EmployeeController(IMapper mapper,IEmployeeRepository employeeRepo/*, IDepartmentRepository departmentRepo*/)
        {
            _mapper = mapper;
            _employeeRepo = employeeRepo;
            //_departmentRepo = departmentRepo;
        }
        public IActionResult Index(string searchInp)
        {

            /// Binding Through View's Dictionary : Transfer Data From Action To View => [One Way]
            /// 1- ViewData
            /// ViewData faster than ViewBag
            /// ViewData is Dictionary Type Property(.Net 3.5)
            ///Transfer Data From Action To View
            ///ViewData["Message"] = "Hello ViewData";
            /// 2- ViewBag
            /// ViewBag is Dynamic Type Property(.Net 4.0)
            ///Transfer Data From Action To View
            ///ViewBag.Message = "Hello ViewBag";
          
            var employees = Enumerable.Empty<Employee>();
            if(string.IsNullOrEmpty(searchInp))
              employees = _employeeRepo.GetAll();
            else
                employees=_employeeRepo.SearchByName(searchInp.ToLower());

            var mappedEmp = _mapper.Map<IEnumerable<Employee>,IEnumerable<EmployeeViewModel>>(employees);
            return View(mappedEmp);
        }
        public IActionResult Create()
        {
            //ViewBag.Departments = _departmentRepo.GetAll(); 


            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeViewModel employeeVM)
        {
            if (ModelState.IsValid) // Server Side Validation
            {
                //Manual Mapping
                ///var mappedEmp = new Employee()
                ///{
                ///    Name = employeeVM.Name,
                ///    Age = employeeVM.Age,
                ///    Address = employeeVM.Address,
                ///    Salary = employeeVM.Salary,
                ///    Email = employeeVM.Email,
                ///    PhoneNumber = employeeVM.PhoneNumber,
                ///    IsActive = employeeVM.IsActive,
                ///    HireDate = employeeVM.HireDate,
                ///};
                
                var mappedEmp = _mapper.Map<EmployeeViewModel,Employee>(employeeVM);
                var count = _employeeRepo.Add(mappedEmp);

                /// 3- TempData
                /// TempData is Dictionary Type Property(.Net 3.5)
                /// Used to pass data between two consecutive requests.
                if (count > 0)
                    TempData["Message"] = "Employee Is Created Successfully";
                   
                
                else 
                    TempData["Message"] = "An Error Has Occured ,Employee not Created ";
                  
                
                return RedirectToAction(nameof(Index));

            }
            return View(employeeVM);
        }

        //[HttpGet]
        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (!id.HasValue)
                return BadRequest();// 400

            var employee = _employeeRepo.Get(id.Value);

            var mappedEmp = _mapper.Map<Employee, EmployeeViewModel>(employee);

            if (employee is null)
                return NotFound();// 404
            return View(viewName, mappedEmp);
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
           // ViewBag.Departments = _departmentRepo.GetAll();

            return Details(id, "Edit");
        }
        [HttpPost]

        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, EmployeeViewModel employeeVM)
        {
            if (id != employeeVM.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var mappedEmp = _mapper.Map<EmployeeViewModel,Employee>(employeeVM);
                    _employeeRepo.Update(mappedEmp);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // 1- Log Exception
                    // 2- Friendly Message
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(employeeVM);
        }

        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id, EmployeeViewModel employeeVM)
        {
            if (id != employeeVM.Id)
                return BadRequest();
            try
            {
                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);

                _employeeRepo.Delete(mappedEmp);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
                return View(employeeVM);
            }

        }
    }
}
