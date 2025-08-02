using System.ComponentModel.DataAnnotations;

namespace CompanySystem.PL.ViewModels
{
    public class SignUpViewModel
    {
        [Required(ErrorMessage ="User Name Is Required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "First Name Is Required")]

        public string FName { get; set; }
        [Required(ErrorMessage = "Last Name Is Required")]

        public string LName { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        [Required]
        //[StringLength(30, MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Compare(nameof(Password), ErrorMessage = "Password Mismatch")]
        [DataType(DataType.Password)]

        public string ConfirmPassword { get; set; }

        [Required]

        public bool IsAgree { get; set; }
    }
}
