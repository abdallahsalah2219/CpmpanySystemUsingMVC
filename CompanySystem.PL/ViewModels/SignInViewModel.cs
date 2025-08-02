using System.ComponentModel.DataAnnotations;

namespace CompanySystem.PL.ViewModels
{
    public class SignInViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
