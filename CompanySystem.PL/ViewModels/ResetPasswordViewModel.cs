using System.ComponentModel.DataAnnotations;

namespace CompanySystem.PL.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required]
        [Compare(nameof(NewPassword), ErrorMessage = "Password Mismatch")]
        [DataType(DataType.Password)]

        public string ConfirmPassword { get; set; }
       
    }
}
