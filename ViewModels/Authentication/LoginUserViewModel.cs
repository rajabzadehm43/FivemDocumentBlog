using System.ComponentModel.DataAnnotations;

namespace ViewModels.Authentication
{
    public class LoginUserViewModel
    {
        [Required]
        public string UsernameOrEmail { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public bool RememberMe { get; set; }
    }
}