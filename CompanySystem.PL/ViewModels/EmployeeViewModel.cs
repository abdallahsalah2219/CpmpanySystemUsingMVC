﻿using DAL.Models;
using System.ComponentModel.DataAnnotations;
using System;

namespace CompanySystem.PL.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name Is Required!")]
        [MaxLength(50, ErrorMessage = "Max Length of Name is 50 chars")]
        [MinLength(5, ErrorMessage = "Min Length of Name is 5 chars")]
        public string Name { get; set; }
        [Range(22, 30)]
        public int? Age { get; set; }
        [RegularExpression(@"^\d+-[A-Za-z\s]+-[A-Za-z\s]+-[A-Za-z\s]+$",
            ErrorMessage = "Address must be in the format 123-Street-City-Country")]
        public string Address { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Display(Name = "Phone Number")]
        [Phone]

        public string PhoneNumber { get; set; }
        [Display(Name = "Hiring Date ")]
        public DateTime HireDate { get; set; }

        public int? DepartmentId { get; set; }// Foreign Key Column
        // Navigational Property =>[One]
        public Department Department { get; set; }
    }
}
