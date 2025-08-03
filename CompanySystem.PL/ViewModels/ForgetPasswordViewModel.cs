using System.ComponentModel.DataAnnotations;

namespace CompanySystem.PL.ViewModels
{
    public class ForgetPasswordViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }
    }
}
