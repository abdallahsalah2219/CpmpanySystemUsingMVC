using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Employee : BaseEntity
    {
     
 
        public string Name { get; set; }
       
        public int? Age { get; set; }
        
        public string Address { get; set; }

        public decimal Salary { get; set; }
      
        public bool IsActive { get; set; }
        
        public string Email { get; set; }
        
       
        public string PhoneNumber { get; set; }
        
        public DateTime HireDate { get; set; }

        public bool IsDeleted { get; set; } = false;
        public DateTime CreationDate { get; set; }= DateTime.Now;
        public int? DepartmentId { get; set; }// Foreign Key Column
        // Navigational Property =>[One]
        public Department Department { get; set; }

    }
}
